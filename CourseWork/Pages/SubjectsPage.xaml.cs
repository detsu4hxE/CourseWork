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
    /// Логика взаимодействия для SubjectsPage.xaml
    /// </summary>
    public partial class SubjectsPage : Page
    {
        public SubjectsPage()
        {
            InitializeComponent();
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

        private void ComboDiscount_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Update();
        }

        private void TBoxSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            Update();
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddEditSubjectPage());
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var currentSubject = button.DataContext as Subjects;
            NavigationService.Navigate(new AddEditSubjectPage(currentSubject));
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            var currentSubject = (sender as Button).DataContext as Subjects;
            var tasks = App.Context.Tasks.Where(t => t.subject_id == currentSubject.subject_id);
            if (MessageBox.Show($"Вы уверены, что хотите удалить тему: {currentSubject.name}?\nКоличество удаляемых заданий: {tasks.Count()}.",
                "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                App.Context.Subjects.Remove(currentSubject);
                App.Context.SaveChanges();
                Update();
            }
        }

        private void Update()
        {
            var subjects = App.Context.Subjects.ToList();
            if (ComboSortBy.SelectedIndex == 0)
            {
                subjects = subjects.OrderBy(s => s.name).ToList();
            }
            else
            {
                subjects = subjects.OrderByDescending(s => s.name).ToList();
            }
            subjects = subjects.Where(s => s.name.ToLower().Contains(TBoxSearch.Text.ToLower())).ToList();
            LViewSubjects.ItemsSource = null;
            LViewSubjects.ItemsSource = subjects;
            int countFind = LViewSubjects.Items.Count;
            TBlockItemCounter.Text = countFind.ToString() + " из " + App.Context.Subjects.Count().ToString();
            if (countFind < 1)
                TBlockItemCounter.Text += "\nПо вашему запросу ничего не найдено. Измените фильтры.";
        }
    }
}
