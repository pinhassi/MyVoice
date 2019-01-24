using Microsoft.Win32;
using MyVoice.Model;
using System;
using System.Collections.Generic;
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
            mainLayout.DataContext = Controller.Instance;
        }

        private void SelectFileBtn_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Words List Files|*" + "txt";
            string filePath = Controller.Instance.FilePath;
            if (filePath != null && filePath.Length > 0)
            {
                openFileDialog.InitialDirectory = Path.GetDirectoryName(Controller.Instance.FilePath);
                openFileDialog.FileName = Path.GetFileName(Controller.Instance.FilePath);
            }
            if (openFileDialog.ShowDialog() == true)
            {
                Controller.Instance.FilePath = openFileDialog.FileName;
                selectedFileLabel.Text = Controller.Instance.FilePath;
                LoadFile(openFileDialog.FileName);
            }
            return;
        }


        private void LoadFile(string filename)
        {
            return;
        }

        private void SelectFolderBtn_Click(object sender, RoutedEventArgs e)
        {
            using (var dialog = new System.Windows.Forms.FolderBrowserDialog())
            {
                dialog.SelectedPath = Controller.Instance.FolderPath;
                if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    Controller.Instance.FolderPath = dialog.SelectedPath;
                    selectFolderLabel.Text = Controller.Instance.FolderPath;
                }


                
            }
        }
    }
}
