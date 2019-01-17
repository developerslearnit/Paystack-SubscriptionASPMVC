using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Repo.DTO
{
    public class CartDTO
    {
        public string cartCode { get; set; }

        public string planName { get; set; }

        public string planId { get; set; }
        
        public decimal amount { get; set; }
    }
}
