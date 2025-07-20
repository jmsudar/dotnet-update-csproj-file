using jmsudar.UpdateCSProj.Interface;

namespace jmsudar.UpdateCSProj.Object
{
    /// <summary>
    /// Represents the PropertyGroup block in a .csproj file
    /// </summary>
    public class PropertyGroup : IPropertyGroup
    {
        /// <summary>
        /// Parameterless constructor for XML serialization
        /// </summary>
        public PropertyGroup() { }

        /// <summary>
        /// Constructs a PropertyGroup, defaulting empty values to null
        /// </summary>
        /// <param name="targetFramework">The .NET Framework in use</param>
        /// <param name="implicitUsings">Whether or not the project uses
        /// implicit usings</param>
        /// <param name="nullable">Whether or not the project allows nullable
        /// fields</param>
        /// <param name="packageId">The package ID of the project</param>
        /// <param name="version">The current version of the project</param>
        /// <param name="authors">The project authors</param>
        /// <param name="description">The project description</param>
        /// <param name="packageTags">The project's package tags</param>
        /// <param name="repositoryUrl">The project's repo URL</param>
        /// <param name="packageLicenseExpression">The license for the
        /// project</param>
        /// <param name="packageProjectUrl">The package URL for the
        /// project</param>
        /// <param name="packageReadmeFile">The path to the project's
        /// README file</param>
        public PropertyGroup(
            string? targetFramework = null,
            string? implicitUsings = null,
            string? nullable = null,
            string? packageId = null,
            string? version = null,
            string? authors = null,
            string? description = null,
            string? packageTags = null,
            string? repositoryUrl = null,
            string? packageLicenseExpression = null,
            string? packageProjectUrl = null,
            string? packageReadmeFile = null)
        {
            TargetFramework = targetFramework.NullIfEmpty();
            ImplicitUsings = implicitUsings.NullIfEmpty();
            Nullable = nullable.NullIfEmpty();
            PackageId = packageId.NullIfEmpty();
            Version = version.NullIfEmpty();
            Authors = authors.NullIfEmpty();
            Description = description.NullIfEmpty();
            PackageTags = packageTags.NullIfEmpty();
            RepositoryUrl = repositoryUrl.NullIfEmpty();
            PackageLicenseExpression = packageLicenseExpression.NullIfEmpty();
            PackageProjectUrl = packageProjectUrl.NullIfEmpty();
            PackageReadmeFile = packageReadmeFile.NullIfEmpty();
        }

        public string? TargetFramework { get; set; } = null;
        public string? ImplicitUsings { get; set; } = null;
        public string? Nullable { get; set; } = null;
        public string? PackageId { get; set; } = null;
        public string? Version { get; set; } = null;
        public string? Authors { get; set; } = null;
        public string? Description { get; set; } = null;
        public string? PackageTags { get; set; } = null;
        public string? RepositoryUrl { get; set; } = null;
        public string? PackageLicenseExpression { get; set; } = null;
        public string? PackageProjectUrl { get; set; } = null;
        public string? PackageReadmeFile { get; set; } = null;
    }

    /// <summary>
    /// Provides extensions for manipulating string objects
    /// </summary>
    public static class StringExtensions
    {
        // TODO: Move this to its own repo and project

        /// <summary>
        /// Returns a null value if a string is empty
        /// </summary>
        /// <param name="value">The string being assessed</param>
        /// <returns>Either a null or string value</returns>
        public static string? NullIfEmpty(this string? value)
        {
            return string.IsNullOrEmpty(value) ? null : value;
        }
    }
}

