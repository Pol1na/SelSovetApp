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
    /// Логика взаимодействия для AppealsPage.xaml
    /// </summary>
    public partial class AppealsPage : Page
    {
        public AppealsPage()
        {
            InitializeComponent();
            LViewAppeals.ItemsSource = App.Context.Appeal.ToList();
            List<Status> types = App.Context.Status.ToList();
            types.Insert(0, new Status() { Name = "Все статусы" });
            CBoxType.ItemsSource = types;
            CBoxType.SelectedIndex = 0;
        }
        private void UpdateList()
        {
            List<Appeal> appeals = App.Context.Appeal.ToList();

            if (!string.IsNullOrWhiteSpace(TBoxSearch.Text))
                appeals = appeals.Where(x => x.User.FullName.ToLower().Contains(TBoxSearch.Text.ToLower()) ||

                x.Status.Name.ToLower().Contains(TBoxSearch.Text.ToLower()) ||
                x.Result.Name.ToLower().Contains(TBoxSearch.Text.ToLower())).ToList();

            if (CBoxType.SelectedIndex == 0)
            {
                LViewAppeals.ItemsSource = appeals;
            }
            else
            {
                appeals = appeals.Where(x => x.Status == CBoxType.SelectedItem as Status).ToList();
            }
            LViewAppeals.ItemsSource = appeals;
        }

        private void BtnReport_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (LViewAppeals.SelectedItem is Appeal appeal)
            {
                if (MessageBox.Show("Вы действительно хотите удалить данное обращение?", "Сообщение", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    App.Context.Appeal.Remove(appeal);
                    App.Context.SaveChanges();
                    UpdateList();
                }
            }
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AppealEditorPage(null));
            UpdateList();
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (LViewAppeals.SelectedItem is Appeal product)
            {
                NavigationService.Navigate(new AppealEditorPage(product));
            }
        }

        private void CBoxType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateList();

        }

        private void TBoxSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateList();

        }

        private void LViewAppeals_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var value = LViewAppeals.SelectedItem != null;
            BtnEdit.IsEnabled = value;
            BtnDelete.IsEnabled = value;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            UpdateList();

        }
    }
}
