using Microsoft.Win32;
using SelSovetApp.Entities;
using System;
using System.Collections.Generic;
using System.IO;
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
    /// Логика взаимодействия для NewsEditorPage.xaml
    /// </summary>
    public partial class NewsEditorPage : Page
    {
        public News _news;
        private byte[] _photo;

        public NewsEditorPage(News news)
        {
            InitializeComponent();
            _news = news;
            if (news != null)
            {
                _photo = _news.Image;
                ImgProduct.DataContext = _photo;
                TBoxName.Text = _news.Title;
                TBoxDescription.Text = _news.Description;
            }
        }

        private void BtnSelectPhoto_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Файлы изображений|*.png;*.jpg";
            if (dialog.ShowDialog() == true)
            {
                _photo = File.ReadAllBytes(dialog.FileName);
                ImgProduct.DataContext = _photo;
            }

        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            var err = "";
            if (string.IsNullOrWhiteSpace(TBoxName.Text)) err += "Введите название новости\n";
            if (string.IsNullOrWhiteSpace(TBoxDescription.Text)) err += "Введите текст новости\n";

            if (err == "")
            {
                if (_news == null)
                    _news = new News();

                _news.Description = TBoxDescription.Text;
                _news.Title = TBoxName.Text;
                _news.Image = _photo;
                _news.DateCreate = DateTime.Now.Date;
                _news.User = App.AuthUser;

                if (_news.Id == 0)
                    App.Context.News.Add(_news);

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
