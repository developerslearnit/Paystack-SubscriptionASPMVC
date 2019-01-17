using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PaystackDirectDebit.AppHelper
{
    public class Helpers
    {
        public static string GetCartId()
        {

            if (HttpContext.Current.Request.Cookies["CartCode"] == null)
            {

                Guid tempCartId = Guid.NewGuid();
                var aCookie = new HttpCookie("CartCode")
                {
                    Value = tempCartId.ToString(),
                    Expires = DateTime.Now.AddDays(4)
                };
                HttpContext.Current.Response.Cookies.Add(aCookie);

            }

            return HttpContext.Current.Request.Cookies["CartCode"].Value.ToString();

        }

        public static string GenerateRandomDigitCode(int length)
        {
            var random = new Random();
            string str = string.Empty;
            for (int i = 0; i < length; i++)
                str = String.Concat(str, random.Next(10).ToString());
            return str;
        }
    }
}