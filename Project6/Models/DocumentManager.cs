using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project6.Models
{
    public class DocumentManager
    {
        private DatabaseContext _context;

        public void NewDocument(Document document)
        {
            using (_context = new DatabaseContext())
            {
                _context.Documents.Add(document);
                _context.SaveChanges();
            }
        }

        public Document FindDocument(int id)
        {
            using (_context = new DatabaseContext())
            {
                return _context.Documents.Include("User").Where(x => x.Id == id).First();
            }
        }

        public void EditDocument2(Document document)
        {
            using (_context = new DatabaseContext())
            {
                var documentToUpdate = _context.Documents.Where(x => x.Id == document.Id).First();
                documentToUpdate.Content = document.Content;
                documentToUpdate.SubmitDate = DateTime.Now;
                documentToUpdate.ProgressStatus = documentToUpdate.ProgressStatus +1;
                documentToUpdate.UserId = document.UserId;
                _context.SaveChanges();
            }
        }

        public void AcceptDocument(int id)
        {
            using (_context = new DatabaseContext())
            {
                var item = _context.Documents.Where(x => x.Id == id).First();
                item.DocumentRegistration = true;
                _context.SaveChanges();
            }
        }

        public void DeclineDocument(int id)
        {
            using (_context = new DatabaseContext())
            {
                var item = _context.Documents.Where(x => x.Id == id).First();
                item.DocumentRegistration = false;
                _context.SaveChanges();
            }
        }


        public List<Document> ListForArchitect()
        {
            using (_context = new DatabaseContext())
            {
                return _context.Documents.Include("User").Where(x => x.ProgressStatus == 2).ToList();
            }
        }

        public List<Document> ListForProgrammer()
        {
            using (_context = new DatabaseContext())
            {
                return _context.Documents.Include("User").Where(x => x.ProgressStatus == 3).ToList();
            }
        }

        public List<Document> ListForTester()
        {
            using (_context = new DatabaseContext())
            {
                return _context.Documents.Include("User").Where(x => x.ProgressStatus == 4).ToList();
            }
        }

        public List<Document> ListForManager()
        {
            using (_context = new DatabaseContext())
            {
                return _context.Documents.Include("User").Where(x => x.ProgressStatus == 5 && x.DocumentRegistration==null).ToList();
            }
        }
    }
}