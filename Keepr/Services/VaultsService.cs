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
      List<Vault> vault = _repo.GetVaults();
      return vault.FindAll(v => v.IsPrivate == false);
    }

    internal Vault GetVaultById(int id, bool careIfPrivate = true)
    {
      Vault found = _repo.GetVaultById(id);
      if (found == null || careIfPrivate && found.IsPrivate == true)
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
      original.Img = editedVault.Img.Length > 0 ? editedVault.Img : original.Img;
      original.IsPrivate = editedVault.IsPrivate ?? original.IsPrivate;
      _repo.Update(original);
      return original;
    }

    internal VaultKeep GetVaultKeepsById(int id)
    {
      VaultKeep vaultkeeps = _repo.GetVaultKeepsById(id);
      return vaultkeeps;
    }

    internal List<Vault> GetVaultsByCreator(string creatorId, bool careIfPrivate = true)
    {
      List<Vault> vaults = _repo.GetVaults(creatorId);
      if (careIfPrivate)
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