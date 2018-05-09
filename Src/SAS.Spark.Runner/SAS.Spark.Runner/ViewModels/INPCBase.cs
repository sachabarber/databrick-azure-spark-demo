using SAS.Spark.Runner.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq.Expressions;

namespace SAS.Spark.Runner.ViewModels
{
    public class INPCBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(PropertyChangedEventArgs args)
        {
            PropertyChanged?.Invoke(this, args);
        }

        //Used by code that may want to fire additional properties. For example
        //when there is only a getter available, but the UI still needs to be notified


        /// <summary>
        /// Used by code that may want to fire additional properties. For example
        /// when there is only a getter available, but the UI still needs to be n
        /// </summary>
        /// <param name="propertyName">Expression representing the property to use</param>
        public void RaisePropertyChanged<TValue>(Expression<Func<TValue>> propertySelector)
        {
            if (PropertyChanged != null)
            {
                NotifyPropertyChanged(new PropertyChangedEventArgs(propertySelector.GetPropertyName()));
            }
        }

        public TRet RaiseAndSetIfChanged<TRet, TValue>(ref TRet backingField, TRet newValue, Expression<Func<TValue>> propertySelector)
        {
            if (EqualityComparer<TRet>.Default.Equals(backingField, newValue))
            {
                return newValue;
            }

            backingField = newValue;
            RaisePropertyChanged(propertySelector);
            return newValue;
        }

        public TRet RaiseAndSet<TRet, TValue>(ref TRet backingField, TRet newValue, Expression<Func<TValue>> propertySelector)
        {
            backingField = newValue;
            RaisePropertyChanged(propertySelector);
            return newValue;
        }
    }
}
