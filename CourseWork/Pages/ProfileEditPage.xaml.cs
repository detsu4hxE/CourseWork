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
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.IO;
using System.Text.RegularExpressions;

namespace CourseWork.Pages
{
    /// <summary>
    /// Логика взаимодействия для ProfileEditPage.xaml
    /// </summary>
    public partial class ProfileEditPage : Page
    {
        public int currentUserId;
        private byte[] _mainImageData = null;
        public string img = null;
        public string path = Path.Combine(Directory.GetParent(Path.Combine(Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).FullName)).FullName, @"Images\");
        public string selectefFileName;
        public string extension = ".jpg";
        string patronymic = null;
        // Регулярные выражения для проверки текстовых полей
        Regex lpCheck = new Regex(@"^\w{5,30}$");
        Regex nameCheck = new Regex(@"^[A-ЯЁ][а-яё]+$");
        Regex emailCheck = new Regex(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$");
        MatchCollection matches;
        public ProfileEditPage(int user_id)
        {
            currentUserId = user_id;
            InitializeComponent();
            UploadInformation(user_id);
        }
        private void UploadInformation(int user_id)
        {
            var users = App.Context.Users.ToList();
            var currentUser = users.Where(u => u.user_id == user_id).FirstOrDefault();
            TBoxLogin.Text = currentUser.login;
            TBoxPassword.Password = currentUser.password;
            TBoxPasswordRepeat.Password = currentUser.password;
            TBoxSurname.Text = currentUser.surname;
            TBoxFirstname.Text = currentUser.firstname;
            TBoxPatronymic.Text = currentUser.patronymic;
            TBoxEmail.Text = currentUser.email;
            if (currentUser.image != null)
            {
                _mainImageData = File.ReadAllBytes(path + currentUser.image);
                ImageService.Source = new ImageSourceConverter().ConvertFrom(_mainImageData) as ImageSource;
            }
        }

        private void BtnSelectImage_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Multiselect = false;
            ofd.Filter = "Image | *.png; *.jpg; *.jpeg";
            if (ofd.ShowDialog() == true)
            {
                img = Path.GetFileName(ofd.FileName);
                extension = Path.GetExtension(img);
                selectefFileName = ofd.FileName;
                _mainImageData = File.ReadAllBytes(ofd.FileName);
                ImageService.Source = new ImageSourceConverter()
                    .ConvertFrom(_mainImageData) as ImageSource;
            }
        }
        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            var errorMessage = CheckErrors();
            if (errorMessage.Length > 0)
            {
                MessageBox.Show(errorMessage, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                var users = App.Context.Users.ToList();
                var currentUser = users.Where(u => u.user_id == currentUserId).FirstOrDefault();
                // Проверка на наличие картинки пользователя
                if (img != null)
                {
                    img = TBoxLogin.Text + extension;
                    var files = Directory.GetFiles(path);
                    int a = 0;
                    while (File.Exists(path + img))
                    {
                        a++;
                        img = TBoxLogin.Text + $" ({a})" + extension;
                    }
                    path = path + img;
                    File.Copy(selectefFileName, path);
                }
                else
                {
                    img = currentUser.image;
                }
                currentUser.login = TBoxLogin.Text;
                currentUser.password = TBoxPassword.Password;
                currentUser.role_id = 2;
                currentUser.surname = TBoxSurname.Text;
                currentUser.firstname = TBoxFirstname.Text;
                currentUser.patronymic = patronymic;
                currentUser.email = TBoxEmail.Text;
                currentUser.image = img;

                App.Context.SaveChanges();
                MessageBox.Show("Данные были обновлены", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                NavigationService.Navigate(new ProfilePage(currentUserId));
            }
        }
        private string CheckErrors()
        {
            var errorBuilder = new StringBuilder();

            matches = lpCheck.Matches(TBoxLogin.Text);
            if (matches.Count == 0)
                errorBuilder.AppendLine("Некорректно введен логин;");
            if (LoginError.Visibility == Visibility.Visible)
                errorBuilder.AppendLine("Пользователь с данным логином уже существует;");
            matches = lpCheck.Matches(TBoxPassword.Password);
            if (matches.Count == 0)
                errorBuilder.AppendLine("Некорректно введен пароль;");
            if (TBoxPasswordRepeat.Password != TBoxPassword.Password)
                errorBuilder.AppendLine("Пароли не совпадают;");
            matches = nameCheck.Matches(TBoxSurname.Text);
            if (matches.Count == 0)
                errorBuilder.AppendLine("Некорректно введена фамилия");
            matches = nameCheck.Matches(TBoxFirstname.Text);
            if (matches.Count == 0)
                errorBuilder.AppendLine("Некорректно введено имя");
            if (TBoxPatronymic.Text == "")
            {
                patronymic = null;
            }
            else
            {
                matches = nameCheck.Matches(TBoxPatronymic.Text);
                if (matches.Count == 0)
                {
                    errorBuilder.AppendLine("Некорректно введено отчество");
                }
                else
                {
                    patronymic = TBoxPatronymic.Text;
                }
            }
            matches = emailCheck.Matches(TBoxEmail.Text);
            if (matches.Count == 0)
                errorBuilder.AppendLine("Некорректно введен email");
            if (EmailError.Visibility == Visibility.Visible)
                errorBuilder.AppendLine("Пользователь с данным email уже существует;");
            if (errorBuilder.Length > 0)
                errorBuilder.Insert(0, "Устраните следующие ошибки:\n");

            return errorBuilder.ToString();
        }

        private void TBoxLogin_TextChanged(object sender, TextChangedEventArgs e)
        {
            var users = App.Context.Users.ToList();
            users = users.Where(u => u.login.ToLower().Equals(TBoxLogin.Text.ToLower()) && u.login != TBoxLogin.Text).ToList();
            if (users.Count > 0)
            {
                LoginError.Visibility = Visibility.Visible;
            }
            else
            {
                LoginError.Visibility = Visibility.Collapsed;
            }
        }

        private void TBoxEmail_TextChanged(object sender, TextChangedEventArgs e)
        {
            var users = App.Context.Users.ToList();
            var currentUser = users.Where(u => u.user_id == currentUserId).FirstOrDefault();
            users = users.Where(u => u.email.ToLower().Equals(TBoxEmail.Text.ToLower()) && u.email != currentUser.email).ToList();
            if (users.Count > 0)
            {
                EmailError.Visibility = Visibility.Visible;
            }
            else
            {
                EmailError.Visibility = Visibility.Collapsed;
            }
        }

        private void PasswordRepeat_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (TBoxPassword.Password != TBoxPasswordRepeat.Password && TBoxPasswordRepeat.Password != "")
            {
                PasswordRepeatError.Visibility = Visibility.Visible;
            }
            else
            {
                PasswordRepeatError.Visibility = Visibility.Collapsed;
            }
        }
    }
}
