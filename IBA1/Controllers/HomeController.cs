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
        LinkRepository repo;
        public HomeController()
        {
            repo = new LinkRepository();
        }
        [HttpGet]
        public ActionResult Links()
        {

            List<Link> links = repo.List();
            List<Group> groups = repo.GroupList();
            SelectList items = new SelectList(repo.GroupList(), "Name", "Name");
            ViewBag.Groups = groups;
            ViewBag.Items = items;
            ViewBag.Links = links;
            return View();

        }
        [HttpGet]
        public ActionResult Error()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Stat()
        {

            List<Link> links = repo.List();
            List<Group> groups = repo.GroupList();
            ViewBag.Links = links;
            ViewBag.Groups = groups;
            return View();

        }
        [HttpGet]
        public ActionResult Rename(int id)
        {
            Group group = repo.GetGroup(id);
            ViewBag.Group = group;
            return View(new Group() { GroupId = id });
        }
        [HttpPost]
        public ActionResult Rename2(Group group)
        {
            repo.RenameGroup(group.GroupId, group.Name);
            return RedirectToAction("Groups");
        }
        [HttpGet]
        public ActionResult Groups()
        {
            List<Group> groups = repo.GroupList();
            ViewBag.Groups = groups;
            return View(new Group());
        }
        [HttpGet]
        public ActionResult DeleteLink(int id)
        {
            repo.DeleteLink(id);
            return RedirectToAction("Links");
        }
        [HttpGet]
        public RedirectResult Link(string hash) //old opening
        {
            List<Link> links = repo.List();
            foreach (Link a in links)
            {
                if (a.ShortedLink == "https://localhost:44354/Home/Link?hash=" + hash && a.IsActive)
                {
                    repo.LinkOpens(a.LinkId);
                    return Redirect(a.OriginLink);
                }
            }
            return Redirect("/Home/Error");
        }

        [HttpPost]
        public ActionResult AddGroup(Group group)
        {
            group.Login = User.Identity.Name;
            List<Group> groups = repo.GroupList();
            foreach (Group x in groups)
            {
                if (x.Name == group.Name)
                    return View("Exists");
            }
            repo.Save(group);
            return RedirectToAction("Groups");
        }
        [HttpGet]
        public ActionResult AppointGroup(int id)
        {
            List<Group> groups = repo.GroupList();
            ViewBag.Groups = groups;
            ViewBag.LinkId = id;
            return View();
        }
        [HttpGet]
        public ActionResult AppointGroup2(int linkid,int groupid)
        {
            using (LinkContext db=new LinkContext())
            {
                repo.Appoint(linkid, groupid);
                return RedirectToAction("Links");
            }
        }

        [HttpPost]
        public ActionResult AddLink(Link link)
        {
            string shorted = "https://localhost:44354/Shrt?h=" + link.OriginLink.GetHashCode();
            if(link.OriginLink.StartsWith("https://"))
            {
                link.Redirections = 0;
                link.ShortedLink = shorted;
                link.IsActive = true;
                link.Login = User.Identity.Name;
                repo.Save(link);
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("LinkNoHttps");
            }
            
        }
        [HttpGet]
        public ActionResult LinkNoHttps()
        {
            return View();
        }
        [HttpGet]
        public ActionResult DeleteGroup(int id)
        {
            repo.DeleteGroup(id);
            return RedirectToAction("Groups");
        }
        [HttpGet]
        public ActionResult GroupView(int id)
        {
            Group a = repo.GetGroup(id);
            ViewBag.Group = a;
            List<Link> links = repo.List();
            ViewBag.Links = links;
            return View("GroupView");
        }
        [HttpGet]
        public ActionResult Index()
        {
            List<Link> links = repo.List();
            ViewBag.Links = links;
            SelectList items = new SelectList(repo.GroupList(), "Name", "Name");
            ViewBag.Groups = items;
            return View(new Link());
        }
        [HttpGet]
        public ActionResult SwitchActive(int id)
        {
            repo.ChangeActivity(id);
            return RedirectToAction("Links");
        }
    }
    
}