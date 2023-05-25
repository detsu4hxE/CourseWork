﻿using System;
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
    /// Логика взаимодействия для AnswersPage.xaml
    /// </summary>
    public partial class AnswersPage : Page
    {
        public AnswersPage()
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

        private void ComboDiscount_SelectionChanged(object sender, SelectionChangedEventArgs e)
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
            NavigationService.Navigate(new AddEditAnswerPage());
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var currentAnswer = button.DataContext as Answers;
            NavigationService.Navigate(new AddEditAnswerPage(currentAnswer));
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            var currentAnswer = (sender as Button).DataContext as Answers;
            var answers = App.Context.Answers.Where(a => a.task_id == currentAnswer.task_id);
            if (MessageBox.Show($"Вы уверены, что хотите удалить данный ответ?",
                "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                App.Context.Answers.Remove(currentAnswer);
                App.Context.SaveChanges();
                Update();
            }
        }

        private void Update()
        {
            var answers = App.Context.Answers.ToList();
            if (Filter.SelectedIndex == 0)
            {
                if (ComboSortBy.SelectedIndex == 0)
                {
                    answers = answers.OrderByDescending(a => a.date).ToList();
                }
                else
                {
                    answers = answers.OrderBy(a => a.date).ToList();
                }
            }
            else
            {
                if (ComboSortBy.SelectedIndex == 0)
                {
                    answers = answers.OrderBy(a => a.login).ToList();
                }
                else
                {
                    answers = answers.OrderByDescending(a => a.login).ToList();
                }
            }
            answers = answers.Where(r => r.description.ToLower().Contains(TBoxSearch.Text.ToLower())).ToList();
            LViewAnswers.ItemsSource = null;
            LViewAnswers.ItemsSource = answers;
            int countFind = LViewAnswers.Items.Count;
            TBlockItemCounter.Text = countFind.ToString() + " из " + App.Context.Tasks.Count().ToString();
            if (countFind < 1)
                TBlockItemCounter.Text += "\nПо вашему запросу ничего не найдено. Измените фильтры.";
        }
    }
}
