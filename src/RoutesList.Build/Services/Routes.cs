using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using RoutesList.Interfaces;
using RoutesList.Build.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using RoutesList.Services.RoutesBuilder;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RoutesList.Services
{
    public class Routes : IRoutes
    {
        public IEnumerable<ActionDescriptor> getRoutes(IActionDescriptorCollectionProvider collectionProvider)
        {
            if (collectionProvider != null) {
                IEnumerable<ActionDescriptor> routes = collectionProvider
                    .ActionDescriptors
                    .Items;

                return routes;
            }

            return Enumerable.Empty<ActionDescriptor>();
        }

        public IList<RoutesInformationModel> getRoutesInformation(IActionDescriptorCollectionProvider collectionProvider)
        {
            IList<RoutesInformationModel> routes = new List<RoutesInformationModel>();

            int id = 1;
            var items = getRoutes(collectionProvider);
            foreach (ActionDescriptor route in items) {
                string controllerName = String.Empty;
                string actionName = String.Empty;
                string displayName = String.Empty;
                string template = String.Empty;
                string methodName = String.Empty;
                string viewEnginePath = String.Empty;
                string relativePath = String.Empty;

                Builder builder = new Builder().Create(id);

                if (IsCompiledPageDescriptor(route)) {
                    var compiledPageActionDescriptor = items.OfType<CompiledPageActionDescriptor>()
                        .Where(r => r.Id == route.Id)
                        .Select(a => new {
                            a.DisplayName,
                            a.ViewEnginePath,
                            a.RelativePath
                        }).First();
                    

                    viewEnginePath = (string)compiledPageActionDescriptor?.ViewEnginePath;
                    relativePath = (string)compiledPageActionDescriptor?.RelativePath;
                    displayName = (string)compiledPageActionDescriptor?.DisplayName;

                    builder.IsCompiledpageActionDescriptior(true);
                } 

                if (IsControllerActionDescriptio(route)) { 
                    controllerName = route.RouteValues.Where(value => value.Key == "controller").First().Value;
                    actionName = route.RouteValues.Where(value => value.Key == "action").First().Value;
                    displayName = route.DisplayName;
                    template = route.AttributeRouteInfo?.Template;
                    methodName = route.ActionConstraints?.OfType<HttpMethodActionConstraint>()?.SingleOrDefault()?.HttpMethods?.First<string>();
                }

                if (!String.IsNullOrEmpty(controllerName)) {
                    builder.ControllerName(controllerName);
                }

                if (!String.IsNullOrEmpty(actionName)) {
                    builder.ActionName(actionName);
                }

                if (!String.IsNullOrEmpty(displayName)) {
                    builder.DisplayName(displayName);
                }

                if (!String.IsNullOrEmpty(template)) {
                    builder.Template(template);
                }

                if (!String.IsNullOrEmpty(methodName)) {
                    builder.MethodName(methodName);
                }

                if (!String.IsNullOrEmpty(viewEnginePath)) {
                    builder.ViewEnginePath(viewEnginePath);
                }

                if (!String.IsNullOrEmpty(relativePath)) {
                    builder.RelativePath(relativePath);
                }

                RoutesInformationModel model = builder.build();
                routes.Add(model);
            }

            return routes;
        }

        private bool IsCompiledPageDescriptor(ActionDescriptor actionDescriptor)
        {
            return actionDescriptor.GetType().FullName == "Microsoft.AspNetCore.Mvc.RazorPages.CompiledPageActionDescriptor";
        }

        private bool IsControllerActionDescriptio(ActionDescriptor actionDescriptor)
        {
            return actionDescriptor.GetType().FullName == "Microsoft.AspNetCore.Mvc.Controllers.ControllerActionDescriptor";
        }
    }
}
