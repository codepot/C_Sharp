﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace TripLog.Wpf.Converters
{
    public class BooleanToTranslucentConverter : IValueConverter
    {
        #region IValueConverter Members

        public object Convert(object value, Type targetType,
            object parameter, System.Globalization.CultureInfo culture)
        {
            if (value.GetType() == typeof(bool) && (bool)value)
                return 0.25;
            return 1.0;
        }

        public object ConvertBack(object value, Type targetType,
            object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}

