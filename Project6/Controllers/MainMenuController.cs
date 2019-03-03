using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Project6.Models;

namespace Project6.Controllers
{
    //[Filter.AuthoriseUser]
    [Authorize]
    public class MainMenuController : Controller
    {
        private Models.User.Role _role;
        UserManager userManager = new UserManager();
        DocumentManager documentManager = new DocumentManager();

        public ActionResult Index2()
        {
            _role = (Models.User.Role)Session["Role"];
            List<Document> documents;
            if (_role == Models.User.Role.Manager)
            {
                 documents = documentManager.ListForManager();
                var unregisteredUsers = userManager.UnregisteredUsers();
                
                ViewBag.TotalUsers = unregisteredUsers;
                ViewBag.Documents = documents;

                return View("ManagerView");
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
                documents = documentManager.ListForTester();
                return View("OtherRolesView", documents);
            }
        }

        [Authorize(Roles = "Manager")]
        public ActionResult AcceptUser(User user)
        {
            userManager.AcceptUser(user);
            return RedirectToAction("Index2");
        }

        [Authorize(Roles = "Manager")]
        public ActionResult Delete(User user)
        {
            userManager.DeleteUser(user);
            return RedirectToAction("Index2");

        }

        [Authorize(Roles ="Manager")]
        public ActionResult AcceptDocument(int id)
        {
            documentManager.AcceptDocument(id);
            return RedirectToAction("Index2");
        }

        [Authorize(Roles = "Manager")]
        public ActionResult DeclineDocument(int id)
        {
            documentManager.DeclineDocument(id);
            return RedirectToAction("Index2");
        }

        [HttpPost]
        [Authorize(Roles = "Analyst")]
        public ActionResult CreateDocument([Bind(Include = "Title,Content")] Document document)
        {
            Document newDocument = new Document
            {
                Title = document.Title,
                Content = document.Content+"Created by:"+(string)Session["Username"]+","+ Session["Role"].ToString(),
                UserId = (int)Session["Id"],
                SubmitDate = DateTime.Now,
                ProgressStatus = 2,
                DocumentRegistration = null
            };
            documentManager.NewDocument(newDocument);

            ViewBag.Message = "Document Created";
            return RedirectToAction("Index2");

        }

        public ActionResult Edit(int id)
        {
            Document item = documentManager.FindDocument(id);
            return View("Edit", item);
        }

        public ActionResult EditDocument(Document document)
        {
            document.UserId =(int) Session["Id"];
            document.Content =document.Content +"Created by:" + (string)Session["Username"] + "," + Session["Role"].ToString();
            documentManager.EditDocument2(document);
            return RedirectToAction("Index2");
        }

        public ActionResult ViewDocument(int id)
        {
            Document item = documentManager.FindDocument(id);
            return View("ViewDocument", item);
        }

        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Index", "Home");
        }

    }
}