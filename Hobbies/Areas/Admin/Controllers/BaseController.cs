using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static Hobbies.Areas.Admin.Constants.AdminConstants;
namespace Hobbies.Areas.Admin.Controllers
{
    [Area(AreaName)]
    [Route("Admin/[controller]/[Action]/{id?}")]
    [Authorize(Roles = AdminRolleName)]
    public class BaseController : Controller
    {
    }
}
