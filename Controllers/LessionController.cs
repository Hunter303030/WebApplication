using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApplication.ViewsPath;

namespace WebApplication.Controllers
{
    [Authorize]
    public class LessionController : Controller
    {
        public IActionResult ListLessionView(Guid courseId)
        {
            return View(ViewPaths.Lession.ListContor);
        }
    }
}
