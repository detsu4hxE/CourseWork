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
using System.Text.RegularExpressions;

namespace CourseWork.Pages
{
    /// <summary>
    /// Логика взаимодействия для AddEditRolePage.xaml
    /// </summary>
    public partial class AddEditRolePage : Page
    {
        private Roles currentRole = null;
        Regex nameCheck = new Regex("^[а-яА-ЯёЁ]+(\\s[а-яА-ЯёЁ]+)*$");
        MatchCollection matches;
        public AddEditRolePage()
        {
            InitializeComponent();
        }

        public AddEditRolePage(Roles role)
        {
            InitializeComponent();
            currentRole = role;
            Title = "Редактировать роль";
            TBoxName.Text = currentRole.name;
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
                if (currentRole == null)
                {
                    var role = new Roles
                    {
                        name = TBoxName.Text
                    };

                    App.Context.Roles.Add(role);
                    App.Context.SaveChanges();
                    MessageBox.Show("Роль успешно создана", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    currentRole.name = TBoxName.Text;
                    App.Context.SaveChanges();
                    MessageBox.Show("Роль успешно обновлена", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                NavigationService.Navigate(new RolesPage());
            }
        }
        private string CheckErrors()
        {
            var errorBuilder = new StringBuilder();

            if (string.IsNullOrWhiteSpace(TBoxName.Text))
                errorBuilder.AppendLine("Название роли обязателно для заполнения;");
            matches = nameCheck.Matches(TBoxName.Text);
            if (matches.Count == 0)
                errorBuilder.AppendLine("Некорректное название роли");
            if (nameError.Visibility == Visibility.Visible)
                errorBuilder.AppendLine("Роль с данным названием уже существует;");
            if (errorBuilder.Length > 0)
                errorBuilder.Insert(0, "Устраните следующие ошибки:\n");
            return errorBuilder.ToString();
        }

        private void TBoxName_TextChanged(object sender, TextChangedEventArgs e)
        {
            var roles = App.Context.Roles.ToList();
            roles = roles.Where(r => r.name == TBoxName.Text && r.name != currentRole.name).ToList();
            if (roles.Count > 0)
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
