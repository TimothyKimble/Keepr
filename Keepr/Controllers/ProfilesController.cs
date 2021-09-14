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

    private readonly VaultsService _vaultsService;
    private readonly KeepsService _keepsService;

    public ProfilesController(AccountService service, VaultsService vaultsService, KeepsService keepsService)
    {
      _service = service;
      _vaultsService = vaultsService;
      _keepsService = keepsService;
    }
    [HttpGet("{id}")]
    public ActionResult<Profile> Get(string id)
    {
      try
      {
        Profile profile = _service.GetProfileById(id);
        return Ok(profile);
      }
      catch (Exception err)
      {
        return BadRequest(err.Message);
      }
    }


    [HttpGet("{id}/vaults")]
    [Authorize]
    public async Task<ActionResult<List<Vault>>> GetVaults(string id)
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        List<Vault> vaults = _vaultsService.GetVaultsByCreator(id, userInfo);
        return vaults;
      }
      catch (Exception err)
      {
        return BadRequest(err.Message);
      }
    }

    [HttpGet("{id}/keeps")]
    public ActionResult<List<Keep>> GetKeeps(string id)
    {
      try
      {
        List<Keep> keeps = _keepsService.GetKeepsByCreator(id);
        return keeps;
      }
      catch (Exception err)
      {
        return BadRequest(err.Message);
      }
    }
  }
}