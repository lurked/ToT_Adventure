using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;

namespace ToT_Adventure
{
    public static class FileManager
    {
        public static void SaveToFile(string strToWrite, string fileType, string path)
        {
            StreamWriter file = new System.IO.StreamWriter("Resources\\" + fileType + "\\" + path, false);
            file.WriteLine(strToWrite);

            file.Close();
        }

        public static string ReadFile(string filePath)
        {
            string fileContent;

            StreamReader file = new StreamReader(filePath.Contains("Resources\\") ? filePath : "Resources\\" + filePath);
            fileContent = file.ReadToEnd();
            file.Close();

            return fileContent;
        }

        public static string[] GetSaves()
        {
            return Directory.GetFiles("Resources\\saves\\", "*.tots");
        }
    }
}
