using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextEditorMVP
{
    internal class FileRepository : IFileRepository
    {
        private List<FileModel> _files;

        public FileRepository()
        {
            _files = new List<FileModel>();
        }

        public FileModel Find(int index)
        {
            return _files[index];
        }
        public FileModel Find(string path)
        {
            return _files.Where(x => x.Path == path).FirstOrDefault();
        }

        public void Remove(FileModel fileModel)
        {
            _files.Remove(fileModel);
        }

        public void RemoveAt(int index)
        {
            _files.RemoveAt(index);
        }

        public void Save(FileModel fileModel)
        {
            fileModel.IsSaved = true;
            if (Find(fileModel.Path) == null)
            {
                _files.Add(fileModel);
                return;
            }
            Find(fileModel.Path).Text = fileModel.Text;
        }

        public void ChangeFilePathByIndex(int index, string newPath)
        {
            _files[index].Path = newPath;
        }

        public int Count()
        {
            return (_files.Count);
        }
    }
}
