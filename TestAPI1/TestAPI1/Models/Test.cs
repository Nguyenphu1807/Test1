using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestAPI1.Models
{
    public class Test
    {
        public string TestId { get; set; }
        public string Name { get; set; }
        public int ClassId { get; set; }
        public string BirthYear { get; set; }
        public int Gender { get; set; }
        public DateTime CreateTime { get; set; }
    }

    public class TestView
    {
        public string TestId { get; set; }
        public string Name { get; set; }
        public int ClassId { get; set; }
        public string ClassName { get; set; }
        public string BirthYear { get; set; }
        public int Gender { get; set; }
        public DateTime CreateTime { get; set; }
    }
}