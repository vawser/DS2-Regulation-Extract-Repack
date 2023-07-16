using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DS2_Param_Extract
{
    internal class Util
    {
        public static void ShowError(string message)
        {
            MessageBox.Show(message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public static bool CopyMod(string basePath, string extractPath)
        {
            if (basePath == "" || basePath == null || extractPath == "" || extractPath == null)
            {
                return false;
            }

            if (System.IO.Directory.Exists(extractPath))
            {
                System.IO.Directory.Delete(extractPath, true);
            }

            System.IO.Directory.CreateDirectory(extractPath);
            System.IO.Directory.CreateDirectory(extractPath + "\\Param\\");

            string source = basePath;
            string destination = extractPath;

            foreach (string dirPath in Directory.GetDirectories(source, "*", SearchOption.AllDirectories))
            {
                Directory.CreateDirectory(dirPath.Replace(source, destination));
            }

            foreach (string newPath in Directory.GetFiles(source, "*.*", SearchOption.AllDirectories))
            {
                File.Copy(newPath, newPath.Replace(source, destination), true);
            }

            return true;
        }
    }
}
