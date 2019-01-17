using App.Domain.Entities;
using App.Repo.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Repo
{
    public interface IAppRepository
    {
        bool AddToCart(CartDTO cartItem);
        CartDTO GetCartItem(string cartCode);
        bool CreateCustomerSubscription(string firstName,string lastName,
            string email, string phone,string customerCode,string planName,decimal amount,
            string payRef,string planId);

        void UpdateSubscription(string payRef);

        void RemoveItemFromCart(string cartCode);

        Subscription GetSubscription(string payRef);

        Customer GetCustomerByEmail(string email);

        bool CreateSubscriberAccount(string email, string password, string planCode,string subscriptionCode,string emailToken, DateTime expDate);
    }
}
