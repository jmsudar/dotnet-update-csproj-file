using System.Collections.Generic;
using jmsudar.UpdateCSProj;
using jmsudar.UpdateCSProj.Object;

namespace DotNet.UpdateCSProjFile.Tests
{
    [TestClass]
    public class ArgsParserTests
    {
        [TestMethod]
        public void ParseArgs_WithValidArguments_ReturnsCorrectDictionary()
        {
            // Arrange
            var args = new[] { "--targetFramework=net6.0", "--version=1.0.0", "--authors=John Doe" };

            // Act
            var result = ArgsParser.ParseArgs(args);

            // Assert
            Assert.AreEqual(3, result.Count);
            Assert.AreEqual("net6.0", result["targetFramework"]);
            Assert.AreEqual("1.0.0", result["version"]);
            Assert.AreEqual("John Doe", result["authors"]);
        }

        [TestMethod]
        public void ParseArgs_WithSingleDashPrefix_ReturnsCorrectDictionary()
        {
            // Arrange
            var args = new[] { "-targetFramework=net6.0", "-version=1.0.0" };

            // Act
            var result = ArgsParser.ParseArgs(args);

            // Assert
            Assert.AreEqual(2, result.Count);
            Assert.AreEqual("net6.0", result["targetFramework"]);
            Assert.AreEqual("1.0.0", result["version"]);
        }

        [TestMethod]
        public void ParseArgs_WithEmptyArray_ReturnsEmptyDictionary()
        {
            // Arrange
            var args = new string[0];

            // Act
            var result = ArgsParser.ParseArgs(args);

            // Assert
            Assert.AreEqual(0, result.Count);
        }

        [TestMethod]
        public void ParseArgs_WithArgumentsWithoutEquals_IgnoresInvalidArguments()
        {
            // Arrange
            var args = new[] { "--targetFramework=net6.0", "invalidArgument", "--version=1.0.0" };

            // Act
            var result = ArgsParser.ParseArgs(args);

            // Assert
            Assert.AreEqual(2, result.Count);
            Assert.AreEqual("net6.0", result["targetFramework"]);
            Assert.AreEqual("1.0.0", result["version"]);
            Assert.IsFalse(result.ContainsKey("invalidArgument"));
        }

        [TestMethod]
        public void ParseArgs_WithEmptyValues_ReturnsEmptyStringValues()
        {
            // Arrange
            var args = new[] { "--targetFramework=", "--version=1.0.0" };

            // Act
            var result = ArgsParser.ParseArgs(args);

            // Assert
            Assert.AreEqual(2, result.Count);
            Assert.AreEqual("", result["targetFramework"]);
            Assert.AreEqual("1.0.0", result["version"]);
        }

        [TestMethod]
        public void ParseArgs_WithValuesContainingSpaces_ReturnsCorrectValues()
        {
            // Arrange
            var args = new[] { "--authors=John Doe Smith", "--description=A test project for unit testing" };

            // Act
            var result = ArgsParser.ParseArgs(args);

            // Assert
            Assert.AreEqual(2, result.Count);
            Assert.AreEqual("John Doe Smith", result["authors"]);
            Assert.AreEqual("A test project for unit testing", result["description"]);
        }

        [TestMethod]
        public void ParseArgs_WithValuesContainingMultipleEquals_SplitsOnFirstEqualsOnly()
        {
            // Arrange
            var args = new[] { "--repositoryUrl=https://github.com/user/repo.git", "--description=Test=Description" };

            // Act
            var result = ArgsParser.ParseArgs(args);

            // Assert
            Assert.AreEqual(2, result.Count);
            Assert.AreEqual("https://github.com/user/repo.git", result["repositoryUrl"]);
            Assert.AreEqual("Test=Description", result["description"]);
        }

        [TestMethod]
        public void ParseArgs_WithDuplicateKeys_LastValueWins()
        {
            // Arrange
            var args = new[] { "--version=1.0.0", "--version=2.0.0" };

            // Act
            var result = ArgsParser.ParseArgs(args);

            // Assert
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual("2.0.0", result["version"]);
        }

        [TestMethod]
        public void ParseArgs_WithSpecialCharacters_ReturnsCorrectValues()
        {
            // Arrange
            var args = new[] { "--packageTags=test;unit;framework", "--description=Test & Development" };

            // Act
            var result = ArgsParser.ParseArgs(args);

            // Assert
            Assert.AreEqual(2, result.Count);
            Assert.AreEqual("test;unit;framework", result["packageTags"]);
            Assert.AreEqual("Test & Development", result["description"]);
        }

        [TestMethod]
        public void ParseArgs_WithAllSupportedProperties_ReturnsAllProperties()
        {
            // Arrange
            var args = new[]
            {
                "--targetFramework=net6.0",
                "--implicitUsings=enable",
                "--nullable=enable",
                "--packageId=TestPackage",
                "--version=1.0.0",
                "--authors=John Doe",
                "--description=Test Description",
                "--packageTags=test;unit",
                "--repositoryUrl=https://github.com/test/repo",
                "--packageLicenseExpression=MIT",
                "--packageProjectUrl=https://github.com/test/repo",
                "--packageReadmeFile=README.md"
            };

            // Act
            var result = ArgsParser.ParseArgs(args);

            // Assert
            Assert.AreEqual(12, result.Count);
            Assert.AreEqual("net6.0", result["targetFramework"]);
            Assert.AreEqual("enable", result["implicitUsings"]);
            Assert.AreEqual("enable", result["nullable"]);
            Assert.AreEqual("TestPackage", result["packageId"]);
            Assert.AreEqual("1.0.0", result["version"]);
            Assert.AreEqual("John Doe", result["authors"]);
            Assert.AreEqual("Test Description", result["description"]);
            Assert.AreEqual("test;unit", result["packageTags"]);
            Assert.AreEqual("https://github.com/test/repo", result["repositoryUrl"]);
            Assert.AreEqual("MIT", result["packageLicenseExpression"]);
            Assert.AreEqual("https://github.com/test/repo", result["packageProjectUrl"]);
            Assert.AreEqual("README.md", result["packageReadmeFile"]);
        }
    }

    [TestClass]
    public class PropertyGroupTests
    {
        [TestMethod]
        public void PropertyGroup_DefaultConstructor_AllPropertiesAreNull()
        {
            // Act
            var propertyGroup = new PropertyGroup();

            // Assert
            Assert.IsNull(propertyGroup.TargetFramework);
            Assert.IsNull(propertyGroup.ImplicitUsings);
            Assert.IsNull(propertyGroup.Nullable);
            Assert.IsNull(propertyGroup.PackageId);
            Assert.IsNull(propertyGroup.Version);
            Assert.IsNull(propertyGroup.Authors);
            Assert.IsNull(propertyGroup.Description);
            Assert.IsNull(propertyGroup.PackageTags);
            Assert.IsNull(propertyGroup.RepositoryUrl);
            Assert.IsNull(propertyGroup.PackageLicenseExpression);
            Assert.IsNull(propertyGroup.PackageProjectUrl);
            Assert.IsNull(propertyGroup.PackageReadmeFile);
        }

        [TestMethod]
        public void PropertyGroup_ParameterizedConstructor_WithValidValues_SetsProperties()
        {
            // Act
            var propertyGroup = new PropertyGroup(
                targetFramework: "net6.0",
                implicitUsings: "enable",
                nullable: "enable",
                packageId: "TestPackage",
                version: "1.0.0",
                authors: "John Doe",
                description: "Test Description",
                packageTags: "test;unit",
                repositoryUrl: "https://github.com/test/repo",
                packageLicenseExpression: "MIT",
                packageProjectUrl: "https://github.com/test/repo",
                packageReadmeFile: "README.md"
            );

            // Assert
            Assert.AreEqual("net6.0", propertyGroup.TargetFramework);
            Assert.AreEqual("enable", propertyGroup.ImplicitUsings);
            Assert.AreEqual("enable", propertyGroup.Nullable);
            Assert.AreEqual("TestPackage", propertyGroup.PackageId);
            Assert.AreEqual("1.0.0", propertyGroup.Version);
            Assert.AreEqual("John Doe", propertyGroup.Authors);
            Assert.AreEqual("Test Description", propertyGroup.Description);
            Assert.AreEqual("test;unit", propertyGroup.PackageTags);
            Assert.AreEqual("https://github.com/test/repo", propertyGroup.RepositoryUrl);
            Assert.AreEqual("MIT", propertyGroup.PackageLicenseExpression);
            Assert.AreEqual("https://github.com/test/repo", propertyGroup.PackageProjectUrl);
            Assert.AreEqual("README.md", propertyGroup.PackageReadmeFile);
        }

        [TestMethod]
        public void PropertyGroup_ParameterizedConstructor_WithEmptyStrings_SetsPropertiesToNull()
        {
            // Act
            var propertyGroup = new PropertyGroup(
                targetFramework: "",
                implicitUsings: "",
                nullable: "",
                packageId: "",
                version: "",
                authors: "",
                description: "",
                packageTags: "",
                repositoryUrl: "",
                packageLicenseExpression: "",
                packageProjectUrl: "",
                packageReadmeFile: ""
            );

            // Assert
            Assert.IsNull(propertyGroup.TargetFramework);
            Assert.IsNull(propertyGroup.ImplicitUsings);
            Assert.IsNull(propertyGroup.Nullable);
            Assert.IsNull(propertyGroup.PackageId);
            Assert.IsNull(propertyGroup.Version);
            Assert.IsNull(propertyGroup.Authors);
            Assert.IsNull(propertyGroup.Description);
            Assert.IsNull(propertyGroup.PackageTags);
            Assert.IsNull(propertyGroup.RepositoryUrl);
            Assert.IsNull(propertyGroup.PackageLicenseExpression);
            Assert.IsNull(propertyGroup.PackageProjectUrl);
            Assert.IsNull(propertyGroup.PackageReadmeFile);
        }

        [TestMethod]
        public void PropertyGroup_ParameterizedConstructor_WithNullValues_SetsPropertiesToNull()
        {
            // Act
            var propertyGroup = new PropertyGroup(
                targetFramework: null,
                implicitUsings: null,
                nullable: null,
                packageId: null,
                version: null,
                authors: null,
                description: null,
                packageTags: null,
                repositoryUrl: null,
                packageLicenseExpression: null,
                packageProjectUrl: null,
                packageReadmeFile: null
            );

            // Assert
            Assert.IsNull(propertyGroup.TargetFramework);
            Assert.IsNull(propertyGroup.ImplicitUsings);
            Assert.IsNull(propertyGroup.Nullable);
            Assert.IsNull(propertyGroup.PackageId);
            Assert.IsNull(propertyGroup.Version);
            Assert.IsNull(propertyGroup.Authors);
            Assert.IsNull(propertyGroup.Description);
            Assert.IsNull(propertyGroup.PackageTags);
            Assert.IsNull(propertyGroup.RepositoryUrl);
            Assert.IsNull(propertyGroup.PackageLicenseExpression);
            Assert.IsNull(propertyGroup.PackageProjectUrl);
            Assert.IsNull(propertyGroup.PackageReadmeFile);
        }

        [TestMethod]
        public void PropertyGroup_PropertySetters_WithValidValues_SetsProperties()
        {
            // Arrange
            var propertyGroup = new PropertyGroup();

            // Act
            propertyGroup.TargetFramework = "net6.0";
            propertyGroup.ImplicitUsings = "enable";
            propertyGroup.Nullable = "enable";
            propertyGroup.PackageId = "TestPackage";
            propertyGroup.Version = "1.0.0";
            propertyGroup.Authors = "John Doe";
            propertyGroup.Description = "Test Description";
            propertyGroup.PackageTags = "test;unit";
            propertyGroup.RepositoryUrl = "https://github.com/test/repo";
            propertyGroup.PackageLicenseExpression = "MIT";
            propertyGroup.PackageProjectUrl = "https://github.com/test/repo";
            propertyGroup.PackageReadmeFile = "README.md";

            // Assert
            Assert.AreEqual("net6.0", propertyGroup.TargetFramework);
            Assert.AreEqual("enable", propertyGroup.ImplicitUsings);
            Assert.AreEqual("enable", propertyGroup.Nullable);
            Assert.AreEqual("TestPackage", propertyGroup.PackageId);
            Assert.AreEqual("1.0.0", propertyGroup.Version);
            Assert.AreEqual("John Doe", propertyGroup.Authors);
            Assert.AreEqual("Test Description", propertyGroup.Description);
            Assert.AreEqual("test;unit", propertyGroup.PackageTags);
            Assert.AreEqual("https://github.com/test/repo", propertyGroup.RepositoryUrl);
            Assert.AreEqual("MIT", propertyGroup.PackageLicenseExpression);
            Assert.AreEqual("https://github.com/test/repo", propertyGroup.PackageProjectUrl);
            Assert.AreEqual("README.md", propertyGroup.PackageReadmeFile);
        }
    }

    [TestClass]
    public class StringExtensionsTests
    {
        [TestMethod]
        public void NullIfEmpty_WithNullString_ReturnsNull()
        {
            // Arrange
            string? value = null;

            // Act
            var result = value.NullIfEmpty();

            // Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public void NullIfEmpty_WithEmptyString_ReturnsNull()
        {
            // Arrange
            string value = "";

            // Act
            var result = value.NullIfEmpty();

            // Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public void NullIfEmpty_WithWhitespaceString_ReturnsOriginalString()
        {
            // Arrange
            string value = "   ";

            // Act
            var result = value.NullIfEmpty();

            // Assert
            Assert.AreEqual("   ", result);
        }

        [TestMethod]
        public void NullIfEmpty_WithValidString_ReturnsOriginalString()
        {
            // Arrange
            string value = "test value";

            // Act
            var result = value.NullIfEmpty();

            // Assert
            Assert.AreEqual("test value", result);
        }

        [TestMethod]
        public void NullIfEmpty_WithSingleCharacterString_ReturnsOriginalString()
        {
            // Arrange
            string value = "a";

            // Act
            var result = value.NullIfEmpty();

            // Assert
            Assert.AreEqual("a", result);
        }
    }

    [TestClass]
    public class UpdateCSProjIntegrationTests
    {
        [TestMethod]
        public void ArgsParser_Integration_WithRealWorldArguments_ParsesCorrectly()
        {
            // Arrange - Simulate real command line arguments
            var args = new[]
            {
                "--filePath=./test.csproj",
                "--targetFramework=net6.0",
                "--implicitUsings=enable",
                "--nullable=enable",
                "--packageId=MyTestPackage",
                "--version=1.2.3",
                "--authors=John Doe, Jane Smith",
                "--description=A comprehensive test package for demonstration purposes",
                "--packageTags=test;demo;sample;unit-testing",
                "--repositoryUrl=https://github.com/myorg/myrepo.git",
                "--packageLicenseExpression=MIT",
                "--packageProjectUrl=https://github.com/myorg/myrepo",
                "--packageReadmeFile=docs/README.md"
            };

            // Act
            var parsedArgs = ArgsParser.ParseArgs(args);

            // Assert
            Assert.AreEqual(13, parsedArgs.Count);
            Assert.AreEqual("./test.csproj", parsedArgs["filePath"]);
            Assert.AreEqual("net6.0", parsedArgs["targetFramework"]);
            Assert.AreEqual("enable", parsedArgs["implicitUsings"]);
            Assert.AreEqual("enable", parsedArgs["nullable"]);
            Assert.AreEqual("MyTestPackage", parsedArgs["packageId"]);
            Assert.AreEqual("1.2.3", parsedArgs["version"]);
            Assert.AreEqual("John Doe, Jane Smith", parsedArgs["authors"]);
            Assert.AreEqual("A comprehensive test package for demonstration purposes", parsedArgs["description"]);
            Assert.AreEqual("test;demo;sample;unit-testing", parsedArgs["packageTags"]);
            Assert.AreEqual("https://github.com/myorg/myrepo.git", parsedArgs["repositoryUrl"]);
            Assert.AreEqual("MIT", parsedArgs["packageLicenseExpression"]);
            Assert.AreEqual("https://github.com/myorg/myrepo", parsedArgs["packageProjectUrl"]);
            Assert.AreEqual("docs/README.md", parsedArgs["packageReadmeFile"]);
        }

        [TestMethod]
        public void PropertyGroup_Integration_WithParsedArguments_CreatesCorrectObject()
        {
            // Arrange
            var arguments = new Dictionary<string, string>
            {
                ["targetFramework"] = "net6.0",
                ["implicitUsings"] = "enable",
                ["nullable"] = "enable",
                ["packageId"] = "TestPackage",
                ["version"] = "1.0.0",
                ["authors"] = "Test Author",
                ["description"] = "Test Description",
                ["packageTags"] = "test;unit",
                ["repositoryUrl"] = "https://github.com/test/repo",
                ["packageLicenseExpression"] = "MIT",
                ["packageProjectUrl"] = "https://github.com/test/repo",
                ["packageReadmeFile"] = "README.md"
            };

            // Act - Simulate the property assignment logic from Main method
            var propertyGroup = new PropertyGroup();
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

            // Assert
            Assert.AreEqual("net6.0", propertyGroup.TargetFramework);
            Assert.AreEqual("enable", propertyGroup.ImplicitUsings);
            Assert.AreEqual("enable", propertyGroup.Nullable);
            Assert.AreEqual("TestPackage", propertyGroup.PackageId);
            Assert.AreEqual("1.0.0", propertyGroup.Version);
            Assert.AreEqual("Test Author", propertyGroup.Authors);
            Assert.AreEqual("Test Description", propertyGroup.Description);
            Assert.AreEqual("test;unit", propertyGroup.PackageTags);
            Assert.AreEqual("https://github.com/test/repo", propertyGroup.RepositoryUrl);
            Assert.AreEqual("MIT", propertyGroup.PackageLicenseExpression);
            Assert.AreEqual("https://github.com/test/repo", propertyGroup.PackageProjectUrl);
            Assert.AreEqual("README.md", propertyGroup.PackageReadmeFile);
        }

        [TestMethod]
        public void PropertyGroup_Integration_WithMissingArguments_HandlesNullValues()
        {
            // Arrange
            var arguments = new Dictionary<string, string>
            {
                ["targetFramework"] = "net6.0",
                ["version"] = "1.0.0"
                // Other properties intentionally missing
            };

            // Act - Simulate the property assignment logic from Main method
            var propertyGroup = new PropertyGroup();
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

            // Assert
            Assert.AreEqual("net6.0", propertyGroup.TargetFramework);
            Assert.IsNull(propertyGroup.ImplicitUsings);
            Assert.IsNull(propertyGroup.Nullable);
            Assert.IsNull(propertyGroup.PackageId);
            Assert.AreEqual("1.0.0", propertyGroup.Version);
            Assert.IsNull(propertyGroup.Authors);
            Assert.IsNull(propertyGroup.Description);
            Assert.IsNull(propertyGroup.PackageTags);
            Assert.IsNull(propertyGroup.RepositoryUrl);
            Assert.IsNull(propertyGroup.PackageLicenseExpression);
            Assert.IsNull(propertyGroup.PackageProjectUrl);
            Assert.IsNull(propertyGroup.PackageReadmeFile);
        }
    }
}
