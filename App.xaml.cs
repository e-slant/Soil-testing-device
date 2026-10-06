using smartAgriculture.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace smartAgriculture
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        public static SystemGlobalVariable mySystemGlobalVariable = new SystemGlobalVariable();
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            SystemGlobalRun.SystemRunStartup(
                () =>
                {
                    Application.Current.Dispatcher.Invoke(() => { new MainWindow().Show(); });
                }
                );
        }
    }
}
