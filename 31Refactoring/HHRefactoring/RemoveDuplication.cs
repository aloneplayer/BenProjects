using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//把重复部分提取到函数中，方便维护和修改
namespace LosTechies.DaysOfRefactoring.RemoveDuplication.Before
{
    public class MedicalRecord
    {
        public DateTime DateArchived { get; private set; }
        public bool Archived { get; private set; }

        public void ArchiveRecord()
        {
            Archived = true;
            DateArchived = DateTime.Now;
        }

        public void CloseRecord()
        {
            Archived = true;
            DateArchived = DateTime.Now;
        }
    }
}


namespace LosTechies.DaysOfRefactoring.RemoveDuplication.After
{
    public class MedicalRecord
    {
        public DateTime DateArchived { get; private set; }
        public bool Archived { get; private set; }

        public void ArchiveRecord()
        {
            SwitchToArchived();
        }

        public void CloseRecord()
        {
            SwitchToArchived();
        }

        private void SwitchToArchived()
        {
            Archived = true;
            DateArchived = DateTime.Now;
        }
    }
}