using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IBA1.Models;

namespace IBA1.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Links()
        {
            using (LinkContext db = new LinkContext())
            {
                List<Link> links = db.Links.ToList();
                List<Group> groups = db.Groups.ToList();
                SelectList items = new SelectList(db.Groups.ToList(), "Name", "Name");
                ViewBag.Groups = groups;
                ViewBag.Items = items;
                ViewBag.Links = links;
                return View();
            }
        }

        public ActionResult Error()
        {
            return View();
        }
        public ActionResult Stat()
        {
            using(LinkContext db=new LinkContext())
            {
                List<Link> links = db.Links.ToList();
                List<Group> groups = db.Groups.ToList();
                ViewBag.Links = links;
                ViewBag.Groups = groups;
                return View();
            }  
        }

        public ActionResult Rename(int id)
        {
            using (LinkContext db = new LinkContext())
            {
                Group group = db.Groups.Find(id);
                ViewBag.Group = group;
                return View(new Group() { GroupId = id });
            }
        }
        [HttpPost]
        public ActionResult Rename2(Group group)
        {
           
            using (LinkContext db=new LinkContext())
            {
                db.Groups.Find(group.GroupId).Name = group.Name;
                db.SaveChanges();
                return RedirectToAction("Groups");
            }
        }
        public ActionResult Groups()
        {
            using(LinkContext db=new LinkContext())
            {
                List<Group> groups = db.Groups.ToList();
                ViewBag.Groups = groups;
            }
            return View(new Group());
        }

        public ActionResult DeleteLink(int id)
        {
            using (LinkContext db=new LinkContext())
            {
                db.Links.Remove(db.Links.Find(id));
                db.SaveChanges();
                return RedirectToAction("Links");
            }
        }
        [HttpGet]
        public RedirectResult Link(string hash)
        {
            using (LinkContext db = new LinkContext())
            {
                List<Link> links = db.Links.ToList();
                foreach (Link a in links)
                {
                    if (a.ShortedLink == "https://localhost:44354/Home/Link?hash=" + hash && a.IsActive)
                    {
                        db.Links.Find(a.LinkId).Redirections++;
                        db.SaveChanges();
                        return Redirect(a.OriginLink);
                    }
                }
            }
            return Redirect("/Home/Error");
        }


        public ActionResult AddGroup (Group group)
        {
            group.Login = User.Identity.Name;
            using (LinkContext db=new LinkContext())
            {
                List<Group> groups = db.Groups.ToList();
                foreach(Group x in groups)
                {
                    if (x.Name == group.Name)
                        return View("Exists");
                }
                db.Groups.Add(group);
                db.SaveChanges();
                return RedirectToAction("Groups");
            }
        }

        public ActionResult AppointGroup(int id)
        {
            using (LinkContext db=new LinkContext())
            {
                List<Group> groups = db.Groups.ToList();
                ViewBag.Groups = groups;
                ViewBag.LinkId = id;
                return View();
            }
        }

        public ActionResult AppointGroup2(int linkid,int groupid)
        {
            using (LinkContext db=new LinkContext())
            {
                db.Links.Find(linkid).GroupId = groupid;
                db.SaveChanges();
                return RedirectToAction("Links");
            }
        }

        [HttpPost]
        public ActionResult AddLink(Link link)
        {
            string shorted = "https://localhost:44354/Home/Link?hash=" + link.OriginLink.GetHashCode();
            link.Redirections = 0;
            link.ShortedLink = shorted;
            link.IsActive = true;
            link.Login = User.Identity.Name;
            using (LinkContext db = new LinkContext())
            {
                db.Links.Add(link);
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        public ActionResult DeleteGroup(int id)
        {
            using (LinkContext db = new LinkContext())
            {
                Group removable = db.Groups.Find(id);
                db.Groups.Remove(removable);
                Response.Write("<script>alert('Группа "+ removable.Name +"успешно удалена')</script>");
                db.SaveChanges();
                return RedirectToAction("Groups");
            }
        }

        public ActionResult GroupView(int id)
        {
            using (LinkContext db=new LinkContext())
            {
                Group a = db.Groups.Find(id);
                ViewBag.Group = a;
                List<Link> links = db.Links.ToList();
                ViewBag.Links = links;
                return View("GroupView");
            }
        }
        public ActionResult Index()
        {
            using (LinkContext db = new LinkContext())
            {
                List<Link> links = db.Links.ToList();
                ViewBag.Links = links;
                SelectList items = new SelectList(db.Groups.ToList(), "Name", "Name");
                ViewBag.Groups = items;
            }
            return View(new Link());
        }
        [HttpGet]
        public ActionResult SwitchActive(int id)
        {
            using (LinkContext db = new LinkContext())
            {
                db.Links.Find(id).IsActive = !db.Links.Find(id).IsActive;
                db.SaveChanges();
            }
            return RedirectToAction("Links");
        }
    }
    
}