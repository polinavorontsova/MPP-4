using System.Collections.Generic;
using Core.Entities;

namespace Core.Services
{
    public interface ITestsCreator
    {
        IEnumerable<TestFile> Create(string fileContents);
    }
}