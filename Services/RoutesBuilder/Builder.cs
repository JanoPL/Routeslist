using RoutesList.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RoutesList.Services.RoutesBuilder
{
    public class Builder
    {
        private RoutesInformationModel _model;

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

        public Builder ControllerName(string controllerName)
        {
            _model.Controller_name = controllerName;
            return this;
        }

        public Builder ActionName(string actionName)
        {
            _model.Action_name = actionName;
            return this;
        }

        public Builder DisplayName(string displayName)
        {
            _model.Display_name = displayName;  
            return this;
        }

        public Builder MethodName(string methodName)
        {
            _model.Method_name = methodName;
            return this;
        }

        public Builder Template(string template)
        {
            _model.Template = template;
            return this;
        }

        public RoutesInformationModel build()
        {
            return _model;
        }
    }
}
