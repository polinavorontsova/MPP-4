namespace Core.Entities
{
    public class TestFile
    {
        public TestFile(string filename, string contents)
        {
            Filename = filename;
            Contents = contents;
        }

        public string Filename { get; }
        public string Contents { get; }
    }
}