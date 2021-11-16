namespace ConsoleApplication.Entities
{
    public class TestsGeneratorConfiguration
    {
        public TestsGeneratorConfiguration(
            string destinationDirectory,
            int maxCountOfParallelFilesRead,
            int maxCountOfParallelTestsGenerationTasks,
            int maxCountOfParallelFilesWrite)
        {
            DestinationDirectory = destinationDirectory;
            MaxCountOfParallelFilesRead = maxCountOfParallelFilesRead;
            MaxCountOfParallelTestsGenerationTasks = maxCountOfParallelTestsGenerationTasks;
            MaxCountOfParallelFilesWrite = maxCountOfParallelFilesWrite;
        }

        public string DestinationDirectory { get; }
        public int MaxCountOfParallelFilesRead { get; }
        public int MaxCountOfParallelTestsGenerationTasks { get; }
        public int MaxCountOfParallelFilesWrite { get; }
    }
}