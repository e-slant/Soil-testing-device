using smartAgriculture.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace smartAgriculture.Models
{
    public class Alarm_information:NotifyBase
    {
        private string _historytime;
        public string historytime
        {
            get { return _historytime; }
            set { _historytime = value; this.NotifyChanged(); }
        }
        public string _manage;
        public string manage
        {
            get { return _manage; }
            set { _manage = value; this.NotifyChanged(); }
        }
    }
}
