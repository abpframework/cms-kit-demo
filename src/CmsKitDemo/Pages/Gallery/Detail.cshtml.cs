using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CmsKitDemo.Pages.Gallery
{
    public class DetailModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public Guid ImageId { get; set; }
        
        public void OnGet()
        {
            //TODO: get the image gallery record by ImageId;
        }
    }
}
