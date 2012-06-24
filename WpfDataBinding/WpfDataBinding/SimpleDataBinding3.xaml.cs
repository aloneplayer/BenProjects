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
using WpfDataBinding.Models;
using System.ComponentModel;

namespace WpfDataBinding
{
    /// <summary>
    /// Interaction logic for DeomWithoutDataBinding.xaml
    /// </summary>
    public partial class SimpleDataBinding3 : Window
    {
        private Person _person;

        public SimpleDataBinding3()
        {
            InitializeComponent();
            _person = new Person() { Name = "The old", Age = 100 };
            this.DataContext = _person;
        }

        private void button_SetNewValue_Click(object sender, RoutedEventArgs e)
        {
            _person.Name = "Tom";
            _person.Age = 99;
        }
    }
}
