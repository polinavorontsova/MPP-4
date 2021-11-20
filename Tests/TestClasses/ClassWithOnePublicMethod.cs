using System;

namespace Tests.TestClasses
{
    public class ClassWithOnePublicMethod
    {
        public string Property { get; }

        public void Method()
        {
            Console.WriteLine("Happy New Year!");
        }
    }
}