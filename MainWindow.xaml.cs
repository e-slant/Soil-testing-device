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
using smartAgriculture.ViewModels;
using smartAgriculture.ViewModels.myProT1.ViewModels;
using smartAgriculture.Models;
using System.IO.Ports;
using System.Text.RegularExpressions;
using smartAgriculture.Properties;
using System.Data;
using smartAgriculture.Views;
using System.Windows.Threading;
using System.Timers;

namespace smartAgriculture
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {

        
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = new mainWindowModel();
            addr.Text = "1";
            //new Task(() =>
            //{
            //    while (true)
            //    {
            //       if(SystemGlobalVariable.currentAlarm.IsTemperatrueAlarm == true)
            //        {
            //            Dispatcher.Invoke(() =>
            //            {
            //            //this.alarm.Text ="temperature hi hi ";
            //            });

            //        }
            //        else
            //        {
            //            Dispatcher.Invoke(() =>
            //            {
            //                this.alarm.Text = " normal";
            //            });
            //        }



            //    }

            //}).Start();
            SetTimer();

        }
        public static Timer timer;
        private static void SetTimer()
        {
            // 创建一个2分钟触发一次的定时器（先设置为2分钟以测试，之后可以改为1分钟）  
            timer = new Timer(60000); // 60000毫秒 = 1分钟  

            // Hook up the Elapsed event for the timer.   
            timer.Elapsed += OnTimedEvent;

            timer.Enabled = true;
        }
        private static void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            DBHelper db = new DBHelper(SystemGlobalVariable.mysqlconnstr);
            db.InsertMultipleRecords(SystemGlobalRun.historydata);
            SystemGlobalRun.historydata.Clear();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            App.Current.Shutdown();
            timer.Stop();
            timer.Dispose();
        }

        private void mainView_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }
}
