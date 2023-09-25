using DataLibrary.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CloudKitchen.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActivityController : ControllerBase
    {
        private readonly IMenuDetailsHelper _menuDetailsHelper;
        public ActivityController(IMenuDetailsHelper menuDetailsHelper)
        {
            _menuDetailsHelper = menuDetailsHelper;
        }

        [HttpGet("[action]")]
        //[Authorize(Policy = "RequiredLoggedIn")]
        public IActionResult GetAllCatagories()
        {
            var menus = _menuDetailsHelper.GetMenuDetails();

            return Ok(menus);
        }

        [HttpGet("[action]/{categoryId}")]
        public IActionResult GetMenuItems([FromRoute]int categoryId)
        {
            return Ok(_menuDetailsHelper.GetItemByCateoryId(categoryId));
        }
    }
}
