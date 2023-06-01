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
    /// Логика взаимодействия для AddEditAnswerPage.xaml
    /// </summary>
    public partial class AddEditAnswerPage : Page
    {
        private Answers currentAnswer = null;
        public int currentUserId = 0;
        public AddEditAnswerPage()
        {
            InitializeComponent();
            var users = App.Context.Users.Select(u => u.login).ToList();
            loginBox.ItemsSource = users;
            loginBox.SelectedIndex = 0;
            var tasks = App.Context.Tasks.Select(t => t.task_id).ToList();
            taskBox.ItemsSource = tasks;
            taskBox.SelectedIndex = 0;
            dateBox.Text = DateTime.Now.ToString();
        }
        public AddEditAnswerPage(Answers answer)
        {
            InitializeComponent();
            currentAnswer = answer;
            Title = "Редактирование ответа";
            var users = App.Context.Users.Select(u => u.login).ToList();
            loginBox.ItemsSource = users;
            var user = App.Context.Users.Where(u => u.user_id == answer.user_id).FirstOrDefault();
            loginBox.SelectedItem = user.login;
            var tasks = App.Context.Tasks.Select(t => t.task_id).ToList();
            taskBox.ItemsSource = tasks;
            taskBox.SelectedItem = answer.task_id;
            TBoxCode.Text = currentAnswer.code;
            dateBox.Text = currentAnswer.date.ToString();
        }
        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            var errorMessage = CheckErrors();
            var user = App.Context.Users.Where(u => u.login == loginBox.SelectedItem.ToString()).FirstOrDefault();
            if (errorMessage.Length > 0)
            {
                MessageBox.Show(errorMessage, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                if (currentAnswer == null)
                {
                    var answer = new Answers
                    {
                        user_id = user.user_id,
                        task_id = int.Parse(taskBox.SelectedItem.ToString()),
                        code = TBoxCode.Text,
                        date = DateTime.Parse(dateBox.Text)
                    };

                    App.Context.Answers.Add(answer);
                    App.Context.SaveChanges();
                    MessageBox.Show("Ответ успешно создан", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    currentAnswer.user_id = user.user_id;
                    currentAnswer.task_id = int.Parse(taskBox.SelectedItem.ToString());
                    currentAnswer.code = TBoxCode.Text;
                    currentAnswer.date = DateTime.Parse(dateBox.Text);
                    App.Context.SaveChanges();
                    MessageBox.Show("Ответ успешно обновлен", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                NavigationService.Navigate(new AnswersPage());
            }
        }
        private string CheckErrors()
        {
            var errorBuilder = new StringBuilder();

            if (string.IsNullOrWhiteSpace(TBoxCode.Text))
                errorBuilder.AppendLine("Код обязателен для заполнения;");
            if (errorBuilder.Length > 0)
                errorBuilder.Insert(0, "Устраните следующие ошибки:\n");
            return errorBuilder.ToString();
        }
    }
}
