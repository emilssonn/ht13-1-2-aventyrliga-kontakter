using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AdventurousContacts.Models.DataModels;
using System.Data;

namespace AdventurousContacts.Models.Repository
{
    public class Repository : IRepository
    {
        private AdventureWorksEntities _entities = new AdventureWorksEntities();

        #region IRepository members

        public void Add(Contact contact)
        {
            this._entities.Contacts.Add(contact);
        }

        public void Delete(Contact contact)
        {
            var entity = this._entities.Contacts.Find(contact.ContactID);
            this._entities.Contacts.Remove(entity);
        }

        public IQueryable<Contact> FindAllContacts()
        {
            //return this._entities.Contacts;
            return this._entities.Contacts.ToList().AsQueryable();
        }

        public Contact GetContactById(int contactId)
        {
            return this._entities.Contacts.Find(contactId);
        }

        public List<Contact> GetLastContacts(int count = 20)
        {
            //return this._entities.Contacts.Skip(this._entities.Contacts.Count() - count).Take(count).ToList();
            return this._entities.Contacts.OrderByDescending(c => c.ContactID).Take(count).OrderBy(c => c.ContactID).ToList();
        }

        public void Save()
        {
            this._entities.SaveChanges();
        }

        public void Update(Contact contact)
        {
            this._entities.Entry(contact).State = EntityState.Modified;
        }

        #endregion

        #region IDisposable

        private bool _disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this._disposed)
            {
                if (disposing)
                    this._entities.Dispose();
            }
            this._disposed = true;
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}