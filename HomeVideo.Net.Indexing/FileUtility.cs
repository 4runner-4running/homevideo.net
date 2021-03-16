using System;
using System.Collections.Generic;
using System.IO;
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

            var dir = Path.GetDirectoryName(inputFile);
            var fileName = Path.GetFileNameWithoutExtension(inputFile);

            // Folder/File naming should be consistent. if file does not match folder, work off folder instead
            if (fileName.ToLower() != dir.ToLower())
                return dir.Replace('.', ' ').Replace('_', ' ').Replace('-', ' ');
            else
                return fileName.Replace('.', ' ').Replace('_', ' ').Replace('-', ' ');
        }
    }
}
