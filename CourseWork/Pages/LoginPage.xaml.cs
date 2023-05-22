using CourseWork.Windows;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CourseWork.Pages
{
    /// <summary>
    /// Логика взаимодействия для LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Page
    {
        public LoginPage()
        {
            InitializeComponent();
        }
        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            var currentUser = App.Context.Users.FirstOrDefault(p => p.login == TBoxLogin.Text && p.password == PBoxPassword.Password);
            if (currentUser != null)
            {
                if (currentUser.login.Equals(TBoxLogin.Text) && currentUser.password.Equals(PBoxPassword.Password))
                {
                    App.CurrentUser = currentUser;
                    var History = new History
                    {
                        user_id = currentUser.user_id,
                        date = DateTime.Now
                    };
                    App.Context.History.Add(History);
                    App.Context.SaveChanges();
                    if (currentUser.role_id == 1)
                    {
                        AdminWindow adminWindow = new AdminWindow(currentUser.user_id);
                        adminWindow.Show();
                    }
                    else
                    {
                        MainWindow mainWindow = new MainWindow(currentUser.user_id);
                        mainWindow.Show();
                    }
                    Window.GetWindow(this).Close();
                }
                else
                {
                    MessageBox.Show("Неверный регистр", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                currentUser = App.Context.Users.FirstOrDefault(p => p.login == TBoxLogin.Text);
                if (currentUser != null)
                {
                    MessageBox.Show("Неверный пароль", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    MessageBox.Show("Пользователь не найден.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void BtnRegistration_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new RegistrationPage());
        }
    }
}
