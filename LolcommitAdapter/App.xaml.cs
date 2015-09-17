using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Owin;
using Microsoft.Owin.Hosting;

namespace LolcommitAdapter
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            WebApp.Start<Router>(url: "http://127.0.0.1:9000");
        }
    }
}
