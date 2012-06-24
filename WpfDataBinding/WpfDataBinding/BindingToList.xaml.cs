using System.Collections.ObjectModel;
using System.Windows;
using WpfDataBinding.Models;

namespace WpfDataBinding
{
    /// <summary>
    /// Interaction logic for DeomWithoutDataBinding.xaml
    /// </summary>
    public partial class BindingToList : Window
    {
        private ShoppingCart shoppingCart = new ShoppingCart();

        public BindingToList()
        {
            shoppingCart.Products.Add(new Product
            {
                Name = "Gun",
                Price = 100
            });

            shoppingCart.Products.Add(new Product
            {
                Name = "BigGun",
                Price = 200
            });
            InitializeComponent();
            this.DataContext = shoppingCart;
        }
    }
}
