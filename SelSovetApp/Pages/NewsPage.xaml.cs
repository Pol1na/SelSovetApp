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
    /// Логика взаимодействия для NewsPage.xaml
    /// </summary>
    public partial class NewsPage : Page
    {
        public NewsPage()
        {
            InitializeComponent();
            LViewNews.ItemsSource = App.Context.News.ToList();
            if (App.AuthClient != null)
            {
                BtnAdd.Visibility = Visibility.Collapsed;
                BtnDelete.Visibility = Visibility.Collapsed;
                BtnEdit.Visibility = Visibility.Collapsed;
            }
        }
        private void UpdateList()
        {
            LViewNews.ItemsSource = App.Context.News.ToList();
        }
        private void LViewNews_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var value = LViewNews.SelectedItem != null;
            BtnEdit.IsEnabled = value;
            BtnDelete.IsEnabled = value;

        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (LViewNews.SelectedItem is News news)
            {
                NavigationService.Navigate(new NewsEditorPage(news));
            }
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (LViewNews.SelectedItem is News news)
            {
                if (MessageBox.Show("Вы действительно хотите удалить данную новость?", "Сообщение", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    App.Context.News.Remove(news);
                    App.Context.SaveChanges();
                    UpdateList();
                }
            }
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {

            NavigationService.Navigate(new NewsEditorPage(null));
            UpdateList();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            UpdateList();


        }
    }
}
