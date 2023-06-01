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
        public int taskId;
        public MainPage(int id)
        {
            InitializeComponent();
            currentUserId = id;
            string code = "using System;\n\npublic class CourseWork\n{\n\tpublic static void Main(string[] args)\n\t{\n\t\tint a = int.Parse(Console.ReadLine());\n\t\tint b = int.Parse(Console.ReadLine());\n\t\tint c = a + b;\n\t\tConsole.WriteLine(\"a + b = \" + c);\n\t}\n}";
            testBox.Text = code;
        }
        private void NewTask_Click(object sender, RoutedEventArgs e)
        {
            var tasks = App.Context.Tasks.ToList();
            int count = tasks.Count;
            Random rnd = new Random();
            int randomTask = rnd.Next(0, count);
            taskBox.Text = tasks[randomTask].description;
            subjectBox.Text = tasks[randomTask].subjectName;
        }
        private void Save_Click(object sender, RoutedEventArgs e)
        {
            //Сохранение
        }
        private void Check_Click(object sender, RoutedEventArgs e)
        {
            string code = testBox.Text;
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
                resultBox.Text = "Код успешно скомпилирован";

                // Запуск скомпилированной программы и получение выходных данных
                var assembly = results.CompiledAssembly;
                Type programType = assembly.GetType("CourseWork" + Counter.a.ToString());
                var method = programType.GetMethod("Main");
                var arguments = new string[] { "5", "7" }; // Входные данные
                var consoleOutput = new StringWriter();
                Console.SetOut(consoleOutput);
                method.Invoke(null, new object[] { arguments });
                var outputText = consoleOutput.ToString();
                Console.SetOut(Console.Out);

                // Вывод результатов в текстовое поле
                resultBox.Text = outputText;
            }
            Console.ReadLine();
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

        private void testBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                int lastLine = testBox.GetLineIndexFromCharacterIndex(testBox.CaretIndex);
                string lastLineText = testBox.GetLineText(lastLine);
                int tabsCount = lastLineText.TakeWhile(c => c == '\t').Count();
                testBox.SelectedText = Environment.NewLine + new String('\t', tabsCount);
                int a = 2;
                if (lastLineText.TrimEnd().EndsWith("{") == true)
                {
                    testBox.SelectedText += "\t";
                    a++;
                }
                testBox.CaretIndex = testBox.SelectionStart + a + tabsCount;
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
    }
}
