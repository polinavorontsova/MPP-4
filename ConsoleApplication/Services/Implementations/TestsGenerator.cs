using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
using ConsoleApplication.Entities;
using Core.Entities;
using Core.Services;

namespace ConsoleApplication.Services.Implementations
{
    public class TestsGenerator : ITestsGenerator
    {
        public TestsGenerator(ITestsCreator testsCreator, TestsGeneratorConfiguration configuration)
        {
            TestsCreator = testsCreator;
            Configuration = configuration;

            FilesReadBlock = new TransformBlock<string, string>(
                async path => await File.ReadAllTextAsync(path),
                new ExecutionDataflowBlockOptions
                {
                    MaxDegreeOfParallelism = Configuration.MaxCountOfParallelFilesRead
                }
            );

            TestsGeneratorBlock = new TransformManyBlock<string, TestFile>(
                async sourceFileContent =>
                    await Task.Run(() => TestsCreator.Create(sourceFileContent)),
                new ExecutionDataflowBlockOptions
                {
                    MaxDegreeOfParallelism = Configuration.MaxCountOfParallelTestsGenerationTasks
                }
            );

            SaveTestsBlock = new ActionBlock<TestFile>(
                async testsFile =>
                    await File.WriteAllTextAsync(
                        Path.Combine(Configuration.DestinationDirectory, testsFile.Filename),
                        testsFile.Contents
                    ),
                new ExecutionDataflowBlockOptions
                {
                    MaxDegreeOfParallelism = Configuration.MaxCountOfParallelFilesWrite
                }
            );
        }

        private ITestsCreator TestsCreator { get; }
        private TestsGeneratorConfiguration Configuration { get; }
        private TransformBlock<string, string> FilesReadBlock { get; }
        private TransformManyBlock<string, TestFile> TestsGeneratorBlock { get; }
        private ActionBlock<TestFile> SaveTestsBlock { get; }

        public async Task Generate(IEnumerable<string> filesPaths)
        {
            var linkOptions = new DataflowLinkOptions {PropagateCompletion = true};
            using (FilesReadBlock.LinkTo(TestsGeneratorBlock, linkOptions))
            {
                using (TestsGeneratorBlock.LinkTo(SaveTestsBlock, linkOptions))
                {
                    foreach (var filePath in filesPaths) FilesReadBlock.Post(filePath);

                    FilesReadBlock.Complete();
                    await SaveTestsBlock.Completion;
                }
            }
        }
    }
}