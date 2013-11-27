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
            try
            {
                var entity = this._entities.Contacts.Find(contact.ContactID);
                this._entities.Contacts.Remove(entity);
            }
            catch (Exception e)
            {
                this.HandleException(e);
            } 
        }

        public IQueryable<Contact> FindAllContacts()
        {
            try
            {
                return this._entities.Contacts.AsQueryable();
            }
            catch (Exception e)
            {
                this.HandleException(e);
                throw;
            }  
        }

        public Contact GetContactById(int contactId)
        {
            try
            {
                return this._entities.Contacts.Find(contactId);
            }
            catch (Exception e)
            {
                this.HandleException(e);
                throw;
            }  
        }

        public List<Contact> GetLastContacts(int count = 20)
        {
            try
            {
                return this._entities.Contacts.OrderByDescending(c => c.ContactID).Take(count).ToList();
            }
            catch (Exception e)
            {
                this.HandleException(e);
                throw;
            } 
        }

        public void Save()
        {
            try
            {
                this._entities.SaveChanges();
            }
            catch (Exception e)
            {
                this.HandleException(e);
            }      
        }

        public void Update(Contact contact)
        {
            this._entities.Entry(contact).State = EntityState.Modified;
        }

        //Throw correct exception
        private void HandleException(Exception e)
        {
            if (e is EntityException)
                if (e.InnerException != null)
                    throw e.InnerException;
            throw e.GetBaseException();
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