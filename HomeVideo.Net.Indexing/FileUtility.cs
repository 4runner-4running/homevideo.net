using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeVideo.Net.Indexing
{
    public static class FileUtility
    {
        public static string FormatFileNameForSearch(string inputFile)
        {
            // Parse out potential issues (dashes, dots, underscores, etc.)
            // Consider more advanced handling if this proved insufficient
            return inputFile.Replace('.', ' ').Replace('_', ' ').Replace('-', ' ');
        }
    }
}
