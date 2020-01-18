using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IBA1.Models
{
    public class Link
    {
        public int LinkId { get; set; }
        public string OriginLink { get; set; }
        public string ShortedLink { get; set; }
        public string Login { get; set; }
        public bool IsActive { get; set; }
        public int Redirections { get; set; }
        public int GroupId { get; set; }
    }
}