using System;

namespace JuMotoManager.Models
{

    public class PartsInfo
    {
        public int part_sno {get; set;}
        public string part_number {get; set;}
        public int supplier_sno {get; set;}
        public int func_sno {get; set;}
        public string part_name {get; set;}
        public int list_price {get; set;}
        public int street_price {get; set;}
        public int cost_price {get; set;}
        public DateTime date_created {get; set;}
        public DateTime date_updated {get; set;}
    }
}
