using Microsoft.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Mvc;

namespace CmsKitDemo.Pages.Shared.Components.Footer;

public class CmsKitDemoFooterViewComponent : AbpViewComponent
{
    public IViewComponentResult Invoke()
    {
        return View("/Pages/Shared/Components/Footer/Default.cshtml");
    }
}