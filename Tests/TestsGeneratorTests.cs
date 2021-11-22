using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ConsoleApplication.Entities;
using ConsoleApplication.Services;
using ConsoleApplication.Services.Implementations;
using Core.Entities;
using Core.Services;
using Moq;
using NUnit.Framework;

namespace Tests
{
    public class TestsGeneratorTests
    {
        private const string TestFileName = "polina.txt";
        private const string TestFileContent = "super-duper content";
        private const string TestDestinationDirectory = "../../../TestsGenerationResults";
        private ITestsGenerator TestsGenerator { get; set; }

        [SetUp]
        public void SetUp()
        {
            var testsCreatorMock = new Mock<ITestsCreator>();
            testsCreatorMock.Setup(service =>
                    service.Create(It.IsAny<string>()))
                .Returns(new List<TestFile> {new(TestFileName, TestFileContent)});

            TestsGenerator = new TestsGenerator(testsCreatorMock.Object, new TestsGeneratorConfiguration(
                TestDestinationDirectory,
                1, 1, 1
            ));
        }

        [Test]
        public async Task Test_NumberOfGeneratedFilesIsCorrect()
        {
            var testFilePath = "../../../CoreTests.cs";

            await TestsGenerator.Generate(new List<string> {testFilePath});

            var filesCount = Directory.GetFiles(TestDestinationDirectory).Length;
            var filePath = Directory.GetFiles(TestDestinationDirectory).First();
            Assert.AreEqual(1, filesCount);
            Assert.AreEqual(TestFileName, Path.GetFileName(filePath));
            Assert.AreEqual(TestFileContent, File.ReadAllText(filePath));
        }
    }
}