using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEgyVision.Infrastructure
{
    public static class MISC
    {
        public static string EnsureCorrectFilename(string filename)
        {
            if (filename.Contains("\\"))
                filename = filename.Substring(filename.LastIndexOf("\\") + 1);

            return filename;
        }
    }
}
