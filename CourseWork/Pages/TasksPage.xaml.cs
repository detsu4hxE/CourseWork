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
    /// Логика взаимодействия для TasksPage.xaml
    /// </summary>
    public partial class TasksPage : Page
    {
        public TasksPage()
        {
            InitializeComponent();
            var subjects = App.Context.Subjects.Select(s => s.name).ToList();
            foreach (var subject in subjects)
            {
                SubjectFilter.Items.Add(subject);
            }
            SubjectFilter.SelectedIndex = 0;
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

        private void SubjectFilter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Update();
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddEditTaskPage());
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var currentTask = button.DataContext as Tasks;
            NavigationService.Navigate(new AddEditTaskPage(currentTask));
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            var currentTask = (sender as Button).DataContext as Tasks;
            var answers = App.Context.Answers.Where(a => a.task_id == currentTask.task_id);
            var tests = App.Context.Tests.Where(t => t.task_id == currentTask.task_id);
            if (MessageBox.Show($"Вы уверены, что хотите удалить задание: {currentTask.description}?\nКоличество удаляемых ответов: {answers.Count()}.\nКоличество удаляемых тестов: {tests.Count()}.",
                "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                App.Context.Tasks.Remove(currentTask);
                App.Context.SaveChanges();
                Update();
            }
        }

        private void Update()
        {
            var tasks = App.Context.Tasks.ToList();
            if (SubjectFilter.SelectedIndex != 0)
            {
                tasks = tasks.Where(t => t.subjectName == SubjectFilter.SelectedItem.ToString()).ToList();
            }
            if (ComboSortBy.SelectedIndex == 0)
            {
                tasks = tasks.OrderBy(t => t.task_id).ToList();
            }
            else
            {
                tasks = tasks.OrderByDescending(t => t.task_id).ToList();
            }
            tasks = tasks.Where(r => r.description.ToLower().Contains(TBoxSearch.Text.ToLower())).ToList();
            LViewTasks.ItemsSource = null;
            LViewTasks.ItemsSource = tasks;
            int countFind = LViewTasks.Items.Count;
            TBlockItemCounter.Text = countFind.ToString() + " из " + App.Context.Tasks.Count().ToString();
            if (countFind < 1)
                TBlockItemCounter.Text += "\nПо вашему запросу ничего не найдено. Измените фильтры.";
        }
    }
}
