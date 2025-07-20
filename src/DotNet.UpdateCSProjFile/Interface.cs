namespace jmsudar.UpdateCSProj.Interface
{
    /// <summary>
    /// Provides the interface for the PropertyGroup block in a .csproj file
    /// </summary>
	public interface IPropertyGroup
    {
		string? TargetFramework { get; set; }
        string? ImplicitUsings { get; set; }
        string? Nullable { get; set; }
        string? PackageId { get; set; }
        string? Version { get; set; }
        string? Authors { get; set; }
        string? Description { get; set; }
        string? PackageTags { get; set; }
        string? RepositoryUrl { get; set; }
        string? PackageLicenseExpression { get; set; }
        string? PackageProjectUrl { get; set; }
        string? PackageReadmeFile { get; set; }
    }
}