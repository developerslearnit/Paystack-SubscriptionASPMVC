using App.Repo;
using Paystack.Net.SDK.Transactions;
using PaystackDirectDebit.AppHelper;
using PaystackDirectDebit.Models;
using System;
using System.Configuration;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace PaystackDirectDebit.Controllers
{
    [RoutePrefix("billing")]
    public class BillingController : Controller
    {
        IAppRepository repo;
        public BillingController(IAppRepository _repo)
        {
            repo = _repo;
        }


        [Route("paystatus/check")]
        public async Task<ActionResult> PaymentCallBack()
        {
            var tranxRef = HttpContext.Request.QueryString["reference"];

            if (!string.IsNullOrWhiteSpace(tranxRef))
            {
                var paystackSec_Key = ConfigurationManager.AppSettings["PayStackKey"].ToString();
                var payStackAPI = new PaystackTransaction(paystackSec_Key);
                var response = await payStackAPI.VerifyTransaction(tranxRef);

                if (response.status)
                {
                    var subscriberInfo = repo.GetSubscription(tranxRef);
                    var planId = subscriberInfo.PlanId;
                    var subscriptionStartDate = DateTime.Now.AddMonths(1);
                    var payStckSubscription = new Paystack.Net.SDK.Subscription.PaystackSubscription(paystackSec_Key);
                    var subscriptionResponse = await payStckSubscription.CreateSubscription(response.data.customer.email, planId,
                        response.data.authorization.authorization_code, subscriptionStartDate.ToString("s"));
                    var customer = repo.GetCustomerByEmail(response.data.customer.email);
                    if (subscriptionResponse.status)
                    {
                        var data = subscriptionResponse.data;

                        repo.CreateSubscriberAccount(response.data.customer.email, "password", planId, data.subscription_code, data.email_token, subscriptionStartDate);
                        
                        repo.UpdateSubscription(tranxRef);
                    }
                    

                    return RedirectToAction("Success");
                }
                else
                {
                    return RedirectToAction("error");
                }

            }

            return RedirectToAction("error");
           
        }


        [Route("payment/success")]
        public ActionResult Success()
        {
            return View();
        }


        [Route("subscription")]
        public ActionResult Subscription()
        {

            var cartCode = Helpers.GetCartId();
            var cartItem = repo.GetCartItem(cartCode);
            if (cartItem != null)
            {
                ViewBag.planName = cartItem.planName;
                ViewBag.formattedPrice = cartItem.amount.ToString("#,##.00");
            }

            return View();
        }


        public async Task<ActionResult> InitTransaction()
        {

            var customerDetail = new CustomerViewModel()
            {
                email = Request.Form.Get("cust_email"),
                firstName = Request.Form.Get("cust_fname"),
                lastName = Request.Form.Get("cust_lname"),
                phone = Request.Form.Get("cust_phone")
            };

            var cartCode = Helpers.GetCartId();
            var cartItem = repo.GetCartItem(cartCode);

            var paystackSec_Key = ConfigurationManager.AppSettings["PayStackKey"].ToString();

            var payStackAPI = new PaystackTransaction(paystackSec_Key);

            var amount = (Convert.ToInt32(cartItem.amount) * 100);

            var reqBody = new Paystack.Net.Models.TransactionInitializationRequestModel()
            {
                email = customerDetail.email,
                firstName = customerDetail.firstName,
                lastName = customerDetail.lastName,
                amount = amount
            };

            //Charge Customer

            var initResponse = await payStackAPI.InitializeTransaction(reqBody);

            if (initResponse.status)
            {
                //Create Order and customer
                var custCode = Helpers.GenerateRandomDigitCode(10);

                if (repo.CreateCustomerSubscription(customerDetail.firstName, customerDetail.lastName, customerDetail.email,
                    customerDetail.phone, custCode, cartItem.planName, cartItem.amount, initResponse.data.reference,cartItem.planId))
                {
                    repo.RemoveItemFromCart(cartCode);
                    Response.AddHeader("Access-Control-Allow-Origin", "*");
                    Response.AppendHeader("Access-Control-Allow-Origin", "*");
                    Response.Redirect(initResponse.data.authorization_url);
                }


            }
            
            return View("OrderError");




        }


        //public async Task<ActionResult> InitTransaction()
        //{

        //    var customerDetail = new CustomerViewModel()
        //    {
        //        email = Request.Form.Get("cust_email"),
        //        firstName = Request.Form.Get("cust_fname"),
        //        lastName = Request.Form.Get("cust_lname"),
        //        phone = Request.Form.Get("cust_phone")
        //    };

        //    var cartCode = Helpers.GetCartId();
        //    var cartItem = repo.GetCartItem(cartCode);

        //    var paystackSec_Key = ConfigurationManager.AppSettings["PayStackKey"].ToString();

        //    var payStackAPI = new Paystack.Net.SDK.Transactions.PaystackTransaction(paystackSec_Key);

        //    var amount = (Convert.ToInt32(cartItem.amount) * 100);

        //    var reqBody = new Paystack.Net.Models.TransactionInitializationRequestModel()
        //    {
        //        email = customerDetail.email,
        //        firstName = customerDetail.firstName,
        //        lastName = customerDetail.lastName,
        //        amount = amount,
        //        plan = cartItem.planId
        //    };

        //    var payStackCust = new Paystack.Net.SDK.Customers.PaystackCustomers(paystackSec_Key);

        //    var custCreate = await payStackCust.CreateCustomer(reqBody.email, reqBody.firstName, reqBody.lastName, customerDetail.phone);

        //    if (custCreate.status)
        //    {
        //        var payStckSubscription = new Paystack.Net.SDK.Subscription.PaystackSubscription(paystackSec_Key);
        //        var subscriptionResponse = await payStckSubscription.CreateSubscription(custCreate.data.email, reqBody.plan, "", "");

        //        if (subscriptionResponse.status)
        //        {
        //            var data = subscriptionResponse.data;
        //        }
        //    }

        //    return null;

        //    var initResponse = await payStackAPI.InitializeTransaction(reqBody);



        //    if (initResponse.status)
        //    {
        //        //Create Order and customer
        //        var custCode = Helpers.GenerateRandomDigitCode(10);

        //        if (repo.CreateCustomerSubscription(customerDetail.firstName, customerDetail.lastName, customerDetail.email, 
        //            customerDetail.phone, custCode, cartItem.planName, cartItem.amount, initResponse.data.reference))
        //        {
        //            Response.AddHeader("Access-Control-Allow-Origin", "*");
        //            Response.AppendHeader("Access-Control-Allow-Origin", "*");
        //            Response.Redirect(initResponse.data.authorization_url);
        //        }


        //    }

        //    return View("OrderError");




        //}
    }
}