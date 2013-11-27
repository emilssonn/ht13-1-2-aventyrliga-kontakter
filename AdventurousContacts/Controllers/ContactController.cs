using AdventurousContacts.Models;
using AdventurousContacts.Models.Repository;
using System;
using System.Collections.Generic;
using System.Data;
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

        // GET: /Contact/
        public ActionResult Index()
        {
            return View("Index", this._repository.GetLastContacts());
        }

        // GET: /Contact/Create
        public ActionResult Create()
        {
            return View("Create");
        }

        // POST: /Contact/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="FirstName, LastName, EmailAddress")]Contact contact)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    this._repository.Add(contact);
                    this._repository.Save();

                    TempData.Add("Success",
                        String.Format(Resources.Strings.CreateSuccessMessage, 
                            contact.FirstName, contact.LastName, contact.EmailAddress));

                    return RedirectToAction("Index");
                }
                catch (Exception e)
                {
                    ModelState.AddModelError(String.Empty,
                        e.Message);
                }
            }
            return View("Create", contact);
        }

        // GET: /Contact/Delete/:id
        public ActionResult Delete(int id = 0)
        {
            var contact = this._repository.GetContactById(id);
            if (contact != null)
                return View("Delete", contact);

            return View("NotFound");
        }

        // POST: /Contact/Delete/:id
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

                    TempData.Add("Success",
                        String.Format(Resources.Strings.DeleteSuccessMessage,
                            contact.FirstName, contact.LastName, contact.EmailAddress));

                    return RedirectToAction("Index");
                }
                catch (Exception e)
                {
                    ModelState.AddModelError(String.Empty,
                        e.Message);                 
                }
                return View("Delete", contact);
            }
            return View("NotFound");
        }

        // GET: /Contact/Edit/:id
        public ActionResult Edit(int id = 0)
        {
            var contact = this._repository.GetContactById(id);
            if (contact == null)
                return View("NotFound");

            return View("Edit", contact);
        }

        // POST: /Contact/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Contact contact)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    this._repository.Update(contact);
                    this._repository.Save();

                    TempData.Add("Success",
                        String.Format(Resources.Strings.EditSuccessMessage, 
                            contact.FirstName, contact.LastName, contact.EmailAddress));

                    return RedirectToAction("Index");
                }
                catch (Exception e)
                {
                    ModelState.AddModelError(String.Empty,
                        e.Message);
                }
            }
            return View("Edit", contact);
        }

        protected override void Dispose(bool disposing)
        {
            this._repository.Dispose();
            base.Dispose(disposing);
        }
    }
}
