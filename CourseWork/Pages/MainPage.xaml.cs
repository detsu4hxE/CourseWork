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

        //Работает

        //public MainPage(int id)
        //{
        //    InitializeComponent();
        //    currentUserId = id;
        //    testBox.Text = "using System;\n\npublic class CourseWork\n{\n\tpublic static void Main(string[] args)\n\t{\n\t\tConsole.WriteLine (\"Hello Mono World\");\n\t}\n}";
        //    string code = "using System;\n\npublic class CourseWork\n{\n\tpublic static void Main(string[] args)\n\t{\n\t\tint a = 3;\n\t\tint b = int.Parse(Console.ReadLine());\n\t\tint c = a + b;\n\t\tConsole.WriteLine(\"a + b = \" + c);\n\t}\n}";
        //    testBox.Text = code;
        //}
        //https://learn.microsoft.com/ru-ru/troubleshoot/developer/visualstudio/csharp/language-compilers/compile-code-using-compiler
        //private void Button_Click(object sender, RoutedEventArgs e)
        //{
        //    // Создание провайдера компиляции для языка C#
        //    CodeDomProvider provider = new CSharpCodeProvider();

        //    // Настройка параметров компиляции
        //    CompilerParameters parameters = new CompilerParameters();
        //    parameters.GenerateExecutable = true;
        //    parameters.OutputAssembly = "MyProgram.exe";

        //    // Добавление ссылок на необходимые библиотеки
        //    parameters.ReferencedAssemblies.Add("System.dll");

        //    var a = testBox.Text;
        //    // Компиляция исходного кода
        //    CompilerResults results = provider.CompileAssemblyFromSource(parameters, a);

        //    // Проверка результатов компиляции
        //    if (results.Errors.Count > 0)
        //    {
        //        resultBox.Text = "Compilation failed:";
        //        foreach (CompilerError error in results.Errors)
        //        {
        //            resultBox.Text += (error.ErrorText);
        //        }
        //    }
        //    else
        //    {
        //        resultBox.Text = "Compilation succeeded!";
        //    }
        //}

        private void testBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter) // Проверяем, что была нажата клавиша Enter
            {
                TextBox textBox = (TextBox)sender;
                if (textBox.LineCount > 1)
                {
                    string previousLine = textBox.GetLineText(textBox.GetLineIndexFromCharacterIndex(textBox.CaretIndex) - 1); // Получаем предыдущую строку
                    int tabCount = previousLine.Count(c => c.ToString() == "    "); // Считаем количество табуляций в предыдущей строке
                    string tabs = new string('\t', tabCount); // Создаем строку с нужным количеством табуляций
                    textBox.SelectedText = tabs; // Вставляем табуляции в начало новой строки
                }
            }
            if (e.Key == Key.Tab) // Проверяем, что была нажата клавиша TAB
            {
                TextBox textBox = (TextBox)sender;
                int selectionStart = textBox.SelectionStart; // Получаем позицию курсора
                int selectionLength = textBox.SelectionLength; // Получаем длину выделенного текста
                string text = textBox.Text; // Получаем текст из текстового поля
                string newText = text.Substring(0, selectionStart) + "\t" + text.Substring(selectionStart + selectionLength); // Формируем новый текст с символом табуляции
                textBox.Text = newText; // Устанавливаем новый текст в текстовое поле
                textBox.SelectionStart = selectionStart + 1; // Устанавливаем позицию курсора после символа табуляции
                textBox.SelectionLength = 0; // Снимаем выделение
                e.Handled = true; // Отменяем стандартное поведение при нажатии TAB
            }
        }
        public MainPage(int id)
        {
            InitializeComponent();
            currentUserId = id;
            string code = "using System;\n\npublic class CourseWork\n{\n\tpublic static void Main(string[] args)\n\t{\n\t\tint a = 3;\n\t\tint b = int.Parse(Console.ReadLine());\n\t\tint c = a + b;\n\t\tConsole.WriteLine(\"a + b = \" + c);\n\t}\n}";
            testBox.Text = code;
        }
        //private void Button_Click(object sender, RoutedEventArgs e)
        //{
        //    string code = "int a = 3; int b = int.Parse(Console.ReadLine()); int c = a + b; Console.WriteLine(\"a + b = \" + c);";
        //    CSharpCodeProvider provider = new CSharpCodeProvider();
        //    CompilerParameters parameters = new CompilerParameters();
        //    parameters.GenerateExecutable = false;
        //    CompilerResults results = provider.CompileAssemblyFromSource(parameters, code);
        //    if (results.Errors.Count > 0)
        //    {
        //        resultBox.Text = "Compilation failed.\n";
        //        resultBox.Text += "Compilation errors:\n";
        //        foreach (CompilerError error in results.Errors)
        //        {
        //            resultBox.Text += error.ErrorText + "\n";
        //        }
        //    }
        //    else
        //    {
        //        resultBox.Text = "Compilation succeeded!";
        //        string output = RunAssembly(results.CompiledAssembly);
        //        resultBox.Text += "Output:";
        //        resultBox.Text += output;
        //    }
        //}

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // Код, который нужно скомпилировать
            string code = @"
            using System; 

            public class CourseWork1
            { 
                public static void Main(string[] args) 
                {
                    Console.Write(""Введите a = "");
                    int a = int.Parse(Console.ReadLine()); 
                    Console.Write(""Введите b = "");
                    int b = int.Parse(Console.ReadLine()); 
                    int c = a + b; 
                    Console.WriteLine(""a + b = "" + c); 
                } 
            }";

            // Настройки компиляции
            var provider = new CSharpCodeProvider();
            var parameters = new CompilerParameters();
            parameters.GenerateExecutable = true;
            parameters.OutputAssembly = "CourseWork1.exe";

            // Компиляция кода
            CompilerResults results = provider.CompileAssemblyFromSource(parameters, code);

            // Проверка на ошибки компиляции
            if (results.Errors.HasErrors)
            {
                resultBox.Text = "Ошибка компиляции:";
                foreach (CompilerError error in results.Errors)
                {
                    resultBox.Text += error.ErrorText;
                }
            }
            else
            {
                resultBox.Text = "Код успешно скомпилирован";

                // Запуск скомпилированной программы и получение выходных данных
                var assembly = results.CompiledAssembly;
                Type programType = assembly.GetType("CourseWork1");
                var method = programType.GetMethod("Main");
                var arguments = new string[] { "5" }; // Входные данные
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

        static string RunAssembly(System.Reflection.Assembly assembly)
        {
            Type programType = assembly.GetType("Namespace.Program");
            if (programType == null)
            {
                throw new InvalidOperationException("Program type not found");
            }
            System.Reflection.MethodInfo mainMethod = programType.GetMethod("Main", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static);
            if (mainMethod == null)
            {
                throw new InvalidOperationException("Main method not found");
            }
            List<string> outputLines = new List<string>();
            Action<string> writeLineDelegate = (s) => outputLines.Add(s);
            Console.SetOut(new StringWriter());
            Console.SetError(new StringWriter());
            mainMethod.Invoke(null, new object[] { });
            Console.SetOut(null);
            Console.SetError(null);
            return string.Join(Environment.NewLine, outputLines);
        }
    }
}
