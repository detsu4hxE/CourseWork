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
    /// Логика взаимодействия для AddEditTaskPage.xaml
    /// </summary>
    public partial class AddEditTaskPage : Page
    {
        private Tasks currentTask = null;
        public AddEditTaskPage()
        {
            InitializeComponent();
            var subjects = App.Context.Subjects.Select(s => s.name).ToList();
            subjectBox.ItemsSource = subjects;
            subjectBox.SelectedIndex = 0;
        }
        public AddEditTaskPage(Tasks task)
        {
            InitializeComponent();
            currentTask = task;
            Title = "Редактирование задания";
            var subjects = App.Context.Subjects.Select(s => s.name).ToList();
            subjectBox.ItemsSource = subjects;
            var selectedItem = App.Context.Subjects.Where(s => s.subject_id == task.subject_id).Select(s => s.name).FirstOrDefault();
            subjectBox.Text = selectedItem;
            TBoxDescription.Text = currentTask.description;
        }
        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            var errorMessage = CheckErrors();
            var subject = App.Context.Subjects.Where(s => s.name == subjectBox.SelectedItem.ToString()).FirstOrDefault();
            if (errorMessage.Length > 0)
            {
                MessageBox.Show(errorMessage, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                if (currentTask == null)
                {
                    var task = new Tasks
                    {
                        description = TBoxDescription.Text,
                        subject_id = subject.subject_id
                    };

                    App.Context.Tasks.Add(task);
                    App.Context.SaveChanges();
                    MessageBox.Show("Задание успешно создано", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    currentTask.description = TBoxDescription.Text;
                    currentTask.subject_id = subject.subject_id;
                    App.Context.SaveChanges();
                    MessageBox.Show("Задание успешно обновлено", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                NavigationService.Navigate(new TasksPage());
            }
        }
        private string CheckErrors()
        {
            var errorBuilder = new StringBuilder();

            if (string.IsNullOrWhiteSpace(TBoxDescription.Text))
                errorBuilder.AppendLine("Описание обязателно для заполнения;");
            if (descriptionError.Visibility == Visibility.Visible)
                errorBuilder.AppendLine("Задание с данным условием уже существует;");
            if (errorBuilder.Length > 0)
                errorBuilder.Insert(0, "Устраните следующие ошибки:\n");
            return errorBuilder.ToString();
        }

        private void TBoxName_TextChanged(object sender, TextChangedEventArgs e)
        {
            var tasks = App.Context.Tasks.ToList();
            tasks = tasks.Where(t => t.description == TBoxDescription.Text && t.description != currentTask.description).ToList();
            if (tasks.Count > 0)
            {
                descriptionError.Visibility = Visibility.Visible;
            }
            else
            {
                descriptionError.Visibility = Visibility.Collapsed;
            }
        }
    }
}
