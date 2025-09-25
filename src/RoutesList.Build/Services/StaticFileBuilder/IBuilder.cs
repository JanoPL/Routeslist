using ConsoleTables;
using RoutesList.Build.Models;

namespace RoutesList.Build.Services.StaticFileBuilder
{
    /// <summary>
    /// Interface for building static file output from route information.
    /// </summary>
    public interface IBuilder
    {
        /// <summary>
        /// Builds the output using the provided route table and options.
        /// </summary>
        /// <param name="table">The console table containing route information.</param>
        /// <param name="options">The options for customizing the output.</param>
        void Build(ConsoleTable table, RoutesListOptions options);

        /// <summary>
        /// Gets or sets the resulting output after the build process.
        /// </summary>
        string Result { get; set; }
    }
}
