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
    /// Логика взаимодействия для ClientEditorPage.xaml
    /// </summary>
    public partial class ClientEditorPage : Page
    {
        private Client _client;
        public ClientEditorPage(Client client)
        {
            InitializeComponent();
            _client = client;
            if (client != null)
            {
                TBoxName.Text = _client.Name;
                TBoxSurname.Text = _client.Surname;
                TBoxPatronymic.Text = _client.Patronymic;
                TBoxPhone.Text = _client.Phone;
                TBoxEmail.Text = _client.Email;
                TBoxSnils.Text = _client.Snils;
            }
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            var err = "";
            if (string.IsNullOrWhiteSpace(TBoxName.Text)) err += "Введите имя\n";
            if (string.IsNullOrWhiteSpace(TBoxSurname.Text)) err += "Введите фамилию\n";
            if (string.IsNullOrWhiteSpace(TBoxPatronymic.Text)) err += "Введите отчество\n";
            if (string.IsNullOrWhiteSpace(TBoxPhone.Text)) err += "Введите телефон\n";
            if (string.IsNullOrWhiteSpace(TBoxEmail.Text)) err += "Введите email\n";
            if (string.IsNullOrWhiteSpace(TBoxSnils.Text)) err += "Введите СНИЛС\n";

            if (err == "")
            {
                if (_client == null)
                    _client = new Client();

                _client.Name = TBoxName.Text;
                _client.Surname = TBoxSurname.Text;
                _client.Patronymic = TBoxPatronymic.Text;
                _client.Phone = TBoxPhone.Text;
                _client.Email = TBoxEmail.Text;
                _client.Snils = TBoxSnils.Text;

                if (_client.Id == 0)
                    App.Context.Client.Add(_client);

                App.Context.SaveChanges();
                NavigationService.GoBack();
            }
            else
            {
                MessageBox.Show(err, "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
