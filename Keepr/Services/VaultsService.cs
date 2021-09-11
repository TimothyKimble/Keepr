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

    internal List<Vault> GetVaults()
    {
      return _repo.GetVaults();
    }

    internal Vault GetVaultById(int id)
    {
      Vault found = _repo.GetVaultById(id);
      if (found == null)
      {
        throw new Exception("Invalid Id");
      }
      return found;
    }

    internal Vault Create(Vault newVault)
    {
      return _repo.Create(newVault);
    }

    internal Vault Update(Vault editedVault)
    {
      Vault original = GetVaultById(editedVault.Id);
      if (original.CreatorId != editedVault.CreatorId)
      {
        throw new Exception("Invalid Access");
      }
      original.Name = editedVault.Name.Length > 0 ? editedVault.Name : original.Name;
      original.Description = editedVault.Description.Length > 0 ? editedVault.Description : original.Description;
      original.IsPrivate = editedVault.IsPrivate ?? original.IsPrivate;
      return _repo.Update(original);
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