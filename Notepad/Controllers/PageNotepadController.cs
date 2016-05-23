using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Newtonsoft.Json.Linq;
using Notepad.Models;

namespace Notepad.Controllers
{
    [LogApi]
    public class PageNotepadController : ApiController
    {
        //// GET: PageNotepad
        //public ActionResult notepad()
        //{
        //    return View();
        //}

        [HttpGet]
        public IEnumerable<dynamic> GetNotepads()
        {
            var data = Database.LoadNotepads();

            return data.Select(notepad => new {name = notepad.Name});
        }

        [HttpPost]
        public HttpResponseMessage Update (NewNotepadModel data)
        {
            return !Helper.UpdateContent(data) 
                ? new HttpResponseMessage(HttpStatusCode.BadRequest) 
                : new HttpResponseMessage(HttpStatusCode.OK);
        }

        [HttpGet]
        public string GetNotepadContent(string name)
        {
            var data = Helper.GetContent(name);
            return data;
        }

        [HttpPost]
        public HttpResponseMessage Create(JObject data)
        {
            var name = data.GetValue("name").ToString();

            return !Helper.AddNotepad(name)
                ? new HttpResponseMessage(HttpStatusCode.BadRequest)
                : new HttpResponseMessage(HttpStatusCode.OK);
        }

        [HttpGet]
        public HttpResponseMessage DeleteNotepad(string name)
        {
            return Database.DeleteNotepad(name)
                ? new HttpResponseMessage(HttpStatusCode.OK)
                : new HttpResponseMessage(HttpStatusCode.BadRequest);
        }
    }
}