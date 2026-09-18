using System.Data.Entity;
using System.Web.Mvc;
using System.Web.Optimization;
using ITSB.AccidentDashboard.Core;

namespace ITSB.AccidentDashboard.Web
{
    // EHS Portal area for the ITSB Accident Dashboard.
    // URLs live under /ACCIDENT/..., default page is the Dashboard.
    public class ACCIDENTAreaRegistration : AreaRegistration
    {
        public override string AreaName => "ACCIDENT";

        public override void RegisterArea(AreaRegistrationContext context)
        {
            // Schema is managed with EF migrations (Update-Database); never let EF create/alter tables on its own.
            Database.SetInitializer<EshDbContext>(null);

            // The area keeps its own copy of jQuery/Bootstrap/Modernizr so the portal's bundles stay untouched.
            var bundles = BundleTable.Bundles;
            bundles.Add(new ScriptBundle("~/bundles/accident/jquery").Include(
                "~/Areas/ACCIDENT/Scripts/jquery-{version}.js"));
            bundles.Add(new ScriptBundle("~/bundles/accident/jqueryval").Include(
                "~/Areas/ACCIDENT/Scripts/jquery.validate*"));
            bundles.Add(new ScriptBundle("~/bundles/accident/modernizr").Include(
                "~/Areas/ACCIDENT/Scripts/modernizr-*"));
            bundles.Add(new Bundle("~/bundles/accident/bootstrap").Include(
                "~/Areas/ACCIDENT/Scripts/bootstrap.js"));
            bundles.Add(new StyleBundle("~/Content/accident/css").Include(
                "~/Areas/ACCIDENT/Content/bootstrap.css",
                "~/Areas/ACCIDENT/Content/Site.css"));

            context.MapRoute(
                "ACCIDENT_default",
                "ACCIDENT/{controller}/{action}/{id}",
                new { controller = "Dashboard", action = "Index", id = UrlParameter.Optional },
                new[] { "ITSB.AccidentDashboard.Web.Controllers" }
            );
        }
    }
}
