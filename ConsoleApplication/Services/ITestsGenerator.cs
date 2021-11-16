using System.Collections.Generic;
using System.Threading.Tasks;

namespace ConsoleApplication.Services
{
    public interface ITestsGenerator
    {
        Task Generate(IEnumerable<string> filesPaths);
    }
}