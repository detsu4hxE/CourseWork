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
    /// Логика взаимодействия для RolesPage.xaml
    /// </summary>
    public partial class RolesPage : Page
    {
        public RolesPage()
        {
            InitializeComponent();
            ComboSortBy.SelectedIndex = 0;
            UpdateProduct();
        }
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            UpdateProduct();
        }

        private void ComboSortBy_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateProduct();
        }

        private void ComboDiscount_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateProduct();
        }

        private void TBoxSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateProduct();
        }

        private void BtnAddProduct_Click(object sender, RoutedEventArgs e)
        {
            //NavigationService.Navigate(new AddEditProductPage());
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var currentRole = button.DataContext as Roles;
            //NavigationService.Navigate(new AddEditProductPage(currentProduct));
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            var currentRole = (sender as Button).DataContext as Roles;
            if (MessageBox.Show($"Вы уверены, что хотите удалить роль: {currentRole.name}?",
                "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                App.Context.Roles.Remove(currentRole);
                App.Context.SaveChanges();
                UpdateProduct();
            }
        }

        private void UpdateProduct()
        {
            var roles = App.Context.Roles.ToList();
            if (ComboSortBy.SelectedIndex == 0)
            {
                roles = roles.OrderBy(r => r.name).ToList();
            }
            else
            {
                roles = roles.OrderByDescending(r => r.name).ToList();
            }
            roles = roles.Where(p => p.name.ToLower().Contains(TBoxSearch.Text.ToLower())).ToList();
            LViewRoles.ItemsSource = null;
            LViewRoles.ItemsSource = roles;
            int countFind = LViewRoles.Items.Count;
            TBlockItemCounter.Text = countFind.ToString() + " из " + App.Context.Roles.Count().ToString();
            if (countFind < 1)
                TBlockItemCounter.Text += "\nПо вашему запросу ничего не найдено. Измените фильтры.";
        }
    }
}
