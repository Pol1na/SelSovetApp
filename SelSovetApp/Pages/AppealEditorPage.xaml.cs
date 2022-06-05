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
    /// Логика взаимодействия для AppealEditorPage.xaml
    /// </summary>
    public partial class AppealEditorPage : Page
    {
        private Appeal _appeal;
        public AppealEditorPage(Appeal appeal)
        {
            InitializeComponent();
            _appeal = appeal;
            if (App.AuthClient != null)
            {
                SPClient.Visibility = Visibility.Collapsed;

                CBoxResult.IsEnabled = false;
                CBoxType.IsEnabled = false;
                DPickerEnd.IsEnabled = false;
                DPickerStart.SelectedDate = DateTime.Now.Date;
            }
            else {

                CBoxClient.Visibility = Visibility.Visible;
                CBoxUser.IsEditable = false;
            }
            CBoxType.ItemsSource = App.Context.Status.ToList();
            CBoxResult.ItemsSource = App.Context.Result.ToList();
            CBoxUser.ItemsSource = App.Context.User.ToList();
            CBoxClient.ItemsSource = App.Context.Client.ToList();
            if (appeal != null)
            {
                TBoxDescription.Text = _appeal.Text;
                CBoxType.Text = _appeal.Status.Name;
                CBoxUser.Text = _appeal.User.FullName;
                CBoxClient.Text = _appeal.Client.FullName;
                CBoxResult.Text = _appeal.Result.Name;
                DPickerEnd.SelectedDate = _appeal.DateEnd;
                DPickerStart.SelectedDate = _appeal.DateStart;
            }
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            var err = "";
            if (string.IsNullOrWhiteSpace(TBoxDescription.Text)) err += "Введите текст обращения\n";

            if (App.AuthClient !=null)
            {
                if (err == "")
                {
                    if (_appeal == null)
                    {
                        _appeal = new Appeal()
                        {
                            Text = TBoxDescription.Text,
                            StatusId = 1,
                            User = CBoxUser.SelectedItem as User,
                            Client = App.AuthClient,
                            ResultId = 3,
                            DateStart = DateTime.Now.Date,
                            DateEnd = DPickerStart.SelectedDate.Value.AddDays(7),

                        };
                        App.Context.Appeal.Add(_appeal);
                    }
                    else
                    {
                        _appeal.Text = TBoxDescription.Text;
                        _appeal.Status = CBoxType.SelectedItem as Status;
                        _appeal.User = CBoxUser.SelectedItem as User;
                        _appeal.Client = App.AuthClient;
                        _appeal.Result = CBoxResult.SelectedItem as Result;
                        _appeal.DateStart = DPickerStart.SelectedDate.Value;
                        _appeal.DateEnd = DPickerStart.SelectedDate.Value.AddDays(7);
                    }

                    App.Context.SaveChanges();
                    NavigationService.GoBack();
                }
                else
                {
                    MessageBox.Show(err, "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                if (err == "")
                {
                    if (_appeal == null)
                        _appeal = new Appeal();


                    _appeal.Text = TBoxDescription.Text;
                    _appeal.Status = CBoxType.SelectedItem as Status;
                    _appeal.User = CBoxUser.SelectedItem as User;
                    _appeal.Client = CBoxClient.SelectedItem as Client;
                    _appeal.Result = CBoxResult.SelectedItem as Result;
                    _appeal.DateStart = DPickerStart.SelectedDate.Value;
                    _appeal.DateEnd = DPickerEnd.SelectedDate.Value;

                    if (_appeal.Id == 0)
                        App.Context.Appeal.Add(_appeal);

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
}
