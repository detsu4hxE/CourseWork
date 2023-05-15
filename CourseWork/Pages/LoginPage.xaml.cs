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
            var currentUser = App.Context.Users
                .FirstOrDefault(p => p.login == TBoxLogin.Text && p.password == PBoxPassword.Password);
            if (currentUser != null)
            {
                App.CurrentUser = currentUser;
                //NavigationService.Navigate(new ProductPage());
            }
            else
            {
                MessageBox.Show("Пользователь не найден.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BtnGuest_Click(object sender, RoutedEventArgs e)
        {
            //NavigationService.Navigate(new ProductPage());
        }
    }
}
