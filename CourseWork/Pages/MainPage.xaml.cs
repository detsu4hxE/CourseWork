using Microsoft.CSharp;
using System.Reflection;
using System;
using System.CodeDom.Compiler;
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
using System.Drawing;
using System.Diagnostics;
using System.IO;

namespace CourseWork.Pages
{
    /// <summary>
    /// Логика взаимодействия для MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        public int currentUserId;
        public int taskId = 0;
        public int answerCheck = 0;
        public MainPage(int id)
        {
            InitializeComponent();
            currentUserId = id;
            string code = "using System;\n\npublic class CourseWork\n{\n\tpublic static void Main(string[] args)\n\t{\n\t\tint a = int.Parse(Console.ReadLine());\n\t\tint b = int.Parse(Console.ReadLine());\n\t\tint c = a + b;\n\t\tConsole.WriteLine(\"a + b = \" + c);\n\t}\n}";
            codeBox.Text = code;
        }
        public MainPage(Answers answer, int id)
        {
            InitializeComponent();
            codeBox.Text = answer.code;
            subjectBox.Text = answer.subjectName;
            taskBox.Text = answer.description;
            currentUserId = id;
            taskId = answer.taskId;
        }
        private void NewTask_Click(object sender, RoutedEventArgs e)
        {
            answerCheck = 0;
            var tasks = App.Context.Tasks.ToList();
            int count = tasks.Count;
            Random rnd = new Random();
            int randomTask = rnd.Next(0, count);
            randomTask = 6;
            taskBox.Text = tasks[randomTask].description;
            subjectBox.Text = tasks[randomTask].subjectName;
            taskId = tasks[randomTask].task_id;
        }
        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if (taskId == 0)
            {
                MessageBox.Show($"Вы еще не получили задание");
                return;
            }
            if (answerCheck == 0)
            {
                MessageBox.Show("Перед сохранением необходимо провести проверку кода");
                return;
            }
            var answer = new Answers
            {
                user_id = currentUserId,
                task_id = taskId,
                code = codeBox.Text,
                date = DateTime.Now
            };
            App.Context.Answers.Add(answer);
            App.Context.SaveChanges();
        }
        private void Check_Click(object sender, RoutedEventArgs e)
        {
            answerCheck = 0;
            if (taskId == 0)
            {
                MessageBox.Show($"Вы еще не получили задание");
                return;
            }
            string code = codeBox.Text;
            if (!code.Contains("public class CourseWork"))
            {
                resultBox.Text = "Необходима строка:\npublic class CourseWork";
                return;
            }
            if (SubjectCheck() == false)
            {
                resultBox.Text = "Задание решено без тематических операторов";
                return;
            }
            Counter.a++;
            ReplaceCR(ref code);
            // Настройки компиляции
            var provider = new CSharpCodeProvider();
            var parameters = new CompilerParameters();
            parameters.GenerateExecutable = true;
            parameters.OutputAssembly = "CourseWork" + Counter.a.ToString() + ".exe";

            // Компиляция кода
            CompilerResults results = provider.CompileAssemblyFromSource(parameters, code);

            // Проверка на ошибки компиляции
            if (results.Errors.HasErrors)
            {
                Counter.a--;
                resultBox.Text = "Ошибка компиляции:\n";
                foreach (CompilerError error in results.Errors)
                {
                    resultBox.Text += error.ErrorText + "\n";
                }
            }
            else
            {
                // Запуск скомпилированной программы и получение выходных данных
                try
                {
                    var assembly = results.CompiledAssembly;
                    Type programType = assembly.GetType("CourseWork" + Counter.a.ToString());
                    var method = programType.GetMethod("Main");

                    var tests = App.Context.Tests.Where(t => t.task_id == taskId).ToList();
                    string outputText = "";
                    foreach (var test in tests)
                    {
                        char[] separators = { ',', ';', '.' };
                        var inputParts = test.input.Split(separators, StringSplitOptions.RemoveEmptyEntries);
                        var consoleOutput = new StringWriter();
                        Console.SetOut(consoleOutput);
                        method.Invoke(null, new object[] { inputParts });
                        outputText = consoleOutput.ToString();
                        Console.SetOut(Console.Out);
                        //MessageBox.Show(outputText + ", " + test.output);
                        if (outputText.Trim() != test.output.Trim())
                        {
                            resultBox.Text = "Задание решено неверно";
                            return;
                        }
                    }
                    resultBox.Text = "Задание решено верно";
                    answerCheck = 1;
                    Console.ReadLine();
                }
                catch (Exception ex)
                {
                    resultBox.Text = ex.Message;
                }
            }
        }
        public bool SubjectCheck()
        {
            string a = codeBox.Text;
            bool b = false;
            var task = App.Context.Tasks.Where(t => t.task_id == taskId).FirstOrDefault();
            switch (task.subjectName)
            {
                case "Условия":
                    b = (a.Contains("switch") || a.Contains("if") ? true : false);
                    break;
                case "Циклы":
                    b = (a.Contains("while") || a.Contains("for") || a.Contains("foreach") ? true : false);
                    break;
                case "Одномерные массивы":
                    b = (a.Contains("int[") || a.Contains("string[") ? true : false);
                    break;
                case "Многомерные массивы":
                    b = (a.Contains("int[") || a.Contains("string[") ? true : false);
                    break;
                case "Функции":
                    var funcs = new string[] { "public static int", "public int", "public double", "public static double", "public string", "public static string" };
                    int c = 0;
                    foreach (var func in funcs)
                    {
                        c = a.Contains(func) ? 1 : c;
                    }
                    b = c == 1 ? true : false;
                    break;
                case "Процедуры":
                    c = 0;
                    c = (a.Length - a.Replace("public static void", "").Length) / "public static void".Length > 1 ? 1 : 0;
                    var proceds = new string[] { "public static void", "private void", "private static void" };
                    foreach (var proced in proceds)
                    {
                        c = a.Contains(proced) ? 1 : c;
                    }
                    b = c == 1 ? true : false;
                    break;
                default:
                    break;
            }

            return b;
        }
        
        public void ReplaceCR(ref string text)
        {
            int c = 0;
            while (text.Contains("Console.ReadLine()"))
            {
                int index = text.IndexOf("Console.ReadLine()");
                string replacement = $"args[{c}]";
                text = text.Substring(0, index) + replacement + text.Substring(index + 18);
                c++;
            }
            text = text.Replace("CourseWork", "CourseWork" + Counter.a.ToString());
        }

        private void codeBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                int lastLine = codeBox.GetLineIndexFromCharacterIndex(codeBox.CaretIndex);
                string lastLineText = codeBox.GetLineText(lastLine);
                int tabsCount = lastLineText.TakeWhile(c => c == '\t').Count();
                codeBox.SelectedText = Environment.NewLine + new String('\t', tabsCount);
                int a = 2;
                if (lastLineText.TrimEnd().EndsWith("{") == true)
                {
                    codeBox.SelectedText += "\t";
                    a++;
                }
                codeBox.CaretIndex = codeBox.SelectionStart + a + tabsCount;
                e.Handled = true;
            }
            if (e.Key == Key.Tab)
            {
                TextBox textBox = (TextBox)sender;
                int selectionStart = textBox.SelectionStart;
                int selectionLength = textBox.SelectionLength;
                string text = textBox.Text;
                string newText = text.Substring(0, selectionStart) + "\t" + text.Substring(selectionStart + selectionLength);
                textBox.Text = newText;
                textBox.SelectionStart = selectionStart + 1;
                textBox.SelectionLength = 0;
                e.Handled = true;
            }
        }

        private void codeBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            answerCheck = 0;
        }
    }
}
