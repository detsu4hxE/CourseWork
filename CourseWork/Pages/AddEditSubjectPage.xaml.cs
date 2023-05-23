using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Логика взаимодействия для AddEditSubjectPage.xaml
    /// </summary>
    public partial class AddEditSubjectPage : Page
    {
        private Subjects currentSubject = null;
        Regex nameCheck = new Regex("^[а-яА-ЯёЁ]+(\\s[а-яА-ЯёЁ]+)*$");
        MatchCollection matches;
        public AddEditSubjectPage()
        {
            InitializeComponent();
        }
        public AddEditSubjectPage(Subjects subject)
        {
            InitializeComponent();
            currentSubject = subject;
            Title = "Редактировать тему";
            TBoxName.Text = currentSubject.name;
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
                if (currentSubject == null)
                {
                    var subject = new Subjects
                    {
                        name = TBoxName.Text
                    };

                    App.Context.Subjects.Add(subject);
                    App.Context.SaveChanges();
                    MessageBox.Show("Тема успешно создана", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    currentSubject.name = TBoxName.Text;
                    App.Context.SaveChanges();
                    MessageBox.Show("Тема успешно обновлена", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                NavigationService.Navigate(new SubjectsPage());
            }
        }
        private string CheckErrors()
        {
            var errorBuilder = new StringBuilder();

            if (string.IsNullOrWhiteSpace(TBoxName.Text))
                errorBuilder.AppendLine("Название темы обязателно для заполнения;");
            matches = nameCheck.Matches(TBoxName.Text);
            if (matches.Count == 0)
                errorBuilder.AppendLine("Некорректное название темы");
            if (nameError.Visibility == Visibility.Visible)
                errorBuilder.AppendLine("Тема с данным названием уже существует;");
            if (errorBuilder.Length > 0)
                errorBuilder.Insert(0, "Устраните следующие ошибки:\n");
            return errorBuilder.ToString();
        }

        private void TBoxName_TextChanged(object sender, TextChangedEventArgs e)
        {
            var subjects = App.Context.Subjects.ToList();
            subjects = subjects.Where(s => s.name == TBoxName.Text && s.name != currentSubject.name).ToList();
            if (subjects.Count > 0)
            {
                nameError.Visibility = Visibility.Visible;
            }
            else
            {
                nameError.Visibility = Visibility.Collapsed;
            }
        }
    }
}
