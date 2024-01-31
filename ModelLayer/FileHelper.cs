using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer
{
    public class FileHelper
    {
        private static readonly string BaseDirectory = @"C:\Users\deept\Pictures"; // Replace with your actual base directory

        public static string GetFilePath(string fileName)
        {
            // Combine the base directory and the provided file name
            return Path.Combine(BaseDirectory, fileName);
        }
    }
}
