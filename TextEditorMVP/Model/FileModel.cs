using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextEditorMVP
{
    internal class FileModel
    {
        public string Name { get => Path.Split('\\').Last(); }
        public string Path { get; set; }
        public string Text { get; set; }
        public bool IsSaved { get; set; }

        public FileModel()
        {
            Text = string.Empty;
            IsSaved = true;
        }
        public FileModel(string path, string text) : base()
        {
            Path = path;
            Text = text;
        }
    }
}
