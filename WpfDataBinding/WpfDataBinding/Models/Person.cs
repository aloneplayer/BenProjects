// -----------------------------------------------------------------------
// <copyright file="Person.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

using System.ComponentModel;  //INotifyPropertyChanged


namespace WpfDataBinding.Models
{
    /// <summary>
    /// 当对象发生改变的时候，向外通知， 
    /// </summary>
    class Person : INotifyPropertyChanged
    {
        private string _name;
        private int _Age;

        public event PropertyChangedEventHandler PropertyChanged;


        protected void Notify(string PropName)
        {
            if (this.PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(PropName));
            }
        }

        public Person()
        {
            _Age = 0;
            _name = "Null";
        }

        public string Name
        {
            get
            { return _name; }
            set
            {
                if (value == _name)
                { return; }
                _name = value;//注意：不能用this.Name来赋值，如果这样形成循环调用，栈溢出
                Notify("Name");
            }
        }

        public int Age
        {
            get
            { return _Age; }
            set
            {
                if (value == _Age) return;
                _Age = value;
                Notify("Age");
            }
        }

    }
}
