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

  public class VaultsController : ControllerBase
  {
    private readonly VaultsService _service;
    private readonly KeepsService _keepsService;

    public VaultsController(VaultsService service, KeepsService keepsService)
    {
      _service = service;
      _keepsService = keepsService;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Vault>> GetVaultById(int id)
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        if (userInfo == null)
        {
          Vault privateVaults = _service.GetVaultById(id, true);
          return Ok(privateVaults);
        }
        Vault vault = _service.GetVaultById(id, false);
        if (vault.CreatorId != userInfo.Id && vault.IsPrivate == true)
        {
          throw new Exception("Not for you");
        }
        return Ok(vault);
      }
      catch (Exception err)
      {
        return BadRequest(err.Message);
      }
    }

    [HttpGet("{id}/keeps")]
    public async Task<ActionResult<List<KeepViewModel>>> GetKeeps(int id)
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        Vault vault = _service.GetVaultById(id);
        List<KeepViewModel> vaultKeep = _keepsService.GetKeepsByVaultId(vault.Id);
        if (vault.CreatorId != userInfo?.Id && vault.IsPrivate == true)
        {
          throw new Exception("Not for You");
        }
        return Ok(vaultKeep);

      }
      catch (Exception err)
      {
        return BadRequest(err.Message);
      }
    }

    [HttpPost]
    [Authorize]
    public async Task<ActionResult<Vault>> Create([FromBody] Vault newVault)
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        newVault.CreatorId = userInfo.Id;
        newVault.Creator = userInfo;
        Vault vault = _service.Create(newVault);
        return Ok(vault);
      }
      catch (Exception err)
      {
        return BadRequest(err.Message);
      }
    }


    [HttpPut("{id}")]
    [Authorize]
    public async Task<ActionResult<Vault>> Update(int id, [FromBody] Vault editedVault)
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        editedVault.CreatorId = userInfo.Id;
        editedVault.Id = id;
        Vault vault = _service.Update(editedVault);
        editedVault.Creator = userInfo;
        return Ok(vault);
      }
      catch (Exception err)
      {
        return BadRequest(err.Message);
      }
    }

    [HttpDelete("{id}")]
    [Authorize]
    public async Task<ActionResult<Vault>> Delete(int id)
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        Vault vault = _service.Delete(id, userInfo.Id);
        return Ok(vault);
      }
      catch (Exception err)
      {
        return BadRequest(err.Message);
      }
    }
  }
}