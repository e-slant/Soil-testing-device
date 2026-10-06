using smartAgriculture.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// HistoryQuery.xaml 的交互逻辑
    /// </summary>
    public partial class HistoryQuery : UserControl
    {
        public HistoryQuery()
        {
            InitializeComponent();
        }
        public static List<History> _alldata = new List<History>();
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                History history = new History();
                _alldata = new List<History>();
                history.GetFromDatabase(_alldata);
                if (_alldata.Count < 10)
                {
                    SystemGlobalVariable.histogram.histories = new ObservableCollection<History>(_alldata.GetRange(0, _alldata.Count));
                    datasource.ItemsSource = SystemGlobalVariable.histogram.histories;
                }
                else
                {
                    SystemGlobalVariable.histogram.histories = new ObservableCollection<History>(_alldata.GetRange(0, SystemGlobalVariable.histogram.pageSize));
                    datasource.ItemsSource = SystemGlobalVariable.histogram.histories;
                }
                SystemGlobalVariable.histogram.totalPage = (int)Math.Ceiling((double)_alldata.Count / SystemGlobalVariable.histogram.pageSize);
                totalPage.Text = (SystemGlobalVariable.histogram.totalPage+1).ToString();
            }catch (Exception ex)
            {
                MessageBox.Show("数据库连接失败\n"+ex.Message);
            }
        }
        public void GoToPage(int pageIndex)
        {
            if (pageIndex >= 0 && pageIndex < SystemGlobalVariable.histogram.totalPage)
            {
                SystemGlobalVariable.histogram.currentPage = pageIndex;
                currentPage.Text= (SystemGlobalVariable.histogram.currentPage+1).ToString();
                int startIndex = pageIndex * SystemGlobalVariable.histogram.pageSize;
                int endIndex = Math.Min(startIndex + SystemGlobalVariable.histogram.pageSize, _alldata.Count);
                SystemGlobalVariable.histogram.histories = new ObservableCollection<History>(_alldata.GetRange(startIndex, endIndex - startIndex));
            }
        }
        public void PreviousPage()
        {
            if (SystemGlobalVariable.histogram.currentPage > 0)
            {
                GoToPage(SystemGlobalVariable.histogram.currentPage - 1);
            }
        }
        public void NextPage()
        {
            if (SystemGlobalVariable.histogram.currentPage < SystemGlobalVariable.histogram.totalPage - 1)
            {
                GoToPage(SystemGlobalVariable.histogram.currentPage + 1);
            }
        }
        public void FirstPage()
        {
                GoToPage(0);
        }
        public void FinallyPage()
        {
            if (SystemGlobalVariable.histogram.currentPage < SystemGlobalVariable.histogram.totalPage - 1)
            {
                GoToPage(SystemGlobalVariable.histogram.totalPage - 1);
            }
        }
        private void TextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            PreviousPage();
        }

        private void TextBlock_MouseDown_1(object sender, MouseButtonEventArgs e)
        {
            NextPage();
        }

        private void TextBlock_MouseDown_2(object sender, MouseButtonEventArgs e)
        {
            FinallyPage();
        }

        private void TextBlock_MouseDown_3(object sender, MouseButtonEventArgs e)
        {
            FirstPage();
        }
    }
}
