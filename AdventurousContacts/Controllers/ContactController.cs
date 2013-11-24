using AdventurousContacts.Models;
using AdventurousContacts.Models.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AdventurousContacts.Controllers
{
    public class ContactController : Controller
    {
        private IRepository _repository;

        public ContactController()
            : this(new Repository())
        {
            // Empty!
        }

        public ContactController(IRepository repository)
        {
            this._repository = repository;
        }

        //
        // GET: /Contact/
        public ActionResult Index()
        {
            return View("Index", this._repository.GetLastContacts());
        }

        public ActionResult Create()
        {
            return View("Create");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="FirstName, LastName, EmailAddress")]Contact contact)
        {
            return View("Create", contact);
        }

        public ActionResult Delete(int id = 0)
        {
            var contact = this._repository.GetContactById(id);
            if (contact != null)
                return View("Delete", contact);

            return View("NotFound");
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var contact = this._repository.GetContactById(id);
            if (contact != null)
            {
                try
                {
                    this._repository.Delete(contact);
                    this._repository.Save();

                    ViewBag.Action = "delete";
                    return View("Success", contact);
                }
                catch (Exception)
                {
                    ModelState.AddModelError(String.Empty,
                        "Error deleteing contact");
                    return View("Delete", contact);
                }
            }
            return View("NotFound");
        }

        public ActionResult Edit(int id = 0)
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Contact contact)
        {
            return View();
        }

        protected override void Dispose(bool disposing)
        {
            this._repository.Dispose();
            base.Dispose(disposing);
        }
    }
}
