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
    /// Логика взаимодействия для UserEditorPage.xaml
    /// </summary>
    public partial class UserEditorPage : Page
    {
        private User _user;
        public UserEditorPage(User user)
        {
            InitializeComponent();
            CBoxRole.ItemsSource = App.Context.Role.ToList();
            if (user != null)
            {
                TBoxName.Text = user.Name;
                TBoxSurname.Text = user.Surname;
                TBoxPatronymic.Text = user.Patronymic;
                TBoxPassword.Text = user.Password;
                TBoxLogin.Text = user.Login;
                TBoxPassport.Text = user.Passport;
                CBoxRole.Text = user.Role.Name;
            }

            _user = user;
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var err = "";
                if (string.IsNullOrWhiteSpace(TBoxName.Text)) err += "Заполните поле Имя\n";
                if (string.IsNullOrWhiteSpace(TBoxSurname.Text)) err += "Заполните поле Фамилия\n";
                if (string.IsNullOrWhiteSpace(TBoxPatronymic.Text)) err += "Заполните поле Отчество\n";
                if (string.IsNullOrWhiteSpace(CBoxRole.Text)) err += "Выберите должность\n";
                if (string.IsNullOrWhiteSpace(TBoxLogin.Text)) err += "Заполните поле Логин\n";
                if (string.IsNullOrWhiteSpace(TBoxPassword.Text)) err += "Заполните поле Пароль\n";
                if (string.IsNullOrWhiteSpace(TBoxPassport.Text)) err += "Заполните поле Пароль\n";

                if (err == "")
                {
                    if (_user == null)
                    {
                        try
                        {
                            _user = new User()
                            {
                                Name = TBoxName.Text,
                                Surname = TBoxSurname.Text,
                                Patronymic = TBoxPatronymic.Text,
                                Password = TBoxPassword.Text,
                                Login = TBoxLogin.Text,
                                Passport = TBoxPassport.Text,
                                RoleId = (CBoxRole.SelectedItem as Role).Id,

                            };
                            App.Context.User.Add(_user);
                            App.Context.SaveChanges();
                            NavigationService.GoBack();
                        }
                        catch (Exception)
                        {
                            MessageBox.Show("Произошла ошибка. Проверьте правильность заполнения полей.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                        }


                    }
                    else
                    {
                        _user.Name = TBoxName.Text;
                        _user.Surname = TBoxSurname.Text;
                        _user.Patronymic = TBoxPatronymic.Text;
                        _user.Login = TBoxLogin.Text;
                        _user.Password = TBoxPassword.Text;
                        _user.Passport = TBoxPassport.Text;
                        _user.RoleId = (CBoxRole.SelectedItem as Role).Id;
                        App.Context.SaveChanges();
                        NavigationService.GoBack();

                    }
                }
                else
                {
                    MessageBox.Show(err, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);

                }
            }
            catch
            {
                _ = MessageBox.Show("Произошла ошибка. Проверьте правильность заполнения полей.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
