namespace EgyVisionCore.Infrastructure
{
    public class Helper
    {
        private static Helper _helper = new Helper();

        private Helper() { }

        public static Helper Instance
        {
            get { return _helper ?? (_helper = new Helper()); }
        }

        public static string EnsureCorrectFilename(string filename)
        {
            if (filename.Contains("\\"))
                filename = filename.Substring(filename.LastIndexOf("\\") + 1);

            return filename;
        }
    }
}
