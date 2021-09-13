using System;
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
  public class VaultKeepsController : ControllerBase
  {
    private readonly VaultKeepsService _service;
    private readonly VaultsService _vs;

    public VaultKeepsController(VaultKeepsService service, VaultsService vs)
    {
      _service = service;
      _vs = vs;
    }

    [HttpPost]
    [Authorize]

    public async Task<ActionResult<VaultKeep>> Create([FromBody] VaultKeep newVaultKeep)
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        newVaultKeep.CreatorId = userInfo.Id;
        VaultKeep vaultKeep = _service.Create(newVaultKeep);
        return Ok(vaultKeep);
      }
      catch (Exception err)
      {
        return BadRequest(err.Message);
      }
    }

    [HttpGet("{id}")]

    public ActionResult<VaultKeep> Get(int id)
    {
      try
      {
        VaultKeep vaultKeeps = _vs.GetVaultKeepsById(id);
        return Ok(vaultKeeps);
      }
      catch (Exception err)
      {
        return BadRequest(err.Message);
      }
    }
  }
}