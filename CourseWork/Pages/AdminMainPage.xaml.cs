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
using System.IO;
using System.Security.Cryptography;
using System.Windows.Interop;

namespace CourseWork.Pages
{
    /// <summary>
    /// Логика взаимодействия для AdminMainPage.xaml
    /// </summary>
    public partial class AdminMainPage : Page
    {
        public string path = Path.Combine(Directory.GetParent(Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).FullName)).FullName, @"Images\");
        public string img;
        private byte[] _mainImageData = null;
        public int currentUserId;
        public AdminMainPage(int id)
        {
            InitializeComponent();
            currentUserId = id;
            Update();
        }
        public void Update()
        {
            // Роль
            var role = App.Context.Roles.ToList().LastOrDefault();
            roleNameBox.Content = role.name;
            // Пользователь
            var user = App.Context.Users.Where(u => u.user_id != currentUserId).ToList().LastOrDefault();
            if (user.image == null)
            {
                img = path + "default_ava.png";
            }
            else
            {
                img = path + user.image;
            }
            _mainImageData = File.ReadAllBytes(img);
            profilePictureBox.Source = new ImageSourceConverter().ConvertFrom(_mainImageData) as ImageSource;
            loginBox.Content = user.login;
            surnameBox.Text = user.surname;
            firstnameBox.Text = user.firstname;
            if (user.patronymic == null)
            {
                patronymicBox.Visibility = Visibility.Collapsed;
                patronymicCaseSeparator.Visibility = Visibility.Collapsed;
            }
            patronymicBox.Text = user.patronymic;
            emailBox.Text = user.email;
            roleUserBox.Content = user.roleName;
            // Ответ
            var answer = App.Context.Answers.ToList().LastOrDefault();
            userBox.Content = answer.login;
            var ad = answer.description.Substring(0, Math.Min(answer.description.Length, 20));
            if (ad != answer.description)
            {
                ad += "...";
            }
            taskBox.Content = ad;
            var ac = answer.shortCode.Substring(0, Math.Min(answer.shortCode.Length, 20));
            if (ac != answer.shortCode)
            {
                ac += "...";
            }
            codeBox.Text = ac;
            dateBox.Text = answer.date.ToString("D");
            // Тема
            var subject = App.Context.Subjects.ToList().LastOrDefault();
            var sn = subject.name.Substring(0, Math.Min(subject.name.Length, 20));
            if (sn != subject.name)
            {
                sn += "...";
            }
            subjectNameBox.Content = sn;
            // Задание
            var task = App.Context.Tasks.ToList().LastOrDefault();
            var tsn = task.subjectName.Substring(0, Math.Min(task.subjectName.Length, 20));
            if (tsn != task.subjectName)
            {
                tsn += "...";
            }
            subjectBox.Content = tsn;
            var td = task.description.Substring(0, Math.Min(task.description.Length, 20));
            if (td != task.description)
            {
                td += "...";
            }
            descriptionBox.Content = td;
            // Тест
            var test = App.Context.Tests.ToList().LastOrDefault();
            var tsd = test.description.Substring(0, Math.Min(test.description.Length, 20));
            if (tsd != test.description)
            {
                tsd += "...";
            }
            description2Box.Content = tsd;
            inputBox.Text = test.input;
            outputBox.Text = test.output;
        }
        private void btnRoles_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new RolesPage());
        }
        private void btnUsers_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new UsersPage(currentUserId));
        }
        private void btnAnswers_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new AnswersPage());
        }
        private void btnSubjects_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new SubjectsPage());
        }
        private void btnTasks_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new TasksPage());
        }
        private void btnTests_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new TestsPage());
        }
        private void roleName_Click(object sender, EventArgs e)
        {
            var role = App.Context.Roles.ToList().LastOrDefault();
            NavigationService.Navigate(new AddEditRolePage(role));
        }
        private void login_Click(object sender, EventArgs e)
        {
            var user = App.Context.Users.ToList().LastOrDefault();
            NavigationService.Navigate(new AddEditUserPage(user, currentUserId));
        }
        private void roleUser_Click(object sender, EventArgs e)
        {
            var user = App.Context.Users.ToList().LastOrDefault();
            var role = App.Context.Roles.Where(r => r.role_id == user.role_id).FirstOrDefault();
            NavigationService.Navigate(new AddEditRolePage(role));
        }
        private void user_Click(object sender, EventArgs e)
        {
            var answer = App.Context.Answers.ToList().LastOrDefault();
            var user = App.Context.Users.Where(u => u.user_id == answer.user_id).FirstOrDefault();
            NavigationService.Navigate(new AddEditUserPage(user, currentUserId));
        }
        private void task_Click(object sender, EventArgs e)
        {
            var answer = App.Context.Answers.ToList().LastOrDefault();
            var task = App.Context.Tasks.Where(t => t.task_id == answer.task_id).FirstOrDefault();
            NavigationService.Navigate(new AddEditTaskPage(task));
        }
        private void subjectName_Click(object sender, EventArgs e)
        {
            var subject = App.Context.Subjects.ToList().LastOrDefault();
            NavigationService.Navigate(new AddEditSubjectPage(subject));
        }
        private void subject_Click(object sender, EventArgs e)
        {
            var task = App.Context.Tasks.ToList().LastOrDefault();
            var subject = App.Context.Subjects.Where(s => s.subject_id == task.subject_id).FirstOrDefault();
            NavigationService.Navigate(new AddEditSubjectPage(subject));
        }
        private void description_Click(object sender, EventArgs e)
        {
            var task = App.Context.Tasks.ToList().LastOrDefault();
            NavigationService.Navigate(new AddEditTaskPage(task));
        }
        private void description2_Click(object sender, EventArgs e)
        {
            var test = App.Context.Tests.ToList().LastOrDefault();
            var task = App.Context.Tasks.Where(t => t.task_id == test.task_id).FirstOrDefault();
            NavigationService.Navigate(new AddEditTaskPage(task));
        }
    }
}
