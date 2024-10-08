using Microsoft.VisualStudio.TestTools.UnitTesting;
using jmsudar.UpdateCSProj;

[TestClass]
public class UpdateCSProjTests
{
    private const string TestCsprojContent = @"
    <Project Sdk=""Microsoft.NET.Sdk"">
        <PropertyGroup>
        <TargetFramework>net6.0</TargetFramework>
        <ImplicitUsings>enable</ImplicitUsings>
        <Nullable>enable</Nullable>
        </PropertyGroup>
    </Project>";

    private string? _tempFilePath;

    [TestInitialize]
    public void Setup()
    {
        _tempFilePath = Path.GetTempFileName();
        File.WriteAllText(_tempFilePath, TestCsprojContent);
    }

    [TestCleanup]
    public void Cleanup()
    {
        if (File.Exists(_tempFilePath))
        {
            File.Delete(_tempFilePath);
        }
    }

    [TestMethod]
    public void UpdateCSProj_WithValidArguments_UpdatesCsprojFile()
    {
        string[] args = {
            $"--filePath={_tempFilePath}",
            "--packageId=jmsudar.DotNet.Xml",
            "--version=1.2.0",
            "--authors=JMSudar",
            "--description=.NET 6 native utilities for XML object manipulation",
            "--packageTags=xml;serialization;deserialization",
            "--repositoryUrl=git@github.com:jmsudar/DotNet.Xml.git",
            "--packageLicenseExpression=GPL-3.0-or-later",
            "--packageProjectUrl=git@github.com:jmsudar/DotNet.Xml.git",
            "--packageReadmeFile=README.md"
        };

        UpdateCSProj.Main(args);

        if (_tempFilePath == null)
        {
            Assert.Fail("Temp file path is null");
            return;
        }
        string updatedContent = File.ReadAllText(_tempFilePath);
        Assert.IsTrue(updatedContent.Contains("<PackageId>jmsudar.DotNet.Xml</PackageId>"));
        Assert.IsTrue(updatedContent.Contains("<Version>1.2.0</Version>"));
        Assert.IsTrue(updatedContent.Contains("<Authors>JMSudar</Authors>"));
        Assert.IsTrue(updatedContent.Contains("<Description>.NET 6 native utilities for XML object manipulation</Description>"));
        Assert.IsTrue(updatedContent.Contains("<PackageTags>xml;serialization;deserialization</PackageTags>"));
        Assert.IsTrue(updatedContent.Contains("<RepositoryUrl>git@github.com:jmsudar/DotNet.Xml.git</RepositoryUrl>"));
        Assert.IsTrue(updatedContent.Contains("<PackageLicenseExpression>GPL-3.0-or-later</PackageLicenseExpression>"));
        Assert.IsTrue(updatedContent.Contains("<PackageProjectUrl>git@github.com:jmsudar/DotNet.Xml.git</PackageProjectUrl>"));
        Assert.IsTrue(updatedContent.Contains("<PackageReadmeFile>README.md</PackageReadmeFile>"));
    }

    [TestMethod]
    public void UpdateCSProj_WithSomeEmptyArguments_UpdatesCsprojFile()
    {
        string[] args = {
            $"--filePath={_tempFilePath}",
            "--packageId=jmsudar.DotNet.Xml",
            "--version=1.2.0",
            "--authors=JMSudar",
            "--description=",
            "--packageTags=",
            "--repositoryUrl=git@github.com:jmsudar/DotNet.Xml.git",
            "--packageLicenseExpression=GPL-3.0-or-later",
            "--packageProjectUrl=git@github.com:jmsudar/DotNet.Xml.git",
            "--packageReadmeFile="
        };

        UpdateCSProj.Main(args);

        if (_tempFilePath == null)
        {
            Assert.Fail("Temp file path is null");
            return;
        }
        string updatedContent = File.ReadAllText(_tempFilePath);
        Assert.IsTrue(updatedContent.Contains("<PackageId>jmsudar.DotNet.Xml</PackageId>"));
        Assert.IsTrue(updatedContent.Contains("<Version>1.2.0</Version>"));
        Assert.IsTrue(updatedContent.Contains("<Authors>JMSudar</Authors>"));
        Assert.IsTrue(updatedContent.Contains("<RepositoryUrl>git@github.com:jmsudar/DotNet.Xml.git</RepositoryUrl>"));
        Assert.IsTrue(updatedContent.Contains("<PackageLicenseExpression>GPL-3.0-or-later</PackageLicenseExpression>"));
        Assert.IsTrue(updatedContent.Contains("<PackageProjectUrl>git@github.com:jmsudar/DotNet.Xml.git</PackageProjectUrl>"));
        Assert.IsFalse(updatedContent.Contains("<Description>"));
        Assert.IsFalse(updatedContent.Contains("<PackageTags>"));
        Assert.IsFalse(updatedContent.Contains("<PackageReadmeFile>"));
    }
}



[TestClass]
public class ArgsParserTests
{
    [TestMethod]
    public void ParseArgs_WithValidArguments_ReturnsParsedDictionary()
    {
        string[] args = { "--targetFramework=net6.0", "--implicitUsings=enable", "--nullable=enable" };
        var result = ArgsParser.ParseArgs(args);

        Assert.AreEqual(3, result.Count);
        Assert.AreEqual("net6.0", result["targetFramework"]);
        Assert.AreEqual("enable", result["implicitUsings"]);
        Assert.AreEqual("enable", result["nullable"]);
    }

    [TestMethod]
    public void ParseArgs_WithNoArguments_ReturnsEmptyDictionary()
    {
        string[] args = { };
        var result = ArgsParser.ParseArgs(args);

        Assert.AreEqual(0, result.Count);
    }
}