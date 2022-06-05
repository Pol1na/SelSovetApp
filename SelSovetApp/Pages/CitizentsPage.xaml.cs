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
    /// Логика взаимодействия для CitizentsPage.xaml
    /// </summary>
    public partial class CitizentsPage : Page
    {
        private Client client => DGridClients.SelectedItem as Client;

        public CitizentsPage()
        {
            InitializeComponent();
            DGridClients.ItemsSource = App.Context.Client.ToList();
            CBoxSort.ItemsSource = new string[] { "Без сортировки", "По ФИО (возр.)", "По ФИО (убыв.)", "По номеру (возр.)", "По номеру (убыв.)" };
            CBoxSort.SelectedIndex = 0;
        }
        private void UpdateList()
        {
            List<Client> clients = App.Context.Client.ToList();

            string text = TBoxSearch.Text.ToLower();
            if (!string.IsNullOrWhiteSpace(text))
                clients = clients.Where(x => x.FullName.ToLower().Contains(text) || x.Phone.ToLower().Contains(text) || x.Email.ToLower().Contains(text)).ToList();

            switch (CBoxSort.SelectedIndex)
            {
                case 1:
                    clients = clients.OrderBy(x => x.FullName).ToList();
                    break;
                case 2:
                    clients = clients.OrderByDescending(x => x.FullName).ToList();
                    break;
                case 3:
                    clients = clients.OrderBy(x => x.Phone).ToList();
                    break;
                case 4:
                    clients = clients.OrderByDescending(x => x.Phone).ToList();
                    break;
                default:
                    break;
            }


            DGridClients.ItemsSource = clients;
        }
        private void CBoxSort_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateList();
        }

        private void TBoxSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateList();

        }

        private void DGridClients_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            var enabled = client != null;
            BtnEdit.IsEnabled = enabled;
            BtnDelete.IsEnabled = enabled;
        }

        private void BtnReport_Click(object sender, RoutedEventArgs e)
        {

            Windows.HtmlClientReportWindow f = new Windows.HtmlClientReportWindow(App.Context.Client.ToList());
            f.ShowDialog();
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {

            NavigationService.Navigate(new ClientEditorPage(client));
            UpdateList();
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Вы действительно хотите удалить данного клиента?",
"Предупреждение", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                App.Context.Client.Remove(client);
                App.Context.SaveChanges();
                UpdateList();
            }
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ClientEditorPage(null));
            UpdateList();

        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            UpdateList();

        }
    }
}
