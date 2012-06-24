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
    public partial class ValueConverter : Window
    {
        private Person _person;

        public ValueConverter()
        {
            InitializeComponent();
        }

        private void button_SetNewValue_Click(object sender, RoutedEventArgs e)
        {
            _person = (Person)this.FindResource("Tom");
            _person.Name = "New Tom";
            _person.Age = 99;
        }
    }
}
