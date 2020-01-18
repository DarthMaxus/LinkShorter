using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace IBA1.Models
{
    public class LinkContext : DbContext
    {
        public DbSet<Link> Links { get; set; }
        public DbSet<Group> Groups { get; set; }
    }
}