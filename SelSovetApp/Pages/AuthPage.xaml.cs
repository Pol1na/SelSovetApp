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
    /// Логика взаимодействия для AuthPage.xaml
    /// </summary>
    public partial class AuthPage : Page
    {
        public AuthPage()
        {
            InitializeComponent();
            App.AuthUser = null;
            App.AuthClient = null;
        }

        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(TBoxLogin.Text) && !string.IsNullOrEmpty(PBoxPassword.Password))
            {
                if (App.Context.User.ToList().FirstOrDefault(x => x.Login == TBoxLogin.Text && x.Password == PBoxPassword.Password) is User user)
                {
                    App.AuthUser = user;
                    NavigationService.Navigate(new MenuPage());
                }
                else
                {
                    MessageBox.Show("Неверный логин или пароль.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Заполните оба поля.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BtnForCitizens_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AuthCitizenPage());
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            App.AuthUser = null;
            App.AuthClient = null;
        }
    }
}
