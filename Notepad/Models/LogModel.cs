using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Notepad.Models
{
    public class LogModel
    {
        public int Id { get; set; }

        public DateTime dateTime { get; set; }

        public string typeController { get; set; }

        public string controller { get; set; }

        public string action { get; set; }
    }
}