using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace IBA1.Models
{
    public class LinkRepository:IDisposable
    {
        private LinkContext db = new LinkContext();
        public void Save(Link link)
        {
            db.Links.Add(link);
            db.SaveChanges();
        }
        public void Save(Group group)
        {
            db.Groups.Add(group);
            db.SaveChanges();
        }
        public void RenameGroup(int id, string newname)
        {
            db.Groups.Find(id).Name = newname;
            db.SaveChanges();
        }
        public List<Link> List()
        {
            return db.Links.ToList();
        }
        public List<Group> GroupList()
        {
            return db.Groups.ToList();
        }
        public Link GetLink(int id)
        {
            return db.Links.Find(id);
        }
        public void LinkOpens(int id)
        {
            db.Links.Find(id).Redirections++;
            db.SaveChanges();
        }
        public void Appoint(int linkid,int groupid)
        {
            db.Links.Find(linkid).GroupId = groupid;
            db.SaveChanges();
        }
        public void ChangeActivity(int id)
        {
            db.Links.Find(id).IsActive = !db.Links.Find(id).IsActive;
            db.SaveChanges();
        }
        public Group GetGroup(int id)
        {
            return db.Groups.Find(id);
        }
        public void DeleteLink(int id)
        {
            db.Links.Remove(db.Links.Find(id));
            db.SaveChanges();
        }
        public void DeleteGroup(int id)
        {
            db.Groups.Remove(db.Groups.Find(id));
            db.SaveChanges();
        }

        protected void Dispose(bool disposing)
        {
            if(disposing)
            {
                if(db!=null)
                {
                    db.Dispose();
                    db = null;
                }
            }
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}