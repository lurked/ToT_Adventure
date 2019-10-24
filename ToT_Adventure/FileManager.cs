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
    }
}
