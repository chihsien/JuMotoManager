using JuMotoManager.Repositories;
using System;
using System.Data;
using System.Data.SqlClient;  // It's for MSSQL 
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;

namespace JuMotoManager.DapperUnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private IDbConnection _connection;
        private IDbTransaction _transaction;
        private IPartsFuncRepo _PartsFuncRepo;
        private IPartsInfoRepo _PartsInfoRepo;
        private ISupplierRepo _SupplierRepo;
        private bool _disposed;

        private readonly IConfiguration _config;

        // public UnitOfWork(string connectionString)
        public UnitOfWork(IConfiguration config)
        {
            _config = config;

            // It's for MSSQL Connection
            // string connectionString = _config["ConnectionStrings:MsSqlConn"];
            // _connection = new SqlConnection(connectionString);

            // It's for Mysql Connection
            string connectionString = _config["ConnectionStrings:MySqlConn"];            
            _connection = new MySqlConnection(connectionString);

            _connection.Open();
            _transaction = _connection.BeginTransaction();
        }

        public IPartsFuncRepo PartsFuncRepo
        {
            get { return _PartsFuncRepo ?? (_PartsFuncRepo = new PartsFuncRepo(_transaction)); }
        }
        public IPartsInfoRepo PartsInfoRepo
        {
            get { return _PartsInfoRepo ?? (_PartsInfoRepo = new PartsInfoRepo(_transaction)); }
        }
        
        public ISupplierRepo SupplierRepo
        {
            get { return _SupplierRepo ?? (_SupplierRepo = new SupplierRepo(_transaction)); }
        }

        public void Commit()
        {
            try
            {
                _transaction.Commit();
            }
            catch
            {
                _transaction.Rollback();
                throw;
            }
            finally
            {
                _transaction.Dispose();
                _transaction = _connection.BeginTransaction();
                resetRepositories();
            }
        }

        private void resetRepositories()
        {
            _PartsFuncRepo = null;
            _PartsInfoRepo = null;
            _SupplierRepo = null;
        }

        public void Dispose()
        {
            dispose(true);
            GC.SuppressFinalize(this);
        }

        private void dispose(bool disposing)
        {
            if (!_disposed)
            {
                if(disposing)
                {
                    if (_transaction != null)
                    {
                        _transaction.Dispose();
                        _transaction = null;
                    }
                    if(_connection != null)
                    {
                        _connection.Dispose();
                        _connection = null;
                    }
                }
                _disposed = true;
            }
        }

        ~UnitOfWork()
        {
            dispose(false);
        }
    }
}
