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
using System.IO;

namespace CourseWork.Pages
{
    /// <summary>
    /// Логика взаимодействия для ProfilePage.xaml
    /// </summary>
    public partial class ProfilePage : Page
    {
        public string path = Path.Combine(Directory.GetParent(Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).FullName)).FullName, @"Images\");
        public string currentLogin;
        public ProfilePage(string login)
        {
            InitializeComponent();
            currentLogin = login;
            var users = App.Context.Users.ToList();
            var currentUser = users.Where(u => u.login == currentLogin).FirstOrDefault();
            var answers = App.Context.Answers.ToList();
            answers = answers.Where(a => a.user_id == currentUser.user_id).ToList();
            var history = App.Context.History.ToList();
            var currentHistory = history.Where(h => h.user_id == currentUser.user_id).FirstOrDefault();
            loginBox.Text = currentUser.login;
            surnameBox.Text = currentUser.surname;
            firstnameBox.Text = currentUser.firstname;
            if (currentUser.patronymic != null)
            {
                patronymicBox.Text = currentUser.patronymic;
                patronymicBox.Visibility = Visibility.Visible;
                patronymicBoxTitle.Visibility = Visibility.Visible;
            }
            emailBox.Text = currentUser.email;
            answersAmountBox.Text = answers.Count().ToString();
            creationDateBox.Text = currentHistory.date.ToString("D");
            var img = currentUser.image;
            if (currentUser.image == null)
            {
                img = "default_ava.png";
            }
            var profilePic = new BitmapImage(new Uri(path + img, UriKind.Relative));
            profilePicture.Source = new ImageSourceConverter().ConvertFrom(path + img) as ImageSource;
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            var users = App.Context.Users.ToList();
            var currentUser = users.Where(u => u.login == currentLogin).FirstOrDefault();
            if (MessageBox.Show($"Вы уверены, что хотите удалить аккаунт?",
                "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                App.Context.Users.Remove(currentUser);
                App.Context.SaveChanges();
                AuthorizationWindow authorizationWindow = new AuthorizationWindow();
                authorizationWindow.Show();
                Window.GetWindow(this).Close();
                MessageBox.Show("Аккаут был удалён");
            }
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
