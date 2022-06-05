using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace SelSovetApp
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static Entities.SelSovetDatabaseEntities Context { get; } = new Entities.SelSovetDatabaseEntities();
        public static Entities.User AuthUser { get; set; }
        public static Entities.Client AuthClient { get; set; }
    }
}
