using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//和提取子类相反，
namespace LosTechies.DaysOfRefactoring.SampleCode.CollapseHierarchy.Before
{
    public class WebPage
    { }
    public class Website
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public IEnumerable<WebPage> Pages { get; set; }
    }

    public class StudentWebsite : Website
    {
        public bool IsActive { get; set; }
    }
}

namespace LosTechies.DaysOfRefactoring.SampleCode.CollapseHierarchy.After
{
    public class WebPage
    { }
 
    public class Website
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public IEnumerable<WebPage> Pages { get; set; }
        public bool IsActive { get; set; }
    }
}