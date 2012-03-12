using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BenCspTest
{
    class ExceptionTest
    {
        public static void Test()
        {
            ExceptionTest test = new ExceptionTest();
            try
            {
                test.HandleException2();
            }
            catch (Exception exp)
            {
                Console.WriteLine(exp.StackTrace);
            }
        }
        private void ShowException()
        {
            int a = 100;

            throw new InvalidOperationException("Hi, exception is here.");
        }
        /// <summary>
        /// Keep the call stack
        /// </summary>
        private void HandleException1()
        {
            try
            {
                ShowException();
            }
            catch (Exception exp)
            {
                throw;
            }
        }

        /// <summary>
        /// Recreate the callstack
        /// </summary>
        private void HandleException2()
        {
            try
            {
                ShowException();
            }
            catch (Exception exp)
            {
                throw exp;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void HandleException3()
        {
            try
            {
                ShowException();
            }
            catch (Exception exp)
            {
                throw new Exception("The exception was swallowed.", exp);
            }
        }
    }
}
