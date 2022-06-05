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
    /// Логика взаимодействия для AuthCitizenPage.xaml
    /// </summary>
    public partial class AuthCitizenPage : Page
    {
        public AuthCitizenPage()
        {
            InitializeComponent();
        }

        private void BtnLogIn_Click(object sender, RoutedEventArgs e)
        {
            if (App.Context.Client.ToList().FirstOrDefault(x => x.Snils == TBoxSnils.Text) is Client client)
            {
                App.AuthClient = client;
                NavigationService.Navigate(new MenuPage());
            }
            else
            {
                MessageBox.Show("Жителя с таким СНИЛСом нет в базе", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
