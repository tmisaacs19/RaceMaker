﻿using Stripe;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace RaceMaker
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            StripeConfiguration.SetApiKey(ConfigurationManager.AppSettings["sk_test_8PG9hZj2NTvZQms85NOEeh83"]);
            StripeConfiguration.SetApiKey("sk_test_8PG9hZj2NTvZQms85NOEeh83");
        }
    }
}
