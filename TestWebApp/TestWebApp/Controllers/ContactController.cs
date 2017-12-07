﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NHibernate;
using NHibernate.Linq;
using TestWebApp.Models;
using TestWebApp.Entity;
using TestWebApp.Helper;

namespace TestWebApp.Controllers
{
    public class ContactController : Controller
    {
        // GET: Contact
        public ActionResult Index()
        {
            ViewBag.Message = "Your Contact page is here.";
            using (ISession session = NHibernateSession.OpenSessionForContact())
            {
                var contact = session.Query<Contact>().ToList();
                var viewModel = Mapper.MapToContactViewModel(contact);
                return View(viewModel);
            }
        }

        public ActionResult Create()
        {
            return View();
        }

        // POST: Contact/Create
        [HttpPost]
        public ActionResult Create(Contact contact)
        {
            try
            {
               
                // TODO: Add insert logic here
                using (ISession session = NHibernateSession.OpenSessionForContact())
                {
                    using (ITransaction transaction = session.BeginTransaction())   //  Begin a transaction
                    {
                        session.Save(contact); //  Save the book in session
                        transaction.Commit();   //  Commit the changes to the database
                    }
                }
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                ViewBag.Message = "Contact Creation Failed. Try another Email.";
                //e.Message;
                return View();
            }
        }

        // GET: Contact/Edit
        public ActionResult Edit(int id)
        {
            using (ISession session = NHibernateSession.OpenSessionForContact())
            {
                var contact = session.Get<Contact>(id);
                return View(contact);
            }

        }

        [HttpPost]
        public ActionResult Edit(int id, Contact contact)
        {
            try
            {
                using (ISession session = NHibernateSession.OpenSessionForContact())
                {
                    var contactUpdate = session.Get<Contact>(id);

                    contactUpdate.Id = contact.Id;
                    contactUpdate.FirstName = contact.FirstName;
                    contactUpdate.LastName = contact.LastName;
                    contactUpdate.Email = contact.Email;



                    using (ITransaction transaction = session.BeginTransaction())
                    {
                        session.Save(contactUpdate);
                        transaction.Commit();
                    }
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Contact/Details
        public ActionResult Details(int id)
        {
            using (ISession session = NHibernateSession.OpenSessionForContact())
            {
                var contact = session.Get<Contact>(id);
                return View(contact);
            }
        }


        //GET: Contact/Delete

        public ActionResult Delete(int id)
        {
            using (ISession session = NHibernateSession.OpenSessionForContact())
            {
                var contact = session.Get<Contact>(id);
                return View(contact);
            }
        }


        [HttpPost]
        public ActionResult Delete(int id, Contact contact)
        {
            try
            {
                using (ISession session = NHibernateSession.OpenSessionForContact())
                {
                    using (ITransaction transaction = session.BeginTransaction())
                    {
                        session.Delete(contact);
                        transaction.Commit();
                    }
                }
                return RedirectToAction("Index");
            }
            catch (Exception exception)
            {
                return View();
            }
        }

    }
}