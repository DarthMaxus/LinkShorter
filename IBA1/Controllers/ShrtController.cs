using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using IBA1.Models;

namespace IBA1.Controllers
{
    public class ShrtController : Controller
    {
        // GET: Shrt
        LinkRepository repo;
        public ShrtController()
        {
            repo = new LinkRepository();
        }
        public ActionResult Index(string h)
        {
            List<Link> links = repo.List();
            foreach (Link a in links)
            {
                if (a.ShortedLink == "https://localhost:44354/Shrt?h=" + h && a.IsActive)
                {
                    repo.LinkOpens(a.LinkId);
                    return Redirect(a.OriginLink);
                }
            }
            return Redirect("/Home/Error");
        }
    }
}