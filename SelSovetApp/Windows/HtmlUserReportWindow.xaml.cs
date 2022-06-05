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
using System.Windows.Shapes;
using mshtml;

namespace SelSovetApp.Windows
{
    /// <summary>
    /// Логика взаимодействия для HtmlUserReportWindow.xaml
    /// </summary>
    public partial class HtmlUserReportWindow : Window
    {
        public HtmlUserReportWindow(List<Entities.User> users)
        {
            InitializeComponent();
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("<html>");
            stringBuilder.Append("<meta http-equiv='Content-Type' content='text/html;charset=UTF-8'><head></head>");
            stringBuilder.Append("<body>");
            stringBuilder.Append($"<p align=\"center\"><b>Вышнереутчанский сельсовет</b></p>");
            stringBuilder.Append($"<p align=\"left\"><b> 307048, Курская область, Медвенский район, Вышнереутчанский сельсовет, с.Верхний Реутец</ b></p>");
            stringBuilder.Append($"<p align=\"center\"><b>Сведения о сотрудниках на {DateTime.Now.ToShortDateString()}</b></p>");
            stringBuilder.Append("<hr>");
            stringBuilder.Append("<table border=\"1\" align=\"center\">");
            stringBuilder.Append("<tr>");

            stringBuilder.Append("<th>");
            stringBuilder.Append("Фамилия");
            stringBuilder.Append("</th>");

            stringBuilder.Append("<th>");
            stringBuilder.Append("Имя");
            stringBuilder.Append("</th>");

            stringBuilder.Append("<th>");
            stringBuilder.Append("Отчество");
            stringBuilder.Append("</th>");


            stringBuilder.Append("<th>");
            stringBuilder.Append("Логин");
            stringBuilder.Append("</th>");


            stringBuilder.Append("<th>");
            stringBuilder.Append("Пароль");
            stringBuilder.Append("</th>");


            stringBuilder.Append("<th>");
            stringBuilder.Append("Должность");
            stringBuilder.Append("</th>");

            stringBuilder.Append("<th>");
            stringBuilder.Append("Паспорт");
            stringBuilder.Append("</th>");
            stringBuilder.Append("</tr>");
            foreach (var user in users)
            {
                stringBuilder.Append("<tr>");

                stringBuilder.Append("<td>");
                stringBuilder.Append(user.Surname);
                stringBuilder.Append("</td>");

                stringBuilder.Append("<td>");
                stringBuilder.Append(user.Name);
                stringBuilder.Append("</td>");

                stringBuilder.Append("<td>");
                stringBuilder.Append(user.Patronymic);
                stringBuilder.Append("</td>");

                stringBuilder.Append("<td>");
                stringBuilder.Append(user.Login);
                stringBuilder.Append("</td>");

                stringBuilder.Append("<td>");
                stringBuilder.Append(user.Password);
                stringBuilder.Append("</td>");


                stringBuilder.Append("<td>");
                stringBuilder.Append(user.Role.Name);
                stringBuilder.Append("</td>");

                stringBuilder.Append("<td>");
                stringBuilder.Append(user.Passport);
                stringBuilder.Append("</td>");
                stringBuilder.Append("</tr>");

            }
            stringBuilder.Append("</table>");
            stringBuilder.Append("<hr>");
            stringBuilder.Append("<br>");
            stringBuilder.Append($"<p align=\"center\"><b>Отчет сформирован сотрудником: {App.AuthUser.FullName}</b></p>");
            stringBuilder.Append("</body>");
            stringBuilder.Append("</html>");
            WBrowserOrder.NavigateToString(stringBuilder.ToString());
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var doc = WBrowserOrder.Document as IHTMLDocument2;
            doc.execCommand("Print");
        }
    }
}
