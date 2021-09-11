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
  public class KeepsController : ControllerBase
  {
    private readonly KeepsService _service;

    public KeepsController(KeepsService service)
    {
      _service = service;
    }
    // NOTE Get all keeps
    [HttpGet]
    public ActionResult<List<Keep>> Get()
    {
      try
      {
        List<Keep> keeps = _service.Get();
        return Ok(keeps);
      }
      catch (Exception err)
      {
        return BadRequest(err.Message);
      }
    }
    // NOTE Create a Keep
    [HttpPost]
    [Authorize]
    public async Task<ActionResult<Keep>> Create([FromBody] Keep newKeep)
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        newKeep.CreatorId = userInfo.Id;
        newKeep.Creator = userInfo;
        Keep keep = _service.Create(newKeep);
        return Ok(keep);
      }
      catch (Exception err)
      {
        return BadRequest(err.Message);
      }
    }

    [HttpGet("{id}")]

    public ActionResult<Keep> Get(int id)
    {
      try
      {
        Keep keep = _service.GetKeepById(id);
        return Ok(keep);
      }
      catch (Exception err)
      {
        return BadRequest(err.Message);
      }
    }

    [HttpPut("{id}")]
    [Authorize]
    public async Task<ActionResult<Keep>> Update(int id, [FromBody] Keep editedKeep)
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        editedKeep.CreatorId = userInfo.Id;
        editedKeep.Id = id;
        Keep keep = _service.Update(editedKeep);
        return Ok(keep);
      }
      catch (Exception err)
      {
        return BadRequest(err.Message);
      }
    }

    [HttpDelete("{id}")]
    [Authorize]
    public async Task<ActionResult<Keep>> Delete(int id)
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        Keep Keep = _service.Delete(id, userInfo.Id);
        return Ok(Keep);
      }
      catch (Exception err)
      {
        return BadRequest(err.Message);
      }
    }
  }
}