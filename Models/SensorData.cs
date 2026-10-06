using Google.Protobuf.WellKnownTypes;
using smartAgriculture.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace smartAgriculture.Models
{
    public class SensorData : NotifyBase
    {
        private string _No;
        private float _PhContent;
        private float _WaterContent;
        //private float  _Conductivity;
        private float _Temperatrue;
        private float _Nitrogen;
        private float _Phosphorus;
        private float _Potassium;
        private float _room_temperature;
        private DateTime _GatherTime;
        public float PhContent
        {
            get { return _PhContent; }
            set { _PhContent = value; this.NotifyChanged(); }
        }
        public float WaterContent
        {
            get { return _WaterContent; }
            set { _WaterContent = value; this.NotifyChanged(); }
        }
        //public float Conductivity
        //{
        //    get { return _Conductivity; }
        //    set { _Conductivity = value; this.NotifyChanged(); }
        //}
        public float Temperatrue
        {
            get { return _Temperatrue; }
            set { _Temperatrue = value; this.NotifyChanged(); }
        }
        public float Nitrogen
        {
            get { return _Nitrogen; }
            set { _Nitrogen = value; this.NotifyChanged(); }
        }
        public float Phosphorus
        {
            get { return _Phosphorus; }
            set { _Phosphorus = value; this.NotifyChanged(); }
        }
        public float Potassium
        {
            get { return _Potassium; }
            set { _Potassium = value; this.NotifyChanged(); }
        }
        public float room_temperature
        {
            get { return _room_temperature; }
            set { _room_temperature = value; this.NotifyChanged(); }
        }
        public DateTime GatherTime
        {
            get { return _GatherTime; }
            set { _GatherTime = value; this.NotifyChanged(); }
        }
        public string No
        {
            get { return _No; }
            set { _No = value; this.NotifyChanged(); }
        }
    }

    public  class  SetAlarmData:NotifyBase
    {
        public SetAlarmData()
        {
            PhUpperLimit = 20f;
            PhLowerLimit = 0f;
            WaterContentUpperLimit =0;
            WaterContentLowerLimit =80;
            TemperatrueUpperLimit =30;
            TemperatrueLowerLimit =0;
            NitrogenUpperLimit =300;
            NitrogenLowerLimit =0;
            PhosphorusUpperLimit =300;
            PhosphorusLowerLimit =0;
            PotassiumUpperLimit =300;
            PotassiumLowerLimit =0;
            //room_temperatureUpperLimit =10;
            //room_temperatureLowerLimit =50;
        }

        // public  DateTime AlarmTime { get; set; }
        public float PhUpperLimit { get; set; }
        public float PhLowerLimit { get; set; }
        public float WaterContentUpperLimit { get; set; }
        public float WaterContentLowerLimit { get; set; }

        //public float  ConductivityUpperLimit { get; set; }
        //public float  ConductivityLowerLimit { get; set; }

        public float TemperatrueUpperLimit { get; set; }
        public float TemperatrueLowerLimit { get; set; }
        public float NitrogenUpperLimit { get; set; }
        public float NitrogenLowerLimit { get; set; }
        public float PhosphorusUpperLimit { get; set; }
        public float PhosphorusLowerLimit { get; set; }
        public float PotassiumUpperLimit { get; set; }
        public float PotassiumLowerLimit { get; set; }
        public float room_temperatureUpperLimit { get; set; }
        public float room_temperatureLowerLimit { get; set; }
        private ObservableCollection<Alarm_information> _Manages=new ObservableCollection<Alarm_information>();
        public  ObservableCollection<Alarm_information> Manages
        {
            get { return _Manages; }
            set { _Manages = value; this.NotifyChanged();
            }
        }


    }

    public class alarmData:NotifyBase
    {
        public  DateTime AlarmTime { get; set; }
        private bool _IsPhAlarm;
        //private string _combinedText= "当前无报警内容";
        private bool _IsWaterContentAlarm;
        //public bool IsCondutivityAlarm { get; set; }
        private bool _IsTemperatrueAlarm;
        private bool _IsNitrogenAlarm;
        private bool _IsPhosphorusAlarm;
        private bool _IsPotassiumAlarm;
        private bool _Isroom_temperatureAlarm;
        public bool IsPhAlarm
        {
            get { return _IsPhAlarm; }
            set { _IsPhAlarm = value; UpdateCombinedText(); this.NotifyChanged(); }
        }
        public bool IsWaterContentAlarm
        {
            get { return _IsWaterContentAlarm; }
            set { _IsWaterContentAlarm = value; UpdateCombinedText(); this.NotifyChanged(); }
        }
        public bool IsTemperatrueAlarm
        {
            get { return _IsTemperatrueAlarm; }
            set { _IsTemperatrueAlarm = value; UpdateCombinedText(); this.NotifyChanged(); }
        }
        public bool IsNitrogenAlarm
        {
            get { return _IsNitrogenAlarm; }
            set { _IsNitrogenAlarm = value; UpdateCombinedText(); this.NotifyChanged(); }
        }
        public bool IsPhosphorusAlarm
        {
            get { return _IsPhosphorusAlarm; }
            set { _IsPhosphorusAlarm = value; UpdateCombinedText(); this.NotifyChanged(); }
        }
        public bool IsPotassiumAlarm
        {
            get { return _IsPotassiumAlarm; }
            set { _IsPotassiumAlarm = value; UpdateCombinedText(); this.NotifyChanged(); }
        }
        public bool Isroom_temperatureAlarm
        {
            get { return _Isroom_temperatureAlarm; }
            set { _Isroom_temperatureAlarm = value; UpdateCombinedText(); this.NotifyChanged(); }
        }
        //public string CombinedText
        //{
        //    get { return _combinedText; }
        //    private set
        //    {
        //        _combinedText = value;
        //        this.NotifyChanged(nameof(CombinedText));
        //    }
        //}
        public string ScopeExceeded(float upper,float lower,float current)
        {
            string tmpstr = "";
            if (current - upper > 0)
            {
                tmpstr = $"上限已超出标准值{current - upper}";
                if (lower - current > 0)
                {
                    tmpstr = $"上限已超出标准值{current - upper}\n下限已低于标准值{lower - current}";
                }
                
            }
            if (lower - current > 0)
            {
                tmpstr = $"下限已低于标准值{lower-current}";
            }
            return tmpstr;
        }
        static int initial = 1;
        public async void UpdateCombinedText()
        {
            await Application.Current.Dispatcher.InvokeAsync(new Action(() =>
            {
                if (initial == 1 && (SystemGlobalVariable.currentAlarm.IsPhAlarm || SystemGlobalVariable.currentAlarm.IsWaterContentAlarm || SystemGlobalVariable.currentAlarm.IsTemperatrueAlarm || SystemGlobalVariable.currentAlarm.IsNitrogenAlarm || SystemGlobalVariable.currentAlarm.IsPhosphorusAlarm || SystemGlobalVariable.currentAlarm.IsPotassiumAlarm || SystemGlobalVariable.currentAlarm.Isroom_temperatureAlarm))
                {
                    initial++;
                    SystemGlobalVariable.setAlarmData.Manages.Clear();
                }
                List<bool> combotext = new List<bool> { SystemGlobalVariable.currentAlarm.IsPhAlarm, SystemGlobalVariable.currentAlarm.IsWaterContentAlarm, SystemGlobalVariable.currentAlarm.IsTemperatrueAlarm, SystemGlobalVariable.currentAlarm.IsNitrogenAlarm, SystemGlobalVariable.currentAlarm.IsPhosphorusAlarm, SystemGlobalVariable.currentAlarm.IsPotassiumAlarm, SystemGlobalVariable.currentAlarm.Isroom_temperatureAlarm };
                for (int i = 0; i < combotext.Count; i++)
                {
                    if (combotext[i] == true)
                    {
                        switch (i)
                        {
                            case 0:
                                SystemGlobalVariable.setAlarmData.Manages.Add(new Alarm_information
                                {
                                    historytime = DateTime.Now.ToString(),
                                    manage = "ph值超出标准范围\n"
                                    + ScopeExceeded(SystemGlobalVariable.setAlarmData.PhUpperLimit,
                                    SystemGlobalVariable.setAlarmData.PhLowerLimit,
                                    SystemGlobalVariable.currentSensorData.PhContent)
                                });
                                SystemGlobalVariable.dB.InsertAlarm("ph值超出标准范围\n" + ScopeExceeded(SystemGlobalVariable.setAlarmData.PhUpperLimit,
                                    SystemGlobalVariable.setAlarmData.PhLowerLimit,
                                    SystemGlobalVariable.currentSensorData.PhContent), DateTime.Now);
                                break;
                            case 1:
                                SystemGlobalVariable.setAlarmData.Manages.Add(new Alarm_information
                                {
                                    historytime = DateTime.Now.ToString(),
                                    manage = "水分超出标准范围\n"
                                    + ScopeExceeded(SystemGlobalVariable.setAlarmData.WaterContentUpperLimit,
                                    SystemGlobalVariable.setAlarmData.WaterContentLowerLimit,
                                    SystemGlobalVariable.currentSensorData.WaterContent)
                                });
                                SystemGlobalVariable.dB.InsertAlarm("水分超出标准范围\n" + ScopeExceeded(SystemGlobalVariable.setAlarmData.WaterContentUpperLimit,
                                    SystemGlobalVariable.setAlarmData.WaterContentLowerLimit,
                                    SystemGlobalVariable.currentSensorData.WaterContent), DateTime.Now);
                                break;
                            case 2:
                                SystemGlobalVariable.setAlarmData.Manages.Add(new Alarm_information
                                {
                                    historytime = DateTime.Now.ToString(),
                                    manage = "温度超出标准范围\n"
                                    + ScopeExceeded(SystemGlobalVariable.setAlarmData.TemperatrueUpperLimit,
                                    SystemGlobalVariable.setAlarmData.TemperatrueLowerLimit,
                                    SystemGlobalVariable.currentSensorData.Temperatrue)
                                });
                                SystemGlobalVariable.dB.InsertAlarm("温度超出标准范围\n" + ScopeExceeded(SystemGlobalVariable.setAlarmData.TemperatrueUpperLimit,
                                    SystemGlobalVariable.setAlarmData.TemperatrueLowerLimit,
                                    SystemGlobalVariable.currentSensorData.Temperatrue), DateTime.Now);

                                break;
                            case 3:
                                SystemGlobalVariable.setAlarmData.Manages.Add(new Alarm_information
                                {
                                    historytime = DateTime.Now.ToString(),
                                    manage = "氮超出标准范围\n"
                                    + ScopeExceeded(SystemGlobalVariable.setAlarmData.NitrogenUpperLimit,
                                    SystemGlobalVariable.setAlarmData.NitrogenLowerLimit,
                                    SystemGlobalVariable.currentSensorData.Nitrogen)
                                });
                                SystemGlobalVariable.dB.InsertAlarm("氮超出标准范围\n" + ScopeExceeded(SystemGlobalVariable.setAlarmData.NitrogenUpperLimit,
                                    SystemGlobalVariable.setAlarmData.NitrogenLowerLimit,
                                    SystemGlobalVariable.currentSensorData.Nitrogen), DateTime.Now);
                                break;
                            case 4:
                                SystemGlobalVariable.setAlarmData.Manages.Add(new Alarm_information
                                {
                                    historytime = DateTime.Now.ToString(),
                                    manage = "磷超出标准范围\n"
                                    + ScopeExceeded(SystemGlobalVariable.setAlarmData.PhosphorusUpperLimit,
                                    SystemGlobalVariable.setAlarmData.PhosphorusLowerLimit,
                                    SystemGlobalVariable.currentSensorData.Phosphorus)
                                });
                                SystemGlobalVariable.dB.InsertAlarm("磷超出标准范围\n" + ScopeExceeded(SystemGlobalVariable.setAlarmData.PhosphorusUpperLimit,
                                    SystemGlobalVariable.setAlarmData.PhosphorusLowerLimit,
                                    SystemGlobalVariable.currentSensorData.Phosphorus), DateTime.Now);
                                break;
                            case 5:
                                SystemGlobalVariable.setAlarmData.Manages.Add(new Alarm_information
                                {
                                    historytime = DateTime.Now.ToString(),
                                    manage = "钾超出标准范围\n"
                                    + ScopeExceeded(SystemGlobalVariable.setAlarmData.PotassiumUpperLimit,
                                    SystemGlobalVariable.setAlarmData.PotassiumLowerLimit,
                                    SystemGlobalVariable.currentSensorData.Potassium)
                                });
                                SystemGlobalVariable.dB.InsertAlarm("钾超出标准范围\n" + ScopeExceeded(SystemGlobalVariable.setAlarmData.PotassiumUpperLimit,
                                    SystemGlobalVariable.setAlarmData.PotassiumLowerLimit,
                                    SystemGlobalVariable.currentSensorData.Potassium), DateTime.Now);
                                break;
                            default:
                                //SystemGlobalVariable.setAlarmData.Manages.Add(new Alarm_information
                                //{
                                //    historytime = DateTime.Now.ToString(),
                                //    manage = "室温超出标准范围\n"
                                //    + ScopeExceeded(SystemGlobalVariable.setAlarmData.room_temperatureUpperLimit,
                                //    SystemGlobalVariable.setAlarmData.room_temperatureLowerLimit,
                                //    SystemGlobalVariable.currentSensorData.room_temperature)
                                //});
                                //SystemGlobalVariable.dB.InsertAlarm("室温超出标准范围\n" + ScopeExceeded(SystemGlobalVariable.setAlarmData.room_temperatureUpperLimit,
                                //    SystemGlobalVariable.setAlarmData.room_temperatureLowerLimit,
                                //    SystemGlobalVariable.currentSensorData.room_temperature), DateTime.Now);
                                break;
                        }
                    }
                }
            }));


        }
    }
}
