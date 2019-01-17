using App.Domain.AppContext;
using App.Domain.Entities;
using App.Repo.DTO;
using System;
using System.Linq;

namespace App.Repo
{
    public class AppRepository : BaseRepository<AppEntities>, IAppRepository
    {
        public bool AddToCart(CartDTO cartItem)
        {
            var cart = new Cart()
            {
                Amount = cartItem.amount,
                CartCode =cartItem.cartCode,
                CartDate =DateTime.Now,
                PlanId =cartItem.planId,
                PlanName =cartItem.planName
            };

            DataContext.Carts.Add(cart);

            return DataContext.SaveChanges() > 0;
        }

        public bool CreateCustomerSubscription(string firstName, string lastName, string email, 
            string phone, string customerCode, string planName, decimal amount, string payRef,string planId)
        {
            var order = new Subscription()
            {
                Amount = amount,
                Confirmed =false,
                CustomerCode = customerCode,
                PaymentRef = payRef,
                PlanName =planName,
                PlanId = planId,
                TransactionDate = DateTime.Now
            };

            var cust = new Customer()
            {
                CustomerCode =customerCode,
                CustomerSince = DateTime.Now,
                Email = email,
                FirstName =firstName,
                LastName =lastName,
                Phone = phone                
            };

            DataContext.Customers.Add(cust);
            DataContext.Subscriptions.Add(order);

            return DataContext.SaveChanges() > 0;

        }

        public bool CreateSubscriberAccount(string email, string password, string planCode, string subscriptionCode, string emailToken, DateTime expDate)
        {
            var subAccount = new SubscriberAccount()
            {
                Email =email,
                Password = password,
                SubscriptionDate = DateTime.Now,
                ExpiryDate = expDate,
                PlanCode = planCode,
                SubscriptionCode = subscriptionCode,
                EmailToken = emailToken
            };

            DataContext.SubscriberAccounts.Add(subAccount);

            return DataContext.SaveChanges() > 0;
        }

        public CartDTO GetCartItem(string cartCode)
        {
            return DataContext.Carts.Where(x => x.CartCode == cartCode)
                    .Select(x => new CartDTO
                    {
                        amount = x.Amount,
                        cartCode =x.CartCode,
                        planId =x.PlanId,
                        planName =x.PlanName

                    }).FirstOrDefault();
        }

        public Customer GetCustomerByEmail(string email)
        {
            return DataContext.Customers.Where(x => x.Email == email).FirstOrDefault();
        }

        public Subscription GetSubscription(string payRef)
        {
            return DataContext.Subscriptions.Where(x => x.PaymentRef == payRef).FirstOrDefault();
        }

        public void RemoveItemFromCart(string cartCode)
        {
            var cart = DataContext.Carts.Where(x => x.CartCode == cartCode).FirstOrDefault();
            if(cart != null)
            {
                DataContext.Carts.Remove(cart);
                DataContext.SaveChanges();
            }
            
        }

        public void UpdateSubscription(string payRef)
        {
            var subscription = DataContext.Subscriptions.Where(x => x.PaymentRef == payRef).FirstOrDefault();
            if(subscription != null)
            {
                subscription.Confirmed = true;
                DataContext.SaveChanges();
            }
        }
    }
}
