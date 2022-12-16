using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.Endpoint.ViewComponents;

public class SiteFooterViewComponent : ViewComponent
{
    public async Task<IViewComponentResult> InvokeAsync()
    {
        return View("SiteFooter");
    }
}