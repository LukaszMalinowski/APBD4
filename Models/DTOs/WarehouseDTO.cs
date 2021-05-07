using System;
using System.ComponentModel.DataAnnotations;

namespace cwiczenia4_zen_s19743.Models.DTOs
{
    public class WarehouseDTO
    {
        [Required(ErrorMessage = "IdProduct is required")]
        public int? IdProduct { get; set; }
        
        [Required(ErrorMessage = "IdWarehouse is required")]
        public int? IdWarehouse { get; set; }
        
        [Required(ErrorMessage = "Amount is required")]
        [Range(1, Int32.MaxValue, ErrorMessage = "Amount must be bigger than 0.")]
        public int? Amount { get; set; }

        [Required(ErrorMessage = "CreatedAt is required")]
        public DateTime? CreatedAt { get; set; }
    }
}