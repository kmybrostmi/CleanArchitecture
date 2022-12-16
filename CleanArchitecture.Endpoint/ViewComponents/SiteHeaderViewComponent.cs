using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.Endpoint.ViewComponents;
public class SiteHeaderViewComponent : ViewComponent
{
    public async Task<IViewComponentResult> InvokeAsync()
    {
        return View("SiteHeader");
    }
}
