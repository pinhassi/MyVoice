using Microsoft.Win32;
using MyVoice.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Path = System.IO.Path;

namespace MyVoice
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            mainLayout.DataContext = MainVM.Instance;
            phrasesListView.ItemsSource = MainVM.Instance.PhrasesList;
        }

        private void SelectFileBtn_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Words List Files|*" + "txt";
            string filePath = MainVM.Instance.FilePath;
            if (filePath != null && filePath.Length > 0)
            {
                openFileDialog.InitialDirectory = Path.GetDirectoryName(MainVM.Instance.FilePath);
                openFileDialog.FileName = Path.GetFileName(MainVM.Instance.FilePath);
            }
            if (openFileDialog.ShowDialog() == true)
            {
                MainVM.Instance.FilePath = openFileDialog.FileName;
                selectedFileLabel.Text = MainVM.Instance.FilePath;
                CollectionViewSource.GetDefaultView(phrasesListView.ItemsSource).Refresh();
            }
            return;
        }


        private void SelectFolderBtn_Click(object sender, RoutedEventArgs e)
        {
            using (var dialog = new System.Windows.Forms.FolderBrowserDialog())
            {
                dialog.SelectedPath = MainVM.Instance.FolderPath;
                if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    MainVM.Instance.FolderPath = dialog.SelectedPath;
                    selectFolderLabel.Text = MainVM.Instance.FolderPath;
                    CollectionViewSource.GetDefaultView(phrasesListView.ItemsSource).Refresh();
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainVM.Instance.PhrasesList.RemoveAt(0);
            CollectionViewSource.GetDefaultView(phrasesListView.ItemsSource).Refresh();

        }
    }
}
