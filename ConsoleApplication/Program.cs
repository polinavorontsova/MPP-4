using System.Collections.Generic;
using System.Threading.Tasks;
using ConsoleApplication.Entities;
using ConsoleApplication.Services.Implementations;
using Core.Services.Implementations;

namespace ConsoleApplication
{
    internal static class Program
    {
        private static async Task Main()
        {
            var testsCreator = new TestsCreator();
            var testsGenerator = new TestsGenerator(testsCreator, new TestsGeneratorConfiguration(
                "../../../../Tests",
                5,
                5,
                5)
            );

            await testsGenerator.Generate(new List<string>
                {
                    "../../../../ConsoleApplication/Program.cs",
                    "../../../../Core/Services/Implementations/TestsCreator.cs"
                }
            );
        }
    }
}