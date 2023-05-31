using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
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
using System.IO;
using System.Diagnostics;

namespace CourseWork
{
    /// <summary>
    /// Логика взаимодействия для AuthorizationWindow.xaml
    /// </summary>
    public partial class AuthorizationWindow : Window
    {
        public AuthorizationWindow()
        {
            InitializeComponent();
            FrameMain.Navigate(new Pages.LoginPage());
            if (Counter.a == -1)
            {
                Counter.a++;
                DeleteOldFiles();
            }
        }

        private void DeleteOldFiles()
        {
            string fileName = Process.GetCurrentProcess().MainModule.FileName;
            string filePath = Path.GetDirectoryName(fileName);
            int a = 1;
            while (File.Exists(filePath + @"\CourseWork" + a.ToString() + ".exe"))
            {
                File.Delete(filePath + @"\CourseWork" + a.ToString() + ".exe");
                a++;
            }
        }
        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            if (FrameMain.CanGoBack && MessageBox.Show($"Вы уверены, что хотите вернуться?\nНесохраненные данные могут быть утеряны",
                "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Exclamation) == MessageBoxResult.Yes)
                FrameMain.GoBack();
        }
        private void FrameMain_ContentRendered(object sender, EventArgs e)
        {
            if (FrameMain.Content.ToString() != "CourseWork.Pages.LoginPage")
            {
                BtnBack.Visibility = Visibility.Visible;
            }
            else
            {
                BtnBack.Visibility = Visibility.Hidden;
            }
        }
    }
}
