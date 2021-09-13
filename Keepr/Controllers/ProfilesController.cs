using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CodeWorks.Auth0Provider;
using Keepr.Models;
using Keepr.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Keepr.Controllers
{
  [ApiController]
  [Route("/api/[controller]")]
  public class ProfilesController : ControllerBase
  {
    private readonly AccountService _service;

    private readonly VaultsService _vs;

    public ProfilesController(AccountService service, VaultsService vs)
    {
      _service = service;
      _vs = vs;
    }
    [HttpGet("{id}")]
    public ActionResult<Account> Get(int id)
    {
      try
      {
        Account account = _service.GetProfileById(id);
        return Ok(account);
      }
      catch (Exception err)
      {
        return BadRequest(err.Message);
      }
    }


    [HttpGet("{id}/vaults")]
    [Authorize]
    public async Task<ActionResult<List<Vault>>> GetVaults()
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        List<Vault> vaults = _vs.GetVaultsByCreator(userInfo.Id, false);
        return Ok(vaults);
      }
      catch (Exception err)
      {
        return BadRequest(err.Message);
      }
    }
  }
}