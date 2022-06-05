using SelSovetApp.Entities;
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

namespace SelSovetApp.Pages
{
    /// <summary>
    /// Логика взаимодействия для WorkersPage.xaml
    /// </summary>
    public partial class WorkersPage : Page
    {
        private User user => DGridWorkers.SelectedItem as User;

        public WorkersPage()
        {
            InitializeComponent();
            DGridWorkers.ItemsSource = App.Context.User.ToList();
            CBoxSort.ItemsSource = new string[] { "Без сортировки", "По ФИО (возр.)", "По ФИО (убыв.)", "По логину (возр.)", "По логину (убыв.)" };
            CBoxSort.SelectedIndex = 0;
        }
        private void UpdateList()
        {
            List<User> users = App.Context.User.ToList();

            string text = TBoxSearch.Text.ToLower();
            if (!string.IsNullOrWhiteSpace(text))
                users = users.Where(x => x.FullName.ToLower().Contains(text) || x.Login.ToLower().Contains(text) || x.Passport.ToLower().Contains(text)).ToList();

            switch (CBoxSort.SelectedIndex)
            {
                case 1:
                    users = users.OrderBy(x => x.FullName).ToList();
                    break;
                case 2:
                    users = users.OrderByDescending(x => x.FullName).ToList();
                    break;
                case 3:
                    users = users.OrderBy(x => x.Login).ToList();
                    break;
                case 4:
                    users = users.OrderByDescending(x => x.Login).ToList();
                    break;
                default:
                    break;
            }


            DGridWorkers.ItemsSource = users;
        }
        private void DGridWorkers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            var enabled = user != null;
            BtnEdit.IsEnabled = enabled;
            BtnDelete.IsEnabled = enabled;
        }

        private void BtnReport_Click(object sender, RoutedEventArgs e)
        {

            Windows.HtmlUserReportWindow f = new Windows.HtmlUserReportWindow(App.Context.User.ToList());
            f.ShowDialog();
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new UserEditorPage(DGridWorkers.SelectedItem as User));
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Вы действительно хотите удалить данного сотрудника?",
"Предупреждение", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                App.Context.User.Remove(user);
                App.Context.SaveChanges();
                UpdateList();
            }
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new UserEditorPage(null));

        }

        private void TBoxSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateList();

        }

        private void CBoxSort_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateList();

        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            UpdateList();

        }
    }
}
