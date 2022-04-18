using Microsoft.AspNetCore.Mvc;

namespace BattleRoom.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class RoomsController : Controller
{
    [HttpPost]
    public async Task<IActionResult> Create()
    {
        return Ok();
    }
}