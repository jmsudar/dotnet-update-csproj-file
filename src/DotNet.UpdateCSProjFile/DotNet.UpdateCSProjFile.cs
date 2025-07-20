using System.Collections.Generic;
using jmsudar.UpdateCSProj.Object;
using XML = jmsudar.DotNet.Xml.XML;

namespace jmsudar.UpdateCSProj
{
    /// <summary>
    /// Accepts arguments representing .csproj PropertyGroup values, and
    /// reads from a file path, assumed to be the path of a .csproj file,
    /// constructing a PropertyGroup object with the provided arguments,
    /// writing the new PropertyGroup to the filepath
    /// </summary>
    public static class UpdateCSProj
    {
        public static void Main(string[] args)
        {
            var arguments = ArgsParser.ParseArgs(args);

            // Reads from the .csproj file, processes the block
            // and writes it back to the original location. For more info, see
            // https://github.com/jmsudar/DotNet.Xml
            XML.ProcessXmlBlockFromFile<PropertyGroup>(arguments["filePath"], propertyGroup =>
            {
                if (propertyGroup == null)
                {
                    propertyGroup = new PropertyGroup();
                }

                propertyGroup.TargetFramework = arguments.GetValueOrDefault("targetFramework");
                propertyGroup.ImplicitUsings = arguments.GetValueOrDefault("implicitUsings");
                propertyGroup.Nullable = arguments.GetValueOrDefault("nullable");
                propertyGroup.PackageId = arguments.GetValueOrDefault("packageId");
                propertyGroup.Version = arguments.GetValueOrDefault("version");
                propertyGroup.Authors = arguments.GetValueOrDefault("authors");
                propertyGroup.Description = arguments.GetValueOrDefault("description");
                propertyGroup.PackageTags = arguments.GetValueOrDefault("packageTags");
                propertyGroup.RepositoryUrl = arguments.GetValueOrDefault("repositoryUrl");
                propertyGroup.PackageLicenseExpression = arguments.GetValueOrDefault("packageLicenseExpression");
                propertyGroup.PackageProjectUrl = arguments.GetValueOrDefault("packageProjectUrl");
                propertyGroup.PackageReadmeFile = arguments.GetValueOrDefault("packageReadmeFile");
            });
        }
    }
    
    /// <summary>
    /// Parses command-line arguments and returns a dictionary
    /// of their keys and values
    /// </summary>
    public static class ArgsParser
    {
        // TODO: Migrate this out into its own class, project, and repo

        /// <summary>
        /// Parses command-line arguments
        /// </summary>
        /// <param name="args">The array of arguments passed to main</param>
        /// <returns>A dictionary of argument key value pairs</returns>
        public static Dictionary<string, string> ParseArgs(string[] args)
        {
            var parsedArgs = new Dictionary<string, string>();

            foreach (var arg in args)
            {
                var parts = arg.Split('=', 2);
                if (parts.Length == 2)
                {
                    var key = parts[0].TrimStart('-');
                    var value = parts[1];
                    parsedArgs[key] = value;
                }
            }

            return parsedArgs;
        }
    }
}
