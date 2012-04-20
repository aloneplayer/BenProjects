using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace CodingBestPractice
{
    public class ClassWithTrouble
    {
        private void WriteLog(string logInfo)
        {

        }

        private void MakeTrouble()
        {
            throw new Exception("I'm unhappy! ");
        }

        public void CallingWithGoodPractice()
        {
            try
            {
                this.MakeTrouble();
            }
            catch (Exception exp)
            {
                WriteLog(exp.Message);
                throw;
            }
        }


        public void Calling()
        {
            try
            {
                this.MakeTrouble();
            }
            catch (Exception exp)
            {
                WriteLog(exp.Message);
                throw exp;
            }
        }
    }

    public class ExceptionHandlingDemo
    {
        public ExceptionHandlingDemo() { }

        public void Showtime()
        {
            ClassWithTrouble c = new ClassWithTrouble();

            try
            {
                //c.Calling();
                c.CallingWithGoodPractice();
            }
            catch (Exception exp)
            {   // The call stack is the KEY OPINT!
                Debug.WriteLine(exp.StackTrace.ToString());
            }
        }
    }

}
