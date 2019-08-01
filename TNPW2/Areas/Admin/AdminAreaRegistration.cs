using System.Web.Mvc;

namespace TNPW2_video_2_ASP_MVC.Areas.Admin
{
    public class AdminAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Admin";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) => context.MapRoute(
                "Admin_default",
                "Admin/{controller}/{action}/{id}",
                new { ćontroller = "Home", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "TNPW2_video_2_ASP_MVC.Areas.Admin.Controllers" }

            );
    }
}