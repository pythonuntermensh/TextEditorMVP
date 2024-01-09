using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextEditorMVP
{
    internal interface IFileManager
    {
        string GetFileContent(string path);
        void SaveFile(string path, string content);
        bool DoesExist(string path);
    }
    internal class FileManager : IFileManager
    {
        public string GetFileContent(string path)
        {
            using (StreamReader sr = new StreamReader(path))
            {
                return sr.ReadToEnd();
            }
        }

        public void SaveFile(string path, string content)
        {
            using (StreamWriter sw = new StreamWriter(path))
            {
                sw.Write(content);
            }
        }
        public bool DoesExist(string path)
        {
            return File.Exists(path);
        }
    }
}
