using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ExpressDBTest;
using System.Text;

namespace ExpressDBTest.Controllers
{ 
    public class ContactController : Controller
    {
        private CelerationContext _context = new CelerationContext();

		public ContactController()
		{
		}
		
        #region helpers

        private static Random random = new Random((int)DateTime.Now.Ticks);

        private string RandomString(int size)
        {
            StringBuilder builder = new StringBuilder();
            char ch;
            for (int i = 0; i < size; i++)
            {
                ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                builder.Append(ch);
            }

            return builder.ToString();
        }

        private string RandomPhone()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("111-22");

            string numbers = "0123456789";

            for(int i=0; i < 5; i++)
            {
                int index = Convert.ToInt32(Math.Floor(10 * random.NextDouble()));
                builder.Append(numbers[index]);
                if (i == 0)
                    builder.Append("-");
            }

            return builder.ToString();
        }

        private List<Contact> GetAllContacts()
        {
            return _context.Contacts.ToList();
        }

        #endregion

        //
        // GET: /Contact/

        public ViewResult Index()
        {
            var items = _context.Contacts.ToList();
            return View(items);
        }

        //
        // GET: /Contact/Details/5

        public ViewResult Details(long id)
        {
            Contact contact = _context.Contacts.Single(c => c.Id == id);
            return View(contact);
        }

        [HttpGet]
        public ActionResult Create()
        {
            Contact contact = new Contact();
            contact.Name = RandomString(6);
            contact.Phone = RandomPhone();
            contact.CreatedDate = DateTime.Now;

              _context.Contacts.AddObject(contact);
              _context.SaveChanges();
             
            return RedirectToAction("Index");  
        }
        
        //
        // GET: /Contact/Edit/5
 
        public ActionResult Edit(long id)
        {
            Contact contact = _context.Contacts.Single(c => c.Id == id);
            return View(contact);
        }

        //
        // POST: /Contact/Edit/5

        [HttpPost]
        public ActionResult Edit(Contact contact)
        {
            if (ModelState.IsValid)
            {
                _context.Contacts.Attach(contact);
                _context.ObjectStateManager.ChangeObjectState(contact, EntityState.Modified);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(contact);
        }

       
        [HttpGet]
        public ActionResult Delete(long id)
        {
            Contact contact = _context.Contacts.Single(c => c.Id == id);
            _context.Contacts.DeleteObject(contact);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }


        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
            base.Dispose(disposing);
        }
    }
}