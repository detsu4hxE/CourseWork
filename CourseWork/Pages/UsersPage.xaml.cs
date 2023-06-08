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
    /// Логика взаимодействия для UsersPage.xaml
    /// </summary>
    public partial class UsersPage : Page
    {
        public int currentUserId;
        public UsersPage(int id)
        {
            InitializeComponent();
            currentUserId = id;
            var roles = App.Context.Roles.Select(r => r.name).ToList();
            foreach (var role in roles)
            {
                RoleFilter.Items.Add(role);
            }
            RoleFilter.SelectedIndex = 0;
            ComboSortBy.SelectedIndex = 0;
            Update();
        }
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            Update();
        }

        private void ComboSortBy_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Update();
        }

        private void TBoxSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            Update();
        }

        private void RoleFilter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Update();
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddEditUserPage(currentUserId));
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var currentUser = button.DataContext as Users;
            NavigationService.Navigate(new AddEditUserPage(currentUser, currentUserId));
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            var currentUser = (sender as Button).DataContext as Users;
            var answers = App.Context.Answers.Where(a => a.user_id == currentUser.user_id);
            if (MessageBox.Show($"Вы уверены, что хотите удалить пользователя: {currentUser.login}?\nКоличество удаляемых ответов: {answers.Count()}.",
                "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                App.Context.Users.Remove(currentUser);
                App.Context.SaveChanges();
                Update();
            }
        }

        private void Update()
        {
            var users = App.Context.Users.Where(u => u.user_id != currentUserId).ToList();
            if (RoleFilter.SelectedIndex != 0)
            {
                users = users.Where(u => u.roleName == RoleFilter.SelectedItem.ToString()).ToList();
            }
            if (ComboSortBy.SelectedIndex == 0)
            {
                users = users.OrderBy(u => u.login).ToList();
            }
            else
            {
                users = users.OrderByDescending(u => u.login).ToList();
            }
            users = users.Where(u => u.login.ToLower().Contains(TBoxSearch.Text.ToLower()) || u.surname.ToLower().Contains(TBoxSearch.Text.ToLower()) || u.email.ToLower().Contains(TBoxSearch.Text.ToLower())).ToList();
            LViewUsers.ItemsSource = null;
            LViewUsers.ItemsSource = users;
            int countFind = LViewUsers.Items.Count;
            TBlockItemCounter.Text = countFind.ToString() + " из " + (App.Context.Users.Count() - 1).ToString();
            if (countFind < 1)
                TBlockItemCounter.Text += "\nПо вашему запросу ничего не найдено. Измените фильтры.";
        }
    }
}
