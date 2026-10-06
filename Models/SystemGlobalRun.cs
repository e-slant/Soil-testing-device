using smartAgriculture.SerialPort485;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using LiveCharts;
using LiveCharts.Defaults;
using MySql.Data.MySqlClient;



namespace smartAgriculture.Models
{
    public  class SystemGlobalRun
    {
        static Task mainTask = null;
        static bool isRunning = true;
        static Rtu485 rtu485 = null;
        SystemGlobalVariable sgv = new SystemGlobalVariable();
        
        //此函数负责485数据解析任务
        public static List<SensorData> historydata= new List<SensorData>();

        public  static void Parsing485Data( List<byte> byteList)
        {
            
            //if (byteList != null && byteList.Count > 0)
            if(true)
            {
                //解析  //将解析好的数据赋值给全局数据结构
                SystemGlobalVariable.currentSensorData.No = ((short)byteList[0]).ToString();
                SystemGlobalVariable.currentSensorData.Temperatrue = (short)((byteList[5] << 8) | byteList[6]) / 10.0f;
                SystemGlobalVariable.currentSensorData.WaterContent = (short)((byteList[3] << 8) | byteList[4]) / 10.0f;
                SystemGlobalVariable.currentSensorData.PhContent = (short)((byteList[9] << 8) | byteList[10]) / 10.0f;
                SystemGlobalVariable.currentSensorData.Nitrogen = (short)((byteList[16] << 8) | byteList[17]);
                SystemGlobalVariable.currentSensorData.Phosphorus = (short)((byteList[18] << 8) | byteList[19]) ;
                SystemGlobalVariable.currentSensorData.Potassium = (short)((byteList[20] << 8) | byteList[21]) ;
                //SystemGlobalVariable.currentSensorData.room_temperature = (short)((byteList[28] << 8) | byteList[29]) / 10.0f;
                //SystemGlobalVariable.currentSensorData.Conductivity = byteList[7] * 256 + byteList[8];
                historydata.Add(SystemGlobalVariable.currentSensorData);
                if (SystemGlobalVariable.WaterContentValues.Count > 24) SystemGlobalVariable.WaterContentValues.RemoveAt(0);
                SystemGlobalVariable.WaterContentValues.Add(new ObservableValue((double )SystemGlobalVariable.currentSensorData.WaterContent));
                if (SystemGlobalVariable.TemperatrueValues.Count > 24) SystemGlobalVariable.TemperatrueValues.RemoveAt(0);
                SystemGlobalVariable.TemperatrueValues.Add(new ObservableValue((double)SystemGlobalVariable.currentSensorData.Temperatrue));
                if (SystemGlobalVariable.PhContentValues.Count > 24) SystemGlobalVariable.PhContentValues.RemoveAt(0);
                SystemGlobalVariable.PhContentValues.Add(new ObservableValue(SystemGlobalVariable.currentSensorData.PhContent));

                if (SystemGlobalVariable.NitrogenValues.Count > 24) SystemGlobalVariable.NitrogenValues.RemoveAt(0);
                SystemGlobalVariable.NitrogenValues.Add(new ObservableValue((double)SystemGlobalVariable.currentSensorData.Nitrogen));

                if (SystemGlobalVariable.PhosphorusValues.Count > 24) SystemGlobalVariable.PhosphorusValues.RemoveAt(0);
                SystemGlobalVariable.PhosphorusValues.Add(new ObservableValue(SystemGlobalVariable.currentSensorData.Phosphorus));

                if (SystemGlobalVariable.PotassiumValues.Count > 24) SystemGlobalVariable.PotassiumValues.RemoveAt(0);
                SystemGlobalVariable.PotassiumValues.Add(new ObservableValue(SystemGlobalVariable.currentSensorData.Potassium));

                //if (SystemGlobalVariable.room_temperatureValues.Count > 24) SystemGlobalVariable.room_temperatureValues.RemoveAt(0);
                //SystemGlobalVariable.room_temperatureValues.Add(new ObservableValue(SystemGlobalVariable.currentSensorData.room_temperature));
                //if (SystemGlobalVariable.ConductivityValues.Count > 24) SystemGlobalVariable.ConductivityValues.RemoveAt(0);
                //SystemGlobalVariable.ConductivityValues.Add(new ObservableValue(SystemGlobalVariable.currentSensorData.Conductivity));

                //
                if (SystemGlobalVariable.setAlarmData.TemperatrueLowerLimit > SystemGlobalVariable.currentSensorData.Temperatrue ||
                    SystemGlobalVariable.currentSensorData.Temperatrue > SystemGlobalVariable.setAlarmData.TemperatrueUpperLimit
                    )
                    SystemGlobalVariable.currentAlarm.IsTemperatrueAlarm = true;
                

                else
                    SystemGlobalVariable.currentAlarm.IsTemperatrueAlarm = false;

                if (SystemGlobalVariable.setAlarmData.WaterContentLowerLimit > SystemGlobalVariable.currentSensorData.WaterContent ||
    SystemGlobalVariable.currentSensorData.WaterContent > SystemGlobalVariable.setAlarmData.WaterContentUpperLimit
    )
                    SystemGlobalVariable.currentAlarm.IsWaterContentAlarm = true;
                else
                    SystemGlobalVariable.currentAlarm.IsWaterContentAlarm = false;

                if (SystemGlobalVariable.setAlarmData.PhLowerLimit > SystemGlobalVariable.currentSensorData.PhContent ||
    SystemGlobalVariable.currentSensorData.PhContent > SystemGlobalVariable.setAlarmData.PhUpperLimit
    )
                    SystemGlobalVariable.currentAlarm.IsPhAlarm = true;
                else
                    SystemGlobalVariable.currentAlarm.IsPhAlarm = false;
                if (SystemGlobalVariable.setAlarmData.NitrogenLowerLimit > SystemGlobalVariable.currentSensorData.Nitrogen ||
    SystemGlobalVariable.currentSensorData.Nitrogen > SystemGlobalVariable.setAlarmData.NitrogenUpperLimit
    )
                    SystemGlobalVariable.currentAlarm.IsNitrogenAlarm = true;
                else
                    SystemGlobalVariable.currentAlarm.IsNitrogenAlarm = false;

                if (SystemGlobalVariable.setAlarmData.PhosphorusLowerLimit > SystemGlobalVariable.currentSensorData.Phosphorus ||
    SystemGlobalVariable.currentSensorData.Phosphorus > SystemGlobalVariable.setAlarmData.PhosphorusUpperLimit
    )
                    SystemGlobalVariable.currentAlarm.IsPhosphorusAlarm = true;
                else
                    SystemGlobalVariable.currentAlarm.IsPhosphorusAlarm = false;

                if (SystemGlobalVariable.setAlarmData.PotassiumLowerLimit > SystemGlobalVariable.currentSensorData.Potassium ||
    SystemGlobalVariable.currentSensorData.Potassium > SystemGlobalVariable.setAlarmData.PotassiumUpperLimit
    )
                    SystemGlobalVariable.currentAlarm.IsPotassiumAlarm = true;
                else
                    SystemGlobalVariable.currentAlarm.IsPotassiumAlarm = false;
                //            if (SystemGlobalVariable.setAlarmData.room_temperatureLowerLimit > SystemGlobalVariable.currentSensorData.room_temperature ||
                //SystemGlobalVariable.currentSensorData.room_temperature > SystemGlobalVariable.setAlarmData.room_temperatureUpperLimit
                //)
                //                SystemGlobalVariable.currentAlarm.Isroom_temperatureAlarm = true;
                //            else
                //                SystemGlobalVariable.currentAlarm.Isroom_temperatureAlarm = false;

            }


        }

        public  static async Task sendSetAlarmData()
        {  
            //while (true)
            //{
                     if (SystemGlobalVariable.Is485SendFlag == false)
                                    {
                                        SystemGlobalVariable.Is485SendFlag = true;
                                      SystemGlobalVariable.my485.Send(
                                          (int)(SystemGlobalVariable.setAlarmData.WaterContentUpperLimit * 10),
                                              (int)(SystemGlobalVariable.setAlarmData.WaterContentLowerLimit * 10),
                                               (int)(SystemGlobalVariable.setAlarmData.TemperatrueUpperLimit * 10),
                                               (int)(SystemGlobalVariable.setAlarmData.TemperatrueLowerLimit * 10),
                                              //(int)(SystemGlobalVariable.setAlarmData.ConductivityLowerLimit * 10),
                                              //(int)(SystemGlobalVariable.setAlarmData.ConductivityUpperLimit * 10),
                                              (int)(SystemGlobalVariable.setAlarmData.PhUpperLimit * 10),
                                              (int)(SystemGlobalVariable.setAlarmData.PhLowerLimit * 10),
                                              (int)(SystemGlobalVariable.setAlarmData.NitrogenUpperLimit),
                                              (int)(SystemGlobalVariable.setAlarmData.NitrogenLowerLimit),
                                              (int)(SystemGlobalVariable.setAlarmData.PhosphorusUpperLimit),
                                              (int)(SystemGlobalVariable.setAlarmData.PhosphorusLowerLimit),
                                              (int)(SystemGlobalVariable.setAlarmData.PotassiumUpperLimit),
                                              (int)(SystemGlobalVariable.setAlarmData.PotassiumLowerLimit)
                                              , (int)(SystemGlobalVariable.setAlarmData.room_temperatureUpperLimit * 10),
                                              (int)(SystemGlobalVariable.setAlarmData.room_temperatureLowerLimit * 10)
                                            );
                                        SystemGlobalVariable.Is485SendFlag = false;
                                    }
            //}
             
               

             
        }

        public  static   void SystemRunStartup(Action  func)
        {
            mainTask = Task.Run(
               async () =>
               //() =>
                {
                    func();
                    //这里负责各种初始化
                    //SystemGlobalVariable.currentSensorData.WaterContent = 11.22f;

                    //SystemGlobalVariable.mySerialInfo.PortName = "COM4";
                    //SystemGlobalVariable.mySerialInfo.DataBit = 8;
                    //SystemGlobalVariable.mySerialInfo.Parity = System.IO.Ports.Parity.None;
                    //SystemGlobalVariable.mySerialInfo.StopBits = System.IO.Ports.StopBits.One;
                    //SystemGlobalVariable.mySerialInfo.BaudRate = 9600;
                    
                    SystemGlobalVariable.my485.ResponseData=Parsing485Data;
                    SystemGlobalVariable.my485.Connection();


                    //这里负责mysql初始化

                    
                    //send 485 data

                    //    await sendSetAlarmData();
                    while (true)
                    {
                        //SystemGlobalVariable.currentSensorData.WaterContent += 1;//test
                        if (SystemGlobalVariable.Is485SendFlag == true)
                        {
                            //SystemGlobalVariable.Is485SendFlag = true;
                            SystemGlobalVariable.my485.Send(
                                    (int)(SystemGlobalVariable.setAlarmData.WaterContentLowerLimit * 10),
                                    (int)(SystemGlobalVariable.setAlarmData.WaterContentUpperLimit * 10),
                                     (int)(SystemGlobalVariable.setAlarmData.TemperatrueLowerLimit * 10),
                                     (int)(SystemGlobalVariable.setAlarmData.TemperatrueUpperLimit * 10),
                                    //(int)(SystemGlobalVariable.setAlarmData.ConductivityLowerLimit * 10),
                                    //(int)(SystemGlobalVariable.setAlarmData.ConductivityUpperLimit * 10),
                                    (int)(SystemGlobalVariable.setAlarmData.PhLowerLimit * 10),
                                    (int)(SystemGlobalVariable.setAlarmData.PhUpperLimit * 10),
                                    (int)(SystemGlobalVariable.setAlarmData.NitrogenLowerLimit),
                                    (int)(SystemGlobalVariable.setAlarmData.NitrogenUpperLimit),
                                    (int)(SystemGlobalVariable.setAlarmData.PhosphorusLowerLimit),
                                    (int)(SystemGlobalVariable.setAlarmData.PhosphorusUpperLimit),
                                    (int)(SystemGlobalVariable.setAlarmData.PotassiumLowerLimit),
                                    (int)(SystemGlobalVariable.setAlarmData.PotassiumUpperLimit),
                                    (int)(SystemGlobalVariable.setAlarmData.room_temperatureLowerLimit * 10),
                                    (int)(SystemGlobalVariable.setAlarmData.room_temperatureUpperLimit * 10)
                                  );
                            SystemGlobalVariable.Is485SendFlag = false;



                        }

                    }
                }
               );
            //mainwindow
           

        }
    }
}
