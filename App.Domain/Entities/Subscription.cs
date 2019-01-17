using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Entities
{
    public class Subscription
    {
        public int SubscriptionId { get; set; }

        [StringLength(10)]
        [Required]
        public string CustomerCode { get; set; }

        [StringLength(150)]
        [Required]
        public string PlanName { get; set; }

        [StringLength(50)]
        [Required]
        public string PlanId { get; set; }


        [Required]
        public decimal Amount { get; set; }

        [StringLength(50)]
        [Required]
        public string PaymentRef { get; set; }

        [Required]
        public bool Confirmed { get; set; }

        public DateTime TransactionDate { get; set; }

    }
}
