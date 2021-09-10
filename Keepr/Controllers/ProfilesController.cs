using Keepr.Services;
using Microsoft.AspNetCore.Mvc;

namespace Keepr.Controllers
{
  [ApiController]
  [Route("/api/[controller]")]
  public class ProfilesController : ControllerBase
  {
    private readonly AccountService _service;

    public ProfilesController(AccountService service)
    {
      _service = service;
    }

    // [HttpGet("{id}")]
    // [Authorize]
    // public async Task<ActionResult<Account>> Get(int id)
    // {
    //   try
    //   {


    //   }
    //   catch (Exception e)
    //   {
    //     return BadRequest(e.Message);
    //   }
    // }
  }
}