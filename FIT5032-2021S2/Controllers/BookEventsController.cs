using FIT5032_2021S2.Models;
using FIT5032_2021S2.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;

namespace FIT5032_2021S2.Controllers
{
    public class BookEventsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: BookEvents
        public ActionResult Index()
        {
            return HttpNotFound();
        }

        [Authorize(Roles = "Customer")]
        public ActionResult BookEvent(int id)
        {
            
            try {
                // find user
                var userId = User.Identity.GetUserId();

                // add bookevent to db
                var bookEvent = new BookEvent { StoreEventId = id, UserId = userId };
                db.BookEvents.Add(bookEvent);
                db.SaveChanges();

                var user = db.Users.FirstOrDefault(u=>u.Id==userId);
                bookEvent.StoreEvent = db.storeEvents.FirstOrDefault(se=>se.Id==id);
                bookEvent.StoreEvent.Store = db.Stores.FirstOrDefault(s => s.Id == bookEvent.StoreEvent.StoreId);

                // send email
                String toEmail = user.Email;
                String subject = "Book event comfirmation";
                String contents = $"Store: {bookEvent.StoreEvent.Store.Name}, start time: {bookEvent.StoreEvent.StartTime.ToString("dd-MMM-yyyy mm:HH")}";

                EmailSender es = new EmailSender();
                es.Send(toEmail, subject, contents);

                ViewBag.Result = "Email has been send.";
                
                return RedirectToAction("ViewBookedEvents");
            }
            catch (Exception exception) {
                Console.WriteLine(exception.Message);
                return RedirectToAction("ViewBookedEvents");
            }
        }

        [Authorize(Roles = "Customer")]
        public ActionResult ViewBookedEvents()
        {
            var userId = User.Identity.GetUserId();
            return View(db.BookEvents.Include("StoreEvent").Include("StoreEvent.EventType").Include("StoreEvent.Store").Where(be => be.UserId == userId).ToList());
        }

        [Authorize(Roles = "Customer")]
        public ActionResult DeleteBookEvent(int storeEventId)
        {
            var userId = User.Identity.GetUserId();
            var deleteEvent = db.BookEvents.FirstOrDefault(be => be.StoreEventId == storeEventId && be.UserId == userId);
            if(deleteEvent != null)
            {
                db.BookEvents.Remove(deleteEvent);
                db.SaveChanges();
            }
            return RedirectToAction("ViewBookedEvents");
        }


    }
}