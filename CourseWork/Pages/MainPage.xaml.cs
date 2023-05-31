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
        public MainPage(int id)
        {
            InitializeComponent();
            currentUserId = id;
            string code = "using System;\n\npublic class CourseWork\n{\n\tpublic static void Main(string[] args)\n\t{\n\t\tint a = int.Parse(Console.ReadLine());\n\t\tint b = int.Parse(Console.ReadLine());\n\t\tint c = a + b;\n\t\tConsole.WriteLine(\"a + b = \" + c);\n\t}\n}";
            testBox.Text = code;
        }
        private void Button_Click(object sender, RoutedEventArgs e)
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
            string[] args = { "args[0]", "args[1]", "args[2]" }; // массив для замены
            int count = 0; // счетчик замен

            while (text.Contains("Console.ReadLine()"))
            {
                int index = text.IndexOf("Console.ReadLine()"); // находим индекс первого вхождения "ab()"
                string replacement = args[count % args.Length]; // определяем элемент массива для замены
                text = text.Substring(0, index) + replacement + text.Substring(index + 18); // заменяем "ab()"
                count++; // увеличиваем счетчик замен
            }
            text = text.Replace("CourseWork", "CourseWork" + Counter.a.ToString());
        }

        private void testBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter) // Проверяем, что была нажата клавиша Enter
            {
                TextBox textBox = (TextBox)sender;
                if (textBox.LineCount > 1)
                {
                    string previousLine = textBox.GetLineText(textBox.GetLineIndexFromCharacterIndex(textBox.CaretIndex) - 1); // Получаем предыдущую строку
                    int tabCount = previousLine.Count(c => c == '\t'); // Считаем количество табуляций в предыдущей строке
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
    }
}
