using System.Collections.Immutable;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using Core.Services;
using Core.Services.Implementations;
using NUnit.Framework;

namespace Tests
{
    public class CoreTests
    {
        private const string EmptyClassTemplate = @"
            using NUnit.Framework;

            namespace Tests
            {
                public class EmptyClassTests
                {
                }
            }
        ";

        private const string ClassWithOnlyOnePublicMethodTemplate = @"
            using NUnit.Framework;

            namespace Tests
            {
                public class ClassWithOnePublicMethodTests
                {
                    [Test]    
                    public void MethodTest()
                    {
                        Assert.Fail(""autogenerated"");
                    }
                }
            }
        ";

        private const string ClassWithOnePublicMethodFirstTemplate = @"
            using NUnit.Framework;

            namespace Tests
            {
                public class ClassInsideNamespaceFirstTests
                {
                    [Test]    
                    public void MethodTest()
                    {
                        Assert.Fail(""autogenerated"");
                    }
                }
            }
        ";

        private const string ClassWithOnePublicMethodSecondTemplate = @"
            using NUnit.Framework;

            namespace Tests
            {
                public class ClassInsideNamespaceSecondTests
                {
                    [Test]    
                    public void GetNewYearTest()
                    {
                        Assert.Fail(""autogenerated"");
                    }
                }
            }
        ";

        private ITestsCreator Creator { get; set; }

        [SetUp]
        public void SetUp()
        {
            Creator = new TestsCreator();
        }

        [Test]
        public void Test_CreateEmptyClassTestsSuccess()
        {
            var text = File.ReadAllText("../../../TestClasses/EmptyClass.cs");

            var result = Creator.Create(text);

            Assert.AreEqual(1, result.Count());
            Assert.AreEqual("EmptyClassTests.cs", result.First().Filename);
            Assert.AreEqual(
                FilterTestFileContent(EmptyClassTemplate),
                FilterTestFileContent(result.First().Contents)
            );
        }

        [Test]
        public void Test_CreateClassWithOnePublicMethodSuccess()
        {
            var text = File.ReadAllText("../../../TestClasses/ClassWithOnePublicMethod.cs");

            var result = Creator.Create(text);

            Assert.AreEqual(1, result.Count());
            Assert.AreEqual("ClassWithOnePublicMethodTests.cs", result.First().Filename);
            Assert.AreEqual(
                FilterTestFileContent(ClassWithOnlyOnePublicMethodTemplate),
                FilterTestFileContent(result.First().Contents)
            );
        }

        [Test]
        public void Test_CreateTwoTestFilesSuccess()
        {
            var text = File.ReadAllText("../../../TestClasses/ClassesInsideNamespace.cs");

            var result = Creator.Create(text);

            Assert.AreEqual(2, result.Count());
            Assert.Contains("ClassInsideNamespaceFirstTests.cs",
                result.Select(testFile => testFile.Filename).ToImmutableList()
            );
            Assert.Contains("ClassInsideNamespaceSecondTests.cs",
                result.Select(testFile => testFile.Filename).ToImmutableList()
            );
            Assert.Contains(
                FilterTestFileContent(ClassWithOnePublicMethodFirstTemplate),
                result.Select(testFile => FilterTestFileContent(testFile.Contents))
                    .ToImmutableList()
            );
            Assert.Contains(
                FilterTestFileContent(ClassWithOnePublicMethodSecondTemplate),
                result.Select(testFile => FilterTestFileContent(testFile.Contents))
                    .ToImmutableList()
            );
        }

        private string FilterTestFileContent(string initialFileContent)
        {
            return Regex.Replace(initialFileContent.Trim(), @"(\n\r)|(\t)|\s+", " ");
        }
    }
}