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
    /// Логика взаимодействия для AddEditTestPage.xaml
    /// </summary>
    public partial class AddEditTestPage : Page
    {
        public Tests currentTest = null;
        public AddEditTestPage()
        {
            InitializeComponent();
            var tasks = App.Context.Tasks.Select(t => t.task_id).ToList();
            taskBox.ItemsSource = tasks;
            taskBox.SelectedIndex = 0;
        }
        public AddEditTestPage(Tests test)
        {
            InitializeComponent();
            currentTest = test;
            Title = "Редактирование теста";
            var tasks = App.Context.Tasks.Select(t => t.task_id).ToList();
            taskBox.ItemsSource = tasks;
            taskBox.SelectedItem = test.task_id;
            TBoxInput.Text = currentTest.input;
            TBoxOutput.Text = currentTest.output;
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
                if (currentTest == null)
                {
                    var test = new Tests
                    {
                        task_id = int.Parse(taskBox.SelectedItem.ToString()),
                        input = TBoxInput.Text,
                        output = TBoxOutput.Text
                    };

                    App.Context.Tests.Add(test);
                    App.Context.SaveChanges();
                    MessageBox.Show("Тест успешно создан", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    currentTest.task_id = int.Parse(taskBox.SelectedItem.ToString());
                    currentTest.input = TBoxInput.Text;
                    currentTest.output = TBoxInput.Text;
                    App.Context.SaveChanges();
                    MessageBox.Show("Тест успешно обновлен", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                NavigationService.Navigate(new TestsPage());
            }
        }
        private string CheckErrors()
        {
            var errorBuilder = new StringBuilder();

            if (string.IsNullOrWhiteSpace(TBoxInput.Text))
                errorBuilder.AppendLine("Входные данные обязательны для заполнения;");
            if (string.IsNullOrWhiteSpace(TBoxOutput.Text))
                errorBuilder.AppendLine("Выходные данные обязательны для заполнения;");
            if (errorBuilder.Length > 0)
                errorBuilder.Insert(0, "Устраните следующие ошибки:\n");
            return errorBuilder.ToString();
        }
    }
}
