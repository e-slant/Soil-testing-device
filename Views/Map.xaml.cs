using GMap.NET;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using smartAgriculture.Controls;
using GMap.NET.WindowsPresentation;
using GMap.NET.MapProviders;

namespace smartAgriculture.Views
{
    /// <summary>
    /// Map.xaml 的交互逻辑
    /// </summary>
    public partial class Map : UserControl
    {//39.585557,118.091202
        public Map()
        {
            InitializeComponent();
        }


        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {

            mapControl.CacheLocation = Environment.CurrentDirectory + "\\GMapCache\\"; //缓存位置
            mapControl.MapProvider = GMapProviders.GoogleChinaMap; // 这里需要填写地图提供商，官方默认包含Google地图
            mapControl.MinZoom = 2;  //最小缩放
            mapControl.MaxZoom = 17; //最大缩放
            mapControl.Zoom = 5;     //当前缩放 
            mapControl.ShowCenter = true; //不显示中心十字点
            mapControl.DragButton = MouseButton.Left; //左键拖拽地图
            mapControl.Position = new PointLatLng(32.064, 118.704); //地图中心位置：珠  海

            // control.MouseLeftButtonDown += new MouseButtonEventHandler(mapControl_MouseLeftButtonDown); // 点击事件可以自由选择


        }
    }
}
