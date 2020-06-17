using RoutesList_cli.Resources;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace RoutesList_cli.Internal
{
    internal class MsBuildProject
    {

        public static string FindProjectFile(string[] args)
        {
            string projectFilePath = string.Empty;

            if (Directory.Exists(args[0]))
            {
                List<string> projectsPath = Directory.EnumerateFileSystemEntries(args[0], "*.*proj", SearchOption.TopDirectoryOnly).ToList();
                projectFilePath = projectsPath.FirstOrDefault();
            } 

            if (projectFilePath == null)
            {
                throw new FileNotFoundException(ResourceProject.Error_NoProjectsFound(args[0]));
            }

            return projectFilePath;
        }
        public static string FindProjectName(string projectPath)
        {
            if (!File.Exists(projectPath))
            {
                throw new FileNotFoundException(ResourceProject.Error_ProjectPath_NotFound(projectPath));
            }

            string projectName = projectPath.Split("\\").Last().Split(".").FirstOrDefault();

            if (string.IsNullOrEmpty(projectName))
            {
                throw new FileNotFoundException(ResourceProject.Error_NoProjectNameFound(projectPath));
            }

            return projectName;
        }
    }
}
