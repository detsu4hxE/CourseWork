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
using System.Windows.Media.Imaging;
using System.IO;

namespace CourseWork.Windows
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public string path = Path.Combine(Directory.GetParent(Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).FullName)).FullName, @"Images\");
        public string currentLogin;
        public MainWindow(string login)
        {
            InitializeComponent();
            currentLogin = login;
            var users = App.Context.Users.ToList();
            var currentUser = users.Where(u => u.login == currentLogin).FirstOrDefault();
            var img = currentUser.image;
            if (currentUser.image == null)
            {
                img = "default_ava.png";
            }
            var profilePic = new BitmapImage(new Uri(path + img, UriKind.Relative));
            (profilePicture.Fill as ImageBrush).ImageSource = profilePic;
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

        private void Ellipse_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            System.Windows.Shapes.Ellipse ellipse = sender as System.Windows.Shapes.Ellipse;
            ellipse.ContextMenu.IsOpen = true;
        }
        private void exitBtn_Click(object sender, EventArgs e)
        {
            AuthorizationWindow authorization = new AuthorizationWindow();
            authorization.Show();
            this.Close();
        }
        private void profileBtn_Click(object sender, EventArgs e)
        {
            FrameMain.Navigate(new Pages.ProfilePage(currentLogin));
        }
    }
}
