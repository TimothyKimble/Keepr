using System;
using System.Collections.Generic;
using Keepr.Models;
using Keepr.Repositories;

namespace Keepr.Services
{
  public class VaultsService
  {
    private readonly VaultsRepository _repo;

    public VaultsService(VaultsRepository repo)
    {
      _repo = repo;
    }
    internal Vault GetVaultById(int id, bool careIfPrivate = false)
    {
      Vault found = _repo.GetVaultById(id);
      if (found == null || (careIfPrivate == true && found.IsPrivate == true))
      {
        throw new Exception("This isn't for you");
      }
      return found;
    }

    internal Vault Create(Vault newVault)
    {
      return _repo.Create(newVault);
    }

    internal Vault Update(Vault editedVault)
    {
      Vault original = GetVaultById(editedVault.Id, true);
      if (original.CreatorId != editedVault.CreatorId)
      {
        throw new Exception("Invalid Access");
      }
      original.Name = editedVault.Name.Length > 0 ? editedVault.Name : original.Name;
      original.Description = editedVault.Description.Length > 0 ? editedVault.Description : original.Description;
      original.Img = editedVault.Img.Length > 0 ? editedVault.Img : original.Img;
      original.IsPrivate = editedVault.IsPrivate ?? original.IsPrivate;
      _repo.Update(original);
      return original;
    }

    internal List<Vault> GetVaultsByCreator(string id, Account userInfo)
    {
      List<Vault> vaults = _repo.GetByCreator(id);
      if (userInfo == null)
      {
        vaults = vaults.FindAll(v => v.IsPrivate == false);
      }
      return vaults;

    }

    internal Vault Delete(int vaultId, string userId)
    {
      Vault vaultToDelete = GetVaultById(vaultId);
      if (vaultToDelete.CreatorId != userId)
      {
        throw new Exception("Invalid Access");
      }
      _repo.Delete(vaultId);
      return vaultToDelete;
    }
  }
}