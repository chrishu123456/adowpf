using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace adowpf
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {

        public App()
        {
            string connectionString = @"server=(localdb)\MSSQLLocalDB;database=Bieren;integrated security=true";
            Application.Current.Properties["Bieren2"] = connectionString;
        }
        
    }
}
