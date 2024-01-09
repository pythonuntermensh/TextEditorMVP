using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextEditorMVP
{
    internal class MainPresenter
    {
        private readonly IView _view;
        private readonly IFileRepository _fileRepository;
        private readonly IFileManager _fileManager;

        public MainPresenter(IView view, IFileRepository fileRepository, IFileManager fileManager)
        {
            _view = view;
            _fileRepository = fileRepository;
            _fileManager = fileManager;

            _view.FileOpenClick += _view_FileOpenClick;
            _view.SelectedFileChanged += _view_SelectedFileChanged;
            _view.FileSaveClick += _view_FileSaveClick;
            _view.FileSaveAsClick += _view_FileSaveAsClick;
            _view.TextBoxContentChanged += _view_TextBoxContentChanged;
        }

        private void _view_TextBoxContentChanged(object sender, EventArgs e)
        {
            if (_view.SelectedTabIndex != -1)
            {
                _fileRepository.Save(new FileModel(_fileRepository.Find(_view.SelectedTabIndex).Path, _view.TextBoxContent));
            }
        }

        private void _view_FileSaveAsClick(object sender, string e)
        {
            _fileRepository.ChangeFilePathByIndex(_view.SelectedTabIndex, e);
            _fileRepository.Save(new FileModel(e, _view.TextBoxContent));
            _fileManager.SaveFile(e, _view.TextBoxContent);
            _view.SelectedTabHeader = _fileRepository.Find(e).Name;
        }

        private void _view_FileSaveClick(object sender, EventArgs e)
        {
            if (_view.SelectedTabIndex != -1)
            {
                _fileRepository.Save(new FileModel(_fileRepository.Find(_view.SelectedTabIndex).Path, _view.TextBoxContent));
                _fileManager.SaveFile(_fileRepository.Find(_view.SelectedTabIndex).Path, _view.TextBoxContent);
            }
        }

        private void _view_SelectedFileChanged(object sender, EventArgs e)
        {
            if (_view.SelectedTabIndex != -1)
            {
                _view.TextBoxContent = _fileRepository.Find(_view.SelectedTabIndex).Text;
                return;
            }
            _view.TextBoxContent = string.Empty;
        }

        private void _view_FileOpenClick(object sender, string e)
        {
            if (!_fileManager.DoesExist(e) || _fileRepository.Find(e) != null)
            {
                return;
            }

            _fileRepository.Save(new FileModel(e, _fileManager.GetFileContent(e)));

            _view.AddNewTab(e.Split('\\').Last());
            _view.SelectedTabIndex = _fileRepository.Count() - 1;
        }
    }
}
