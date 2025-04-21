using RoutesList.Build.Models;

namespace RoutesList.Build.Services.RoutesBuilder
{
    /// <summary>
    /// Builder for RoutesInformationModel 
    /// <see cref="RoutesInformationModel"/>
    /// </summary>
    public class Builder : IBuilder
    {
#if NET5_0_OR_GREATER
        private readonly RoutesInformationModel _model = new();
#endif
#if NETCOREAPP3_1
        private readonly RoutesInformationModel _model = new RoutesInformationModel();
#endif

        /// <summary>
        /// Adding the id of the line from which to start counting to the builder. has automatic value increments
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IBuilder Create(
            int? id
        )
        {
            int routeId = 0;

            if (!id.HasValue) {
                routeId += 1;
            } else {
                routeId += id.Value;
            }

            _model.Id = routeId;

            return this;
        }

        /// <summary>
        /// Add Controller name to builder
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public IBuilder ControllerName(string name)
        {
            _model.ControllerName = name;
            return this;
        }

        /// <summary>
        /// Add Action Name to builder
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public IBuilder ActionName(string name)
        {
            _model.ActionName = name;
            return this;
        }

        /// <summary>
        /// Add Display name from controller to builder
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public IBuilder DisplayName(string name)
        {
            _model.DisplayName = name;
            return this;
        }

        /// <summary>
        /// Add method name from controller ActionConstraints to builder
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public IBuilder MethodName(string name)
        {
            _model.MethodName = name;
            return this;
        }

        /// <summary>
        /// Add Template from controller AttributeRouteInfo to builder
        /// </summary>
        /// <param name="templateName"></param>
        /// <returns></returns>
        public IBuilder Template(string templateName)
        {
            _model.Template = templateName;
            return this;
        }

        /// <summary>
        /// Add View Engine Path from razor pages to builder
        /// </summary>
        /// <param name="enginePath"></param>
        /// <returns></returns>
        public IBuilder ViewEnginePath(string enginePath)
        {
            _model.ViewEnginePath = enginePath;
            return this;
        }

        /// <summary>
        /// Add Relative Path from razor pages to builder
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public IBuilder RelativePath(string path)
        {
            _model.RelativePath = path;
            return this;
        }

        /// <summary>
        /// Sets the flag indicating whether the page action descriptor is compiled.
        /// </summary>
        /// <param name="isCompiled">A boolean value indicating if the page action descriptor is compiled.</param>
        /// <returns>An instance of the builder with the specified compiled setting applied.</returns>
        public IBuilder IsCompiledPageActionDescriptior(bool isCompiled)
        {
            _model.IsCompiledPageActionDescriptor = isCompiled;
            return this;
        }

        /// <summary>
        /// Started to build object
        /// </summary>
        /// <returns></returns>
        public RoutesInformationModel Build()
        {
            return _model;
        }
    }
}
