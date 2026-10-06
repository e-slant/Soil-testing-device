using LiveCharts;
using LiveCharts.Defaults;
using smartAgriculture.SerialPort485;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace smartAgriculture.Models
{
    public  class SystemGlobalVariable
    {
        public static string mysqlconnstr = "server=localhost;port=3306;database=soildata;uid=root;pwd=123456;Charset=utf8";
        public static bool is485Connected { get; set; }
        public  static bool Is485SendFlag { get; set; }
        public static  SensorData currentSensorData { get; set; }
        public static SetAlarmData setAlarmData { get; set; }
        public static Rtu485 my485 { get; set; }
        public static suggest smartsuggest { get; set; }
        public static SerialInfo mySerialInfo { get; set; }
        
        public static  ObservableCollection<SensorData> HistoricalSensorData { get; set; }
        public static ObservableCollection<alarmData> HistoricalAlarmData { get; set; }
        public static GridData histogram { get; set; }
        public static Pagefixes pagefixes { get; set; }
        public static alarmData currentAlarm { get; set; }
        public static ChartValues<ObservableValue> PhContentValues { get; set; }
        public static ChartValues<ObservableValue> WaterContentValues { get; set; }
        //public static ChartValues<ObservableValue> ConductivityValues { get; set; }
        public static ChartValues<ObservableValue> TemperatrueValues { get; set; }
        public static ChartValues<ObservableValue> NitrogenValues { get; set; }
        public static ChartValues<ObservableValue> PhosphorusValues { get; set; }
        public static ChartValues<ObservableValue> PotassiumValues { get; set; }
        public static ChartValues<ObservableValue> room_temperatureValues { get; set; }
        public static DBHelper dB {  get; set; }
        public SystemGlobalVariable()
        {
            pagefixes=new Pagefixes();

            TemperatrueValues = new ChartValues<ObservableValue>();
            //ConductivityValues  = new ChartValues<ObservableValue>();
            PhContentValues  = new ChartValues<ObservableValue>();
            WaterContentValues = new ChartValues<ObservableValue>();
            NitrogenValues = new ChartValues<ObservableValue>();
            PhosphorusValues = new ChartValues<ObservableValue>();
            PotassiumValues = new ChartValues<ObservableValue>();
            room_temperatureValues = new ChartValues<ObservableValue>();
            HistoricalAlarmData  =               new ObservableCollection<alarmData>();
            HistoricalSensorData  =    new ObservableCollection<SensorData>();
            histogram = new GridData();
            histogram.pageSize = 10;
            Is485SendFlag = false;
            is485Connected = false;
            mySerialInfo = new SerialInfo();
            my485 = Rtu485.GetInstance(mySerialInfo);
            setAlarmData = new SetAlarmData();
            setAlarmData.Manages.Add(new Alarm_information { manage="暂无报警信息"});
            currentSensorData = new SensorData();
            smartsuggest = new suggest();
            currentAlarm = new alarmData();
            currentSensorData.Potassium = 154;
            currentSensorData.Phosphorus = 161;
            currentSensorData.Nitrogen = 49;
            currentSensorData.WaterContent = 13;
            currentSensorData.Temperatrue = 20;
            currentSensorData.PhContent = 7;
            //currentSensorData.Temperatrue = 28;
            //currentSensorData.WaterContent = 0.5f;
            //currentSensorData.PhContent = 5.8f;
            //currentSensorData.Nitrogen = 1;
            //currentSensorData.Phosphorus = 0.4f;
            //currentSensorData.Potassium = 1.8f;
            dB = new DBHelper(mysqlconnstr);
            dB.LoadPage(setAlarmData);
        }
    }
}
