using Orchard.Environment.Extensions;
using Orchard.Mvc.Routes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace LccNetwork
{
    [OrchardFeature("StaticPages")]
    public class Routes : IRouteProvider
    {
        public void GetRoutes(ICollection<RouteDescriptor> routes)
        {
            foreach (var routeDescriptor in GetRoutes())
            {
                routes.Add(routeDescriptor);
            }
        }

        public IEnumerable<RouteDescriptor> GetRoutes()
        {
            return new[] { 
                new RouteDescriptor {
                    Priority = 5, 
                    Route = new Route(
                        // url
                        "find-an-lcc",  
                        // defaults
                        new RouteValueDictionary { 
                            { "area", "LccNetwork" }, 
                            { "controller", "StaticPages" }, 
                            { "action", "FindAnLcc" } },
                        // constraints
                        new RouteValueDictionary(),     
                        // data tokens
                        new RouteValueDictionary {
                            {"area", "LccNetwork" }, 
                        },     
                        // route handler
                        new MvcRouteHandler())          
                }
            };
        }
    }
}