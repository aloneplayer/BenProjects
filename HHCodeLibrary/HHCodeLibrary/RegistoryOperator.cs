using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Win32;

namespace HHCodeLibrary
{
    public class RegistoryOperator
    {
        private const string RegPath = @"SOFTWARE\SPSS\DimensionNet\InstallOptions";
        private const string SSOKeyName = "EnableWindowsLogin";

        public void Demo()
        {
          string keyValue = string.Empty;
            RegistryKey rootKey = Registry.LocalMachine;
            rootKey = rootKey.OpenSubKey(RegPath);
            try
            {
                keyValue = rootKey.GetValue(SSOKeyName) as string;
            }
            catch(Exception ex) 
            {
            }
        }
    }
}
