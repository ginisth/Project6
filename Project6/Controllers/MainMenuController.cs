using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Project6.Models;

namespace Project6.Controllers
{
    [Filter.AuthoriseUser]
    public class MainMenuController : Controller
    {
        private Models.User.Role _role;


        public ActionResult Index2()
        {
            UserManager manager = new UserManager();
            DocumentManager documentManager = new DocumentManager();
            List<Document> documents;
            _role = (Models.User.Role)Session["Role"];
            if (_role == Models.User.Role.Manager)
            {
                documents = documentManager.ListForManager();
                var unregisteredUsers = manager.UnregisteredUsers();
                
                ViewBag.TotalUsers = unregisteredUsers;
                ViewBag.Documents = documents;
                return View("Experiment");
            }
            else if (_role == Models.User.Role.Analyst)
            {
                return View("AnalystView");
            }
            else if (_role == Models.User.Role.Architect)
            {
                documents = documentManager.ListForArchitect();
                return View("OtherRolesView", documents);
            }
            else if(_role==Models.User.Role.Programmer)
            {
                documents = documentManager.ListForProgrammer();
                return View("OtherRolesView", documents);
            }
            else
            {
                return View("Index");
            }
        }

        public ActionResult AcceptUser(User user)
        {
            UserManager manager = new UserManager();
            manager.AcceptUser(user);
            return RedirectToAction("Index2");
        }

        public ActionResult Delete(User user)
        {
            UserManager manager = new UserManager();
            manager.DeleteUser(user);
            return RedirectToAction("Index2");

        }

        [HttpPost]
        public ActionResult CreateDocument([Bind(Include = "Title,Content")] Document document)
        {
            Document newDocument = new Document
            {
                Title = document.Title,
                Content = document.Content+"Created by:"+(string)Session["Username"]+","+ Session["Role"].ToString(),
                UserId = (int)Session["Id"],
                SubmitDate = DateTime.Now,
                ProgressStatus = 2,
                DocumentRegistration = false
            };
            DocumentManager manager = new DocumentManager();
            manager.NewDocument(newDocument);
            ViewBag.Message = "Document Created";
            return RedirectToAction("Index2");

        }

        public ActionResult Edit(int id)
        {
            DocumentManager manager = new DocumentManager();
            Document item = manager.EditDocument(id);
            return View("Edit", item);
        }

        public ActionResult EditDocument(Document document)
        {
            document.UserId =(int) Session["Id"];
            document.Content =document.Content +"Created by:" + (string)Session["Username"] + "," + Session["Role"].ToString();
            DocumentManager manager = new DocumentManager();
            manager.EditDocument2(document);
            return RedirectToAction("Index2");
        }

        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Index", "Home");
        }

    }
}