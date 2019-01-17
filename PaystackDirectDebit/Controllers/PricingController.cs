using App.Repo;
using App.Repo.DTO;
using PaystackDirectDebit.AppHelper;
using System;
using System.Web;
using System.Web.Mvc;

namespace PaystackDirectDebit.Controllers
{
    [RoutePrefix("pricing")]
    public class PricingController : Controller
    {
        IAppRepository repo;
        public PricingController(IAppRepository _repo)
        {
            repo = _repo;
        }

        public ActionResult Index()
        {
            return View();
        }


        public JsonResult AddToCart(CartDTO cartItem)
        {
            var exMessage = string.Empty;
            try
            {
                cartItem.cartCode = Helpers.GetCartId();

                if (repo.AddToCart(cartItem))
                {
                    return Json(new { error = false });
                }
            }
            catch (Exception ex)
            {
                exMessage = ex.Message;
            }

            return Json(new { error = true, message = exMessage });
        }

        



    }
}