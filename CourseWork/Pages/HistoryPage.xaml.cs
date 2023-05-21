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
    /// Логика взаимодействия для HistoryPage.xaml
    /// </summary>
    public partial class HistoryPage : Page
    {
        public HistoryPage(int user_id)
        {
            InitializeComponent();
            UpdateHistory(user_id);
        }
        private void UpdateHistory(int user_id)
        {
            var history = App.Context.History;
            var userHistory = history.Where(h => h.user_id == user_id).OrderByDescending(h => h.id_history).ToList();
            listViewHistory.ItemsSource = null;
            listViewHistory.ItemsSource = userHistory;
            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(listViewHistory.ItemsSource);
            PropertyGroupDescription groupDescription = new PropertyGroupDescription("Day");
            view.GroupDescriptions.Add(groupDescription);
        }
    }
}
