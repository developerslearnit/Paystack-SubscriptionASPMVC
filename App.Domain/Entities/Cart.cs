using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Entities
{
    public class Cart
    {
        public int CartId { get; set; }

        [StringLength(50)]
        [Required]
        public string CartCode { get; set; }

        [StringLength(150)]
        [Required]
        public string PlanName { get; set; }

        [StringLength(50)]
        [Required]
        public string PlanId { get; set; }

        [Required]
        public decimal Amount { get; set; }

        public DateTime CartDate { get; set; }
    }
}
