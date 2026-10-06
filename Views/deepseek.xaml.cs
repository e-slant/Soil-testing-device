using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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

namespace smartAgriculture.Views
{
    /// <summary>
    /// deepseek.xaml 的交互逻辑
    /// </summary>
    public partial class deepseek : UserControl
    {
        public deepseek()
        {
            InitializeComponent();
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            string url = "";
            HttpClient client = new HttpClient();
            string tmp = "";
            HttpResponseMessage content = await client.GetAsync(url);
            if(content.IsSuccessStatusCode)
            {
                tmp= await content.Content.ReadAsStringAsync();
            }
            else
            {
                tmp = "响应超时，请重新分析";
            }
            AIcontent.Text = tmp;
        }
    }
}
