using smartAgriculture.Models;
using smartAgriculture.SerialPort485;
using System;
using System.IO.Ports;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;


namespace smartAgriculture.Views
{
    /// <summary>
    /// systemConfiger.xaml 的交互逻辑
    /// </summary>
    public partial class systemConfiger : UserControl
    {

        public systemConfiger()
        {
            InitializeComponent();
            setPortName.ItemsSource=SerialPort.GetPortNames();
            setPortName.SelectedIndex=0;
        }

        private void sendConfig_Click(object sender, RoutedEventArgs e)
        {
            int num = 0;
            bool submit = true;
            foreach (UIElement i in page.Children)
            {
                if(i is StackPanel stack)
                {
                    
                    foreach(UIElement child in stack.Children)
                    {
                        if (child is TextBox textBox && num >= 8)
                        {
                            int test;
                            num++;
                            if (!int.TryParse(textBox.Text,out test))
                            {
                                submit = false;
                                MessageBox.Show("氮磷钾只支持整数参数");
                                break;
                            }
                        }
                        if (child is TextBox tb&&num<8)
                        {
                            num++;
                            if (!IsTextAllowed(tb.Text))
                            {
                                submit=false;
                                MessageBox.Show("水分，温度，室温，ph值只支持整数或小数点后保留一位的参数");
                                break;
                            }
                        }

                    }

                }
            }
            if (submit)
            {
                SystemGlobalVariable.setAlarmData.TemperatrueUpperLimit = float.Parse(this.TemperatrueUpperValue.Text.Trim());
                SystemGlobalVariable.setAlarmData.TemperatrueLowerLimit = float.Parse(this.TemperatrueLowerValue.Text.Trim());

                SystemGlobalVariable.setAlarmData.WaterContentUpperLimit = float.Parse(this.WaterUpperValue.Text.Trim());
                SystemGlobalVariable.setAlarmData.WaterContentLowerLimit = float.Parse(this.WaterLowerValue.Text.Trim());

                SystemGlobalVariable.setAlarmData.PhUpperLimit = float.Parse(this.PhUpperValue.Text.Trim());
                SystemGlobalVariable.setAlarmData.PhLowerLimit = float.Parse(this.PhLowerValue.Text.Trim());

                SystemGlobalVariable.setAlarmData.NitrogenUpperLimit = float.Parse(this.NitrogenUpperValue.Text.Trim());
                SystemGlobalVariable.setAlarmData.NitrogenLowerLimit = float.Parse(this.NitrogenLowerValue.Text.Trim());

                SystemGlobalVariable.setAlarmData.PhosphorusUpperLimit = float.Parse(this.PhosphorusUpperValue.Text.Trim());
                SystemGlobalVariable.setAlarmData.PhosphorusLowerLimit = float.Parse(this.PhosphorusLowerValue.Text.Trim());

                SystemGlobalVariable.setAlarmData.PotassiumUpperLimit = float.Parse(this.PotassiumUpperValue.Text.Trim());
                SystemGlobalVariable.setAlarmData.PotassiumLowerLimit = float.Parse(this.PotassiumLowerValue.Text.Trim());

                //SystemGlobalVariable.setAlarmData.room_temperatureUpperLimit = float.Parse(this.room_temperatureUpperValue.Text.Trim());
                //SystemGlobalVariable.setAlarmData.room_temperatureLowerLimit = float.Parse(this.room_temperatureLowerValue.Text.Trim());
                SystemGlobalVariable.Is485SendFlag = true;
                DBHelper dB = new DBHelper(SystemGlobalVariable.mysqlconnstr);
                dB.UpdatePage(SystemGlobalVariable.setAlarmData.TemperatrueUpperLimit, SystemGlobalVariable.setAlarmData.TemperatrueLowerLimit
                    , SystemGlobalVariable.setAlarmData.WaterContentUpperLimit, SystemGlobalVariable.setAlarmData.WaterContentLowerLimit
                    , SystemGlobalVariable.setAlarmData.PhUpperLimit, SystemGlobalVariable.setAlarmData.PhLowerLimit
                    , SystemGlobalVariable.setAlarmData.NitrogenUpperLimit, SystemGlobalVariable.setAlarmData.NitrogenLowerLimit
                    , SystemGlobalVariable.setAlarmData.PhosphorusUpperLimit, SystemGlobalVariable.setAlarmData.PhosphorusLowerLimit
                    , SystemGlobalVariable.setAlarmData.PotassiumUpperLimit, SystemGlobalVariable.setAlarmData.PotassiumLowerLimit);
                    //, SystemGlobalVariable.setAlarmData.room_temperatureUpperLimit, SystemGlobalVariable.setAlarmData.room_temperatureLowerLimit);
                MessageBox.Show("更新完成");
            }


          
        }

        private void open_Click(object sender, RoutedEventArgs e)
        {
            //if (Models.SystemGlobalVariable.is485Connected) Models.SystemGlobalVariable.my485.Dispose();
            //Models.SystemGlobalVariable.mySerialInfo.BaudRate =int.Parse( this.setBaudRate.Text.Trim());
            //Models.SystemGlobalVariable.mySerialInfo.Parity = System.IO.Ports.Parity.None;
            //Models.SystemGlobalVariable.mySerialInfo.DataBit =8;
            //Models.SystemGlobalVariable.mySerialInfo.StopBits =System.IO.Ports.StopBits.One ;
            //Models.SystemGlobalVariable.mySerialInfo.PortName =this.setPortName.Text.ToString().Trim() ;
            //Models.SystemGlobalVariable.my485.ResponseData = SystemGlobalRun.Parsing485Data;
            //Models.SystemGlobalVariable.my485.Connection();
            Openport();

        }
        private void Openport()//打开串口
        {
            try
            {

                //if (setPortName == null || setBaudRate == null || setDataBit == null || setParity == null || setStopBit == null)
                //{
                //    MessageBox.Show("某些控件尚未初始化或不可用。");
                //    return; // 或者进行其他错误处理  
                //}
                //串口参数填入
                SystemGlobalVariable.mySerialInfo.PortName = setPortName.SelectedItem.ToString();
                SystemGlobalVariable.mySerialInfo.BaudRate = Convert.ToInt32(setBaudRate.Text.Trim());

                switch (setStopBit.SelectedItem)
                {
                    case "1":
                        SystemGlobalVariable.mySerialInfo.StopBits = StopBits.One;
                        break;
                    case "1.5":
                        SystemGlobalVariable.mySerialInfo.StopBits = StopBits.OnePointFive;
                        break;
                    case "2":
                        SystemGlobalVariable.mySerialInfo.StopBits = StopBits.Two;
                        break;
                }
                //port2.PortName = "COM2";
                //port2.BaudRate = Convert.ToInt32(PortConfig.name2);
                //port2.DataBits = Convert.ToInt32(PortConfig.name3);
                //port2.Parity = Parity.None;
                //port2.StopBits = StopBits.One;
                //port2.Open();
                switch (setParity.SelectedItem)
                    {
                        case "None":
                        SystemGlobalVariable.mySerialInfo.Parity = Parity.None;
                            break;
                        case "Odd":
                        SystemGlobalVariable.mySerialInfo.Parity = Parity.Odd;
                            break;
                        case "Even":
                        SystemGlobalVariable.mySerialInfo.Parity = Parity.Even;
                            break;
                        case "Mark":
                        SystemGlobalVariable.mySerialInfo.Parity = Parity.Mark;
                            break;
                        default:
                        SystemGlobalVariable.mySerialInfo.Parity = Parity.Space;
                            break;
                    }
                    switch (setDataBit.SelectedItem)
                    {
                        case "8":
                        SystemGlobalVariable.mySerialInfo.DataBit = 8;
                            break;
                        case "7":
                        SystemGlobalVariable.mySerialInfo.DataBit = 7;
                            break;
                        case "6":
                        SystemGlobalVariable.mySerialInfo.DataBit = 6;
                            break;
                    case "5":
                        SystemGlobalVariable.mySerialInfo.DataBit = 5;
                        break;
                }
                if (SystemGlobalVariable.my485.Iscreate())
                {
                    SystemGlobalVariable.my485 = Rtu485.GetInstance(SystemGlobalVariable.mySerialInfo);
                }
                else
                {
                    SystemGlobalVariable.my485.UpSerialinfo(SystemGlobalVariable.mySerialInfo);
                }
                SystemGlobalVariable.my485.ResponseData = SystemGlobalRun.Parsing485Data;
                SystemGlobalVariable.my485.Connection();
                SystemGlobalRun.SystemRunStartup(
    () =>
    {
        Application.Current.Dispatcher.Invoke(() => { new MainWindow().Show(); });
    }
    );
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }


        private void off_Click(object sender, RoutedEventArgs e)
        {
            if (Models.SystemGlobalVariable.is485Connected) Models.SystemGlobalVariable.my485.Dispose();
        }
        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (textBox != null)
            {
                e.Handled = !IsTextAllowed(textBox.Text.Insert(textBox.CaretIndex, e.Text));
            }
        }
        
        
        private static readonly Regex IntegerOrDecimalRegex = new Regex("^-?(([0-9]*))(\\.[\\d]{1})?$");
        private bool IsTextAllowed(string text)
        {
            // 允许空字符串  
            if (string.IsNullOrEmpty(text.Trim()))
            {
                return true;
            }

            // 使用正则表达式检查文本是否符合整数或小数点后一位的格式  
            return IntegerOrDecimalRegex.IsMatch(text.Trim());
        }
    }
}
