using System;
using smartAgriculture.Base;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using smartAgriculture.Views;
using smartAgriculture.Models;
using MySqlX.XDevAPI;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Collections.ObjectModel;

namespace smartAgriculture.ViewModels
{


    namespace myProT1.ViewModels
    {

        
        public class mainWindowModel : NotifyBase
        {
            private string _CurrentUserName;

            public CommandBase menuItemCommand { get; set; }



            private object _pageContent;
     
            public object PageContent
            {
                get { return _pageContent; }
                set { SetProperty(ref _pageContent, value); }
            }

            private string _nowTime;
            public string currentTime
            {
                get { return _nowTime; }
                set
                {
                    _nowTime = value;
                    this.NotifyChanged();
                }
            }

            private string _statusString;
            public string statusString
            {
                get { return _statusString; }
                set
                {
                    _statusString = value;
                    this.NotifyChanged();
                }
            }

            private int myVar;

            public int MyProperty
            {
                get { return myVar; }
                set { myVar = value; }
            }

            public mainWindowModel()
            {


                new Task(() =>
                {
                    while (true)
                    {
                        System.Threading.Thread.Sleep(1000);
                        currentTime = DateTime.Now.ToString();
                        if (Models.SystemGlobalVariable.is485Connected) statusString = "通信已连接，数据正常！";
                        else statusString = "通信已关闭！";


                    }

                }).Start();
                menuItemCommand = new CommandBase();
                menuItemCommand.DoExecute = menuDo;
                PageContent = new systemMainWindow();
                alarmData alarmData = new alarmData();
            }

 
            private void menuDo(object obj)
            {
                if (obj == null) return;
                if (obj.ToString() == "1")
                    return;
                Type type = Type.GetType(obj.ToString());
                this.PageContent = (UIElement)Activator.CreateInstance(type);
               
            }
        }
    }

}
