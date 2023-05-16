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
using System.Windows.Shapes;

namespace CourseWork
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        object frameContent = null;
        public MainWindow()
        {
            InitializeComponent();
            FrameMain.Navigate(new Pages.LoginPage());
        }
        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            if (FrameMain.CanGoBack && MessageBox.Show($"Вы уверены, что хотите вернуться?\nНесохраненные данные могут быть утеряны",
                "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Exclamation) == MessageBoxResult.Yes)
                FrameMain.GoBack();
        }
        private void FrameMain_ContentRendered(object sender, EventArgs e)
        {
            if (FrameMain.Content != frameContent)
            {
                if (App.CurrentUser != null)
                    TBlockUsername.Text = App.CurrentUser.login;
                else TBlockUsername.Text = String.Empty;
            }
            else
            {
                TBlockUsername.Text = String.Empty;
            }
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            frameContent = FrameMain.Content;
        }
    }
}
