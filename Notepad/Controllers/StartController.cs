using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Notepad.Models;

namespace Notepad.Controllers
{
    [ActLog]
    public class StartController : Controller
    {
        // GET: Start
        public ActionResult NotepadPage(string name)
        {
            if (name != null)
            {
                var notepad = Database.LoadNotepads().FirstOrDefault(item => item.Name == name);

                ViewBag.Script = "<script>viewModel1.loadNotepad({ notepadName: '" + notepad?.Name + "' });</script>";
            }

            return View();
        }

        public ActionResult Image(string text)
        {
            text = string.IsNullOrWhiteSpace(text) 
                ? "Выберите блокнот"
                : "Открыт блокнот: " + text;

            return File(Helper.ToStream(Helper.ToImage(text)), text + "/png");
        }

        public ActionResult CreateNotepadPage()
        {
            return View();
        }

        public ActionResult LogPage()
        {
            List<LogModel> logModels = Database.GetLogList();

            List<string> tableRows = new List<string>();


            foreach (LogModel logModel in logModels)
            {
                tableRows.Add("<tr><td>" + logModel.dateTime.ToString("dd MMMM yyyy [HH:mm:ss]") + "</td><td>" +
                              logModel.typeController + "</td><td>" + logModel.controller + "</td><td>" +
                              logModel.action + "</td></tr>");
            }

            ViewBag.TableRows = tableRows;

            return View("Log");
        }
    }
}