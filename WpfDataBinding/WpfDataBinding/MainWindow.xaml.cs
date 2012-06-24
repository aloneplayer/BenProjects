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

namespace WpfDataBinding
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

        private void button_WithoutDataBinding_Click(object sender, RoutedEventArgs e)
        {
            DeomWithoutDataBinding win = new DeomWithoutDataBinding();
            win.Show();
        }

        private void button_SimpleDataBinding1_Click(object sender, RoutedEventArgs e)
        {
            SimpleDataBinding1 win = new SimpleDataBinding1();
            win.Show();
        }

        private void button_SimpleDataBinding2_Click(object sender, RoutedEventArgs e)
        {
            SimpleDataBinding2 win = new SimpleDataBinding2();
            win.Show();
        }

        private void button_SimpleDataBinding3_Click(object sender, RoutedEventArgs e)
        {
            SimpleDataBinding3 win = new SimpleDataBinding3();
            win.Show();
        }

        private void button_DataBindingToResource_Click(object sender, RoutedEventArgs e)
        {
            DataBindingToResource win = new DataBindingToResource();
            win.Show();
        }

        private void button_DataBindingToExplicitSource_Click(object sender, RoutedEventArgs e)
        {
            ExplicitDataSource win = new ExplicitDataSource();
            win.Show();
        }

        private void button_ValueConverter_Click(object sender, RoutedEventArgs e)
        {
            ValueConverter win = new ValueConverter();
            win.Show();
        }

        private void button_BindToList_Click(object sender, RoutedEventArgs e)
        {
            BindingToList win = new BindingToList();
            win.Show();
        }
    }
}
