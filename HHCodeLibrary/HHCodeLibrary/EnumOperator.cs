using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HHCodeLibrary
{
    public class EnumOperator
    {
        /// <summary>
        /// Using Enum.Parse
        /// </summary>
        public void StringToEnum()
        {
            DayOfWeek wednesday = (DayOfWeek)Enum.Parse(typeof(DayOfWeek), "Wednesday");

        }

        public void IntToEnum()
        {
            DayOfWeek wednesday = (DayOfWeek)Enum.ToObject(typeof(DayOfWeek), 3);
        }

        public void IntToString()
        {
            string value = ((DayOfWeek)Enum.ToObject(typeof(DayOfWeek), 3)).ToString();
        }

        public void EnumToInt()
        {
            int value = (int)DayOfWeek.Friday;
        }

        public void EnumToString()
        {
            string value = DayOfWeek.Friday.ToString();
        }

        public void BindEnumToComboBox()
        {
            foreach (int day in Enum.GetValues(typeof(DayOfWeek)))
            {
                string dayText = ((DayOfWeek)Enum.ToObject(typeof(DayOfWeek), day)).ToString();

                //this.comboBox_ServerType.Items.Add(dayText);

            }
            //Get value from combo
            //ComboBox.Items(ComboBox.SelectedIndex).value
        }
        private void MockFunciton()
        {

        }
    }
}
