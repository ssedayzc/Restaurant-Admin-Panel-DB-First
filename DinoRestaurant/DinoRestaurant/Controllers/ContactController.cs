using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DinoRestaurant.Models.Entity;

namespace DinoRestaurant.Controllers
{
    public class ContactController : Controller
    {
        dinoMariaDBEntities db = new dinoMariaDBEntities();

        public ActionResult Index(string p)
        {
            var contact = from d in db.Contacts select d;
            if (!string.IsNullOrEmpty(p))
            {
                contact = contact.Where(c=>c.Message.Contains(p));
            }
            return View(contact.ToList());

            //var contacts = db.Contacts.ToList();
            //return View(contacts);
        }

        public PartialViewResult MessageListMenu()
        {
            return PartialView();
        }

        public ActionResult Inbox()
        {
            var contactValues = db.Contacts.ToList();
            return View(contactValues);
        }

        public ActionResult Sendbox()
        {
            var contactValues = db.Contacts.ToList();
            return View(contactValues);
        }

        public ActionResult GetContactDetails(int id)
        {
            var contactValues = db.Contacts.Where(x => x.ContactId == id);
            return View(contactValues);
        }

        [HttpGet]
        public ActionResult NewMessage()
        {
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult NewMessage(Contacts p)
        {
            db.Contacts.Add(p);
            db.SaveChanges();
            return RedirectToAction("Sendbox");
        }

        [HttpPost]
        public ActionResult MoveToTrash(List<int> ids)
        {
            foreach (var id in ids)
            {
                var message = db.Contacts.Find(id);
                if (message != null)
                {
                    message.UserMail = "Çöp Kutusu";
                    db.SaveChanges();
                }
            }

            return Json(new { success = true });
        }

        public ActionResult Trash()
        {
            var trashedMessages = db.Contacts.Where(x => x.UserMail == "Çöp Kutusu").ToList();
            return View(trashedMessages);
        }

        public ActionResult DeleteMsg(int id)
        {
            var msg = db.Contacts.Find(id);
            db.Contacts.Remove(msg);
            db.SaveChanges();
            return RedirectToAction("Trash");
        }

        public ActionResult ConfirmDeleteMsg(int id)
        {
            var msg = db.Contacts.Find(id);
            return View(msg);
        }

        [HttpPost]
        public ActionResult ConfirmDeleteMsgConfirmed(int id)
        {
            var msg = db.Contacts.Find(id);
            db.Contacts.Remove(msg);
            db.SaveChanges();
            return RedirectToAction("Trash");
        }
    }
}
