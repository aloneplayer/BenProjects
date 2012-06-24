// -----------------------------------------------------------------------
// <copyright file="Product.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace WpfDataBinding.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.ComponentModel;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class Product : INotifyPropertyChanged
    {
        private int _price = 0;

        public string Name { get; set; }

        public int Price 
        {
            get
            {
                return _price;
            }

            set 
            {
                if (value != _price)
                {
                    _price = value;
                    Notify("Price");
                }
            }   
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void Notify(string PropName)
        {
            if (this.PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(PropName));
            }
        }
    }
}
