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

    internal Keep GetKeepById(int id)
    {
      Keep foundKeep = _repo.GetKeepById(id);
      if (foundKeep == null)
      {
        throw new Exception("Invalid Id");
      }
      return foundKeep;
    }

    internal Keep Update(Keep editedKeep)
    {
      Keep original = GetKeepById(editedKeep.Id);
      if (original.CreatorId != editedKeep.CreatorId)
      {
        throw new Exception("Invalid Access");
      }
      original.Name = editedKeep.Name.Length > 0 ? editedKeep.Name : original.Name;
      original.Description = editedKeep.Description.Length > 0 ? editedKeep.Description : original.Description;
      original.Img = editedKeep.Img.Length > 0 ? editedKeep.Img : original.Img;
      return _repo.Update(original);
    }

    internal Keep Delete(int keepId, string userId)
    {
      Keep keepToDelete = GetKeepById(keepId);
      if (keepToDelete.CreatorId != userId)
      {
        throw new Exception("Invalid Access");
      }
      _repo.Delete(keepId);
      return keepToDelete;
    }
  }
}