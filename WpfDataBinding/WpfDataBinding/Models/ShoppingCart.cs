// -----------------------------------------------------------------------
// <copyright file="ShoppingCart.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace WpfDataBinding.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Collections.ObjectModel;
    using System.Collections.Specialized;
    using Microsoft.Practices.Prism.ViewModel;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class ShoppingCart : NotificationObject
    {
        private ObservableCollection<Product> productList = new ObservableCollection<Product>();

        public ShoppingCart()
        {
            productList.CollectionChanged += OnCollectionChanged;
        }
        public ObservableCollection<Product> Products
        {
            get { return productList; }
        }

        public int PriceSummary
        {
            get
            {
                return productList.Select(item => item.Price).Sum();
            }
        }

        private void OnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
	    {
	        if (e.Action == NotifyCollectionChangedAction.Add)
	        {
	            foreach (Product product in e.NewItems)
	            {
                    product.PropertyChanged += (x, y) =>
	                {
                        RaisePropertyChanged("PriceSummary");
	                };
	            }
	        }
	    }
    }
}
