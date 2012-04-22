using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfLibrary
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_ProductItemDemo_Click(object sender, RoutedEventArgs e)
        {
            NavigationWindow window = new NavigationWindow();
            window.Source = new Uri("ProductItemPage.xaml", UriKind.Relative);
            window.Show();  
        }

        private void Button_ProductListDemo_Click(object sender, RoutedEventArgs e)
        {
            NavigationWindow window = new NavigationWindow();
            window.Source = new Uri("ProductListPage.xaml", UriKind.Relative);
            window.Show();
        }
    }
}
