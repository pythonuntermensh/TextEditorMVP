using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TextEditorMVP
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, IView
    {
        public MainWindow()
        {
            InitializeComponent();
            OpenButton.Click += OpenButton_Click;
            SaveButton.Click += SaveButton_Click;
            SaveAsButton.Click += SaveAsButton_Click;
            TabControl.SelectionChanged += TabControl_SelectionChanged;
            TextBox.TextChanged += TextBox_TextChanged;

            MainPresenter mainPresenter = new MainPresenter(this, new FileRepository(), new FileManager());
        }

        private void OpenButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog()
            {
                Title = "Open new file",
                Filter = "Text documents (.txt)|*.txt",
                DefaultExt = ".txt",
                CheckFileExists = true,
                CheckPathExists = true,
            };

            if (openFileDialog.ShowDialog() == true)
            {
                FileOpenClick?.Invoke(this, openFileDialog.FileName);
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            FileSaveClick?.Invoke(this, EventArgs.Empty);
        }

        private void SaveAsButton_Click(object sender, RoutedEventArgs e)
        {
            if (TabControl.SelectedIndex == -1)
            {
                return;
            }

            SaveFileDialog saveFileDialog = new SaveFileDialog()
            {
                Title = "Save file",
                Filter = "Text documents (.txt)|*.txt",
                DefaultExt = ".txt",
                CheckPathExists = true,
            };

            if (saveFileDialog.ShowDialog() == true)
            {
                FileSaveAsClick?.Invoke(this, saveFileDialog.FileName);
            }
        }

        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SelectedFileChanged?.Invoke(this, EventArgs.Empty);
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBoxContentChanged?.Invoke(this, EventArgs.Empty);
        }

        // IView implementation
        int IView.SelectedTabIndex { get => TabControl.SelectedIndex; set => TabControl.SelectedIndex = value; }
        string IView.SelectedTabHeader {
            get => (string)((TabItem)TabControl.Items.GetItemAt(TabControl.SelectedIndex)).Header;
            set => ((TabItem)TabControl.Items.GetItemAt(TabControl.SelectedIndex)).Header = value;
            }
        string IView.TextBoxContent { get => TextBox.Text; set => TextBox.Text = value; }

        public event EventHandler<string> FileOpenClick;
        public event EventHandler FileSaveClick;
        public event EventHandler<string> FileSaveAsClick;
        public event EventHandler TextBoxContentChanged;
        public event EventHandler SelectedFileChanged;

        public void AddNewTab(string header)
        {
            TabControl.Items.Add(new TabItem()
            {
                Header = header
            });
        }

        public void SetSymbolsCount(int count)
        {
            SymbolsCounterTextBlock.Text = $"Symbols count: {count}";
        }
    }
}
