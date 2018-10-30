using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using JuMotoManager.Services;
using JuMotoManager.Models;

namespace JuMotoManager.Controllers
{
    [Route("Product")]
    public class ProductController : Controller
    {
        private readonly ISupplierService _SupplierSvc;

        public ProductController(ISupplierService SupplierSvc)
        {
            _SupplierSvc = SupplierSvc;

        }

        [Route("Index")]
        public IActionResult Index()
        {    
            return View();
        }

        // [HttpGet]
        // public IEnumerable<Supplier> Get()
        // {         
        //     List<Supplier> r = _SupplierSvc.Get();
        //     return r.AsEnumerable();
        //     // return new string[] { "value1", "value2" };         
        // }

        [Route("GetList")]
        public IEnumerable<Supplier> GetList()
        {         
            List<Supplier> r = _SupplierSvc.Get();
            return r.AsEnumerable();
            // return new string[] { "value1", "value2" };         
        }
    }
}