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
using System.Data.Odbc;

namespace CourseWork.Pages
{
    /// <summary>
    /// Логика взаимодействия для Registration.xaml
    /// </summary>
    public partial class Registration : Page
    {
        // Переменные для загрузки изображения
        private byte[] _mainImageData = null;
        public string img = "default_ava.jpg";
        public string path = Path.Combine(Directory.GetParent(Path.Combine(Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).FullName)).FullName, @"Images\");
        public string selectefFileName;
        public string extension = ".jpg";
        string patronymic = "NULL";
        // Регулярные выражения для проверки текстовых полей
        Regex lpCheck = new Regex(@"^\w{5,30}$");
        Regex nameCheck = new Regex(@"^[A-ЯЁ][а-яё]+$");
        Regex emailCheck = new Regex(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$");
        MatchCollection matches;
        public Registration()
        {
            InitializeComponent();
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
                // Проверка на наличие картинки пользователя
                if (img != "default_ava.jpg")
                {
                    img = TBoxLogin.Text + extension;
                    path = path + img;
                    File.Copy(selectefFileName, path);
                }
                var User = new Users
                {
                    login = TBoxLogin.Text,
                    password = TBoxPassword.Password,
                    role_id = 2,
                    surname = TBoxSurname.Text,
                    firstname = TBoxFirstname.Text,
                    patronymic = patronymic,
                    email = TBoxEmail.Text,
                    image = img
                };
                if (_mainImageData != null)
                {
                    User.image = img;
                }

                App.Context.Users.Add(User);
                App.Context.SaveChanges();
                MessageBox.Show("Пользователь успешно создан", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                NavigationService.Navigate(new LoginPage());
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
                patronymic = "NULL";
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
            users = users.Where(u => u.login.ToLower().Equals(TBoxLogin.Text.ToLower())).ToList();
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
            users = users.Where(u => u.email.ToLower().Equals(TBoxEmail.Text.ToLower())).ToList();
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
