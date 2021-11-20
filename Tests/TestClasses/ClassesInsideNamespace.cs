using System;

namespace Tests.TestClasses
{
    public class ClassInsideNamespaceFirst
    {
        private void UnreachableMethod()
        {
            Console.WriteLine("UnreachableMethod");
        }

        public void Method()
        {
            Console.WriteLine("Merry Christmas!");
        }
    }

    public class ClassInsideNamespaceSecond
    {
        protected string Property { get; set; }

        public int GetNewYear()
        {
            return 2022;
        }
    }
}