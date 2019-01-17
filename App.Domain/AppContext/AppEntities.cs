using App.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.AppContext
{
    public class AppEntities : DbContext
    {
        public AppEntities(): base("name=appConnection")
        {

        }

        public DbSet<Customer> Customers { get; set; }

        public DbSet<Cart> Carts { get; set; }

        public DbSet<Subscription> Subscriptions { get; set; }

        public DbSet<SubscriberAccount> SubscriberAccounts { get; set; }
    }
}
