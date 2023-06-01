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
    /// Логика взаимодействия для TestsPage.xaml
    /// </summary>
    public partial class TestsPage : Page
    {
        public TestsPage()
        {
            InitializeComponent();
            Filter.SelectedIndex = 0;
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

        private void Filter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Update();
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddEditTestPage());
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var currentTest = button.DataContext as Tests;
            NavigationService.Navigate(new AddEditTestPage(currentTest));
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            var currentTest = (sender as Button).DataContext as Tests;
            if (MessageBox.Show($"Вы уверены, что хотите удалить данный тест?",
                "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                App.Context.Tests.Remove(currentTest);
                App.Context.SaveChanges();
                Update();
            }
        }

        private void Update()
        {
            var tests = App.Context.Tests.ToList();
            if (Filter.SelectedIndex == 0)
            {
                if (ComboSortBy.SelectedIndex == 0)
                {
                    tests = tests.OrderByDescending(t => t.shortDescription).ToList();
                }
                else
                {
                    tests = tests.OrderBy(t => t.shortDescription).ToList();
                }
            }
            tests = tests.Where(t => t.description.ToLower().Contains(TBoxSearch.Text.ToLower())).ToList();
            LViewTests.ItemsSource = null;
            LViewTests.ItemsSource = tests;
            int countFind = LViewTests.Items.Count;
            TBlockItemCounter.Text = countFind.ToString() + " из " + App.Context.Answers.Count().ToString();
            if (countFind < 1)
                TBlockItemCounter.Text += "\nПо вашему запросу ничего не найдено. Измените фильтры.";
        }
    }
}
