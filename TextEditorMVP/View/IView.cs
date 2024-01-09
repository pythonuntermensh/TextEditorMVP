using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace TextEditorMVP
{
    internal interface IView
    {
        int SelectedTabIndex { get; set; }
        string SelectedTabHeader { get; set; }
        string TextBoxContent { get; set; }

        void AddNewTab(string header);
        void SetSymbolsCount(int count);

        event EventHandler<string> FileOpenClick;
        event EventHandler FileSaveClick;
        event EventHandler<string> FileSaveAsClick;
        event EventHandler TextBoxContentChanged;
        event EventHandler SelectedFileChanged;
    }
}
