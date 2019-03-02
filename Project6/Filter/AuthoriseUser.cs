using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace Project6.Filter
{
    public class AuthoriseUser:AuthorizeAttribute,IAuthorizationFilter
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {

            // Check for authorization  
            if (HttpContext.Current.Session["UserName"] == null && HttpContext.Current.Session["Role"] == null)
            {
                filterContext.Result = new RedirectResult("~/User/Login");
            }
        }
    }
}

    
