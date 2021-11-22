using System;

namespace Tests.TestClasses
{
    public class PrivateMethodsAndPropertiesClass
    {
        public int IntProperty { get; set; }
        private string StringProperty { get; }
        protected bool BooleanProperty { get; init; }

        private void NeverHappeningMethod()
        {
            Console.WriteLine("Never reached!");
        }

        private string NeverStringReturningMethod()
        {
            return "Merry Christmas!";
        }
    }
}