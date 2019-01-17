using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Entities
{
    public class SubscriberAccount
    {
        public int SubscriberAccountId { get; set; }

        [StringLength(50)]
        [Required]
        public string Email { get; set; }

        [StringLength(50)]
        [Required]
        public string Password { get; set; }

        [StringLength(30)]
        [Required]
        public string PlanCode { get; set; }

        [StringLength(100)]
        [Required]
        public string SubscriptionCode { get; set; }

        [StringLength(100)]
        [Required]
        public string EmailToken { get; set; }

        [Required]
        public DateTime SubscriptionDate { get; set; }

        [Required]
        public DateTime ExpiryDate { get; set; }
    }
}
