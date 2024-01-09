using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextEditorMVP
{
    internal interface IFileRepository
    {
        FileModel Find(int index);
        FileModel Find(string path);
        void Save(FileModel fileModel);
        void ChangeFilePathByIndex(int index, string newPath);
        void Remove(FileModel fileModel);
        void RemoveAt(int index);
        int Count();
    }
}
