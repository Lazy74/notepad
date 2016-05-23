using System.Web.Http.Controllers;
using System.Web.Http.ModelBinding;

namespace Notepad.Models
{
    public class NewModel : IModelBinder
    {
        public bool BindModel(HttpActionContext actionContext, ModelBindingContext bindingContext)
        {
            bindingContext.Model = new NewNotepadModel()
            {
                //Id = Convert.ToInt32(bindingContext.ValueProvider.GetValue("Id").AttemptedValue),
                Name = bindingContext.ValueProvider.GetValue("name").AttemptedValue,
                Content = bindingContext.ValueProvider.GetValue("content").AttemptedValue
            };

            return true;
        }
    }

    public class NewNotepadModel
    {
        //public int Id { get; set; }

        public string Name { get; set; }

        public string Content { get; set; }
    }
}