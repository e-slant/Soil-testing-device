using smartAgriculture.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace smartAgriculture.Models
{
    public class GridData : NotifyBase
    {
        public  int _pageSize { get; set; }
        public int _currentPage { get; set; }
        public  int _totalPage { get; set; }
        public  ObservableCollection<History> _histories { get; set; }
        public int pageSize
        {
            get { return _pageSize; }
            set { _pageSize = value; this.NotifyChanged(); }
        }
        public int currentPage
        {
            get { return _currentPage; }
            set { _currentPage = value; this.NotifyChanged(); }
        }
        public int totalPage
        {
            get { return _totalPage; }
            set { _totalPage = value; this.NotifyChanged(); }
        }
        public ObservableCollection<History> histories
        {
            get { return _histories; }
            set { _histories = value; this.NotifyChanged(); }
        }
    }
}
