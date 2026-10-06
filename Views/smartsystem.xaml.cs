using smartAgriculture.Models;
using SmartAgriculture;
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
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace smartAgriculture.Views
{
    /// <summary>
    /// smartsystem.xaml 的交互逻辑
    /// </summary>
    public partial class smartsystem : UserControl
    {
        suggest suggest;
        public smartsystem()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            float Temperatrue = SystemGlobalVariable.currentSensorData.Temperatrue;
            float WaterContent = SystemGlobalVariable.currentSensorData.WaterContent ;
            float PhContent = SystemGlobalVariable.currentSensorData.PhContent;
            float Nitrogen = SystemGlobalVariable.currentSensorData.Nitrogen ;
            float Phosphorus = SystemGlobalVariable.currentSensorData.Phosphorus ;
            float Potassium = SystemGlobalVariable.currentSensorData.Potassium ;
            Dictionary<string, float> dc = new Dictionary<string, float> {
                {"小麦",0},
                {"玉米",0 },
                { "大豆",0},
                { "棉花",0},
                {"稻谷",0},
                { "土豆",0},
                {"西红柿",0 },
                {"苹果",0 },
                {"葡萄",0 },
                {"油菜",0 }
            };
            for (int o = 0; o < dc.Count; o++)
            {
                string key = dc.Keys.ElementAt(o);
                var sampleData = new Cropforecast.ModelInput()
                {
                    Temperature = Temperatrue,
                    Moisture = WaterContent,
                    Nitrogen = Nitrogen,
                    Phosphorus = Phosphorus,
                    Potassium = Potassium,
                    PH = PhContent,
                    CropType = @key,
                };
                //Load model and predict output
                var result = Cropforecast.Predict(sampleData);
                dc[key] = result.Score;
            }
            var softresult = dc.OrderBy(kv => kv.Value);
            int i = 0;
            foreach (var item in softresult)
            {
                if (i == 5)
                {
                    break;
                }
                switch (i)
                {
                    case 0:
                        this.Crop1.Content = item.Key;
                        this.Crop1_Rate.Content = float.Parse(string.Format("{0:F2}", item.Value)) * 100;
                        break;
                    case 1:
                        this.Crop2.Content = item.Key;
                        this.Crop2_Rate.Content = float.Parse(string.Format("{0:F2}", item.Value)) * 100;
                        break;
                    case 2:
                        this.Crop3.Content = item.Key;
                        this.Crop3_Rate.Content = float.Parse(string.Format("{0:F2}", item.Value)) * 100;
                        break;
                    case 3:
                        this.Crop4.Content = item.Key;
                        this.Crop4_Rate.Content = float.Parse(string.Format("{0:F2}", item.Value)) * 100;
                        break;
                    case 4:
                        this.Crop5.Content = item.Key;
                        this.Crop5_Rate.Content = float.Parse(string.Format("{0:F2}", item.Value)) * 100;
                        break;
                        //case 0:
                        //    this.Crop1.Content = item.Key;
                        //    this.Crop1_Rate.Content = item.Value * 100;
                        //    break;
                        //case 1:
                        //    this.Crop2.Content = item.Key;
                        //    this.Crop2_Rate.Content = item.Value * 100;
                        //    break;
                        //case 2:
                        //    this.Crop3.Content = item.Key;
                        //    this.Crop3_Rate.Content = item.Value * 100;
                        //    break;
                        //case 3:
                        //    this.Crop4.Content = item.Key;
                        //    this.Crop4_Rate.Content = item.Value * 100;
                        //    break;
                        //case 4:
                        //    this.Crop5.Content = item.Key;
                        //    this.Crop5_Rate.Content = item.Value * 100;
                        //    break;
                        //case 0:
                        //    this.crop1.content = item.key;
                        //    this.crop1_rate.content = float.parse(string.format("{0:f2}", item.value * 10000));
                        //    break;
                        //case 1:
                        //    this.crop2.content = item.key;
                        //    this.crop2_rate.content = float.parse(string.format("{0:f2}", item.value * 10000)) ;
                        //    break;
                        //case 2:
                        //    this.crop3.content = item.key;
                        //    this.crop3_rate.content = float.parse(string.format("{0:f2}", item.value * 10000)) ;
                        //    break;
                        //case 3:
                        //    this.crop4.content = item.key;
                        //    this.crop4_rate.content = float.parse(string.format("{0:f2}", item.value * 10000)) ;
                        //    break;
                        //case 4:
                        //    this.crop5.content = item.key;
                        //    this.crop5_rate.content = float.parse(string.format("{0:f2}", item.value * 10000)) ;
                        //    break;
                }
                i++;
            }

            var sampleData1 = new Nitrogenforecast.ModelInput()
            {
                Date = DateTime.Now,
                CropType = crop.SelectedItem.ToString(),
                Temperature = Temperatrue,
                Moisture = WaterContent,
                Nitrogen = Nitrogen,
                PH = PhContent,
            };

            //Load model and predict output
            var result1 = Nitrogenforecast.Predict(sampleData1);
            this.NitrogenFertilizer.Content = string.Format("{0:F2}", result1.Score);
            //Load sample data
            var sampleData2 = new Phosphorusforecast.ModelInput()
            {
                Date = DateTime.Now,
                CropType = crop.SelectedItem.ToString(),
                Temperature = Temperatrue,
                Moisture = WaterContent,
                Phosphorus = Phosphorus,
                PH = PhContent,
            };

            //Load model and predict output
            var result2 = Phosphorusforecast.Predict(sampleData2);
            this.PhosphorusFertilizer.Content = string.Format("{0:F2}", result2.Score);
            //Load sample data
            var sampleData3 = new Potassiumforecast.ModelInput()
            {
                Date = DateTime.Now,
                CropType = crop.SelectedItem.ToString(),
                Temperature = Temperatrue,
                Moisture = WaterContent,
                Potassium = Potassium,
                PH = PhContent,
            };

            //Load model and predict output
            var result3 = Potassiumforecast.Predict(sampleData3);
            this.PotassiumFertilizer.Content = string.Format("{0:F2}", result3.Score);
            //Load sample data
            //Load sample data
            var sampleData4 = new Waterforecast.ModelInput()
            {
                Date = DateTime.Now,
                CropType = crop.SelectedItem.ToString(),
                Temperature = Temperatrue,
                Moisture = WaterContent,
                PH = PhContent,
            };

            //Load model and predict output
            var result4 = Waterforecast.Predict(sampleData4);
            this.WateringAmount.Content = string.Format("{0:F2}", result4.Score);
            //SystemGlobalVariable.currentAlarm.IsTemperatrueAlarm = true;
            

        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            suggest=new suggest();
            //NitrogenFertilizer
            //    PhosphorusFertilizer
            //    PotassiumFertilizer
            //    WateringAmount
        }
    }
}
