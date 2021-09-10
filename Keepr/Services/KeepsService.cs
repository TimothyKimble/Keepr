using System;
using System.Collections.Generic;
using Keepr.Models;
using Keepr.Repositories;

namespace Keepr.Services
{
  public class KeepsService
  {
    private readonly KeepsRepository _repo;

    public KeepsService(KeepsRepository repo)
    {
      _repo = repo;
    }

    internal List<Keep> Get()
    {
      return _repo.GetKeeps();
    }

    internal Keep Create(Keep newKeep)
    {
      Keep keep = _repo.Create(newKeep);
      if (keep == null)
      {
        throw new Exception("invalid ID");
      }
      return keep;
    }
  }
}