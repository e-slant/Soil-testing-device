using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace smartAgriculture.Models
{
    public class BooltoColor:IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool && (bool)value)
            {
                // 如果值为真，返回红色  
                return Brushes.Red;
            }
            else
            {
                // 如果值为假或不是布尔值，返回橙色  
                return Brushes.DarkOrange;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
            // 注意：通常不需要实现 ConvertBack，除非你需要在双向绑定中从目标属性转换回源属性。  
        }

    }
}
