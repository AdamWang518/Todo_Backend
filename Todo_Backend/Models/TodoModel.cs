using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Todo_Backend.Models
{
    public class TodoModel
    {
        public Guid id { get; set; }
        public String date { get; set; }
        public String text { get; set; }
        public String time { get; set; }
        public String comment { get; set; }
    }
}