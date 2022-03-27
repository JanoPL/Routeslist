using RoutesList.Build.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RoutesList.Services.RoutesBuilder
{
    /// <summary>
    /// Builder for RoutesInformationModel 
    /// <see cref="RoutesInformationModel"/>
    /// </summary>
    public class Builder
    {
        private RoutesInformationModel _model;

        /// <summary>
        /// Adding the id of the line from which to start counting to the builder. has automatic value increments
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Builder Create(
            int? id
        ) {
            int routeId = 0;

            if (!id.HasValue) {
                routeId += 1;
            } else {
                routeId += id.Value;
            }

            _model = new RoutesInformationModel() {
                Id = routeId,
            };
            
            return this;
        }

        /// <summary>
        /// Add Controller name to builder
        /// </summary>
        /// <param name="controllerName"></param>
        /// <returns></returns>
        public Builder ControllerName(string controllerName)
        {
            _model.Controller_name = controllerName;
            return this;
        }

        /// <summary>
        /// Add Action Name to builder
        /// </summary>
        /// <param name="actionName"></param>
        /// <returns></returns>
        public Builder ActionName(string actionName)
        {
            _model.Action_name = actionName;
            return this;
        }

        /// <summary>
        /// Add Display name from controller to builder
        /// </summary>
        /// <param name="displayName"></param>
        /// <returns></returns>
        public Builder DisplayName(string displayName)
        {
            _model.Display_name = displayName;  
            return this;
        }

        /// <summary>
        /// Add method name from controller ActionConstraints to builder
        /// </summary>
        /// <param name="methodName"></param>
        /// <returns></returns>
        public Builder MethodName(string methodName)
        {
            _model.Method_name = methodName;
            return this;
        }

        /// <summary>
        /// Add Template from controller AttributeRouteInfo to builder
        /// </summary>
        /// <param name="template"></param>
        /// <returns></returns>
        public Builder Template(string template)
        {
            _model.Template = template;
            return this;
        }

        /// <summary>
        /// Add View Engine Path from razor pages to builder
        /// </summary>
        /// <param name="viewEnginePath"></param>
        /// <returns></returns>
        public Builder ViewEnginePath(string viewEnginePath)
        {
            _model.ViewEnginePath = viewEnginePath;
            return this;
        }

        /// <summary>
        /// Add Relative Path from razor pages to builder
        /// </summary>
        /// <param name="relativePath"></param>
        /// <returns></returns>
        public Builder RelativePath(string relativePath)
        {
            _model.RelativePath = relativePath;
            return this;
        }

        public Builder IsCompiledpageActionDescriptior(bool IsCompiledpageActionDescriptior)
        {
            _model.IsCompiledPageActionDescriptor = IsCompiledpageActionDescriptior;
            return this;
        }

        /// <summary>
        /// Started to build object
        /// </summary>
        /// <returns></returns>
        public RoutesInformationModel build()
        {
            return _model;
        }
    }
}
