using System;
using Keepr.Models;
using Keepr.Repositories;

namespace Keepr.Services
{
  public class VaultKeepsService
  {
    private readonly VaultKeepsRepository _repo;
    private readonly VaultsRepository _vaultsRepository;
    private readonly KeepsRepository _keepsRepository;

    public VaultKeepsService(VaultKeepsRepository repo, VaultsRepository vaultsRepository, KeepsRepository keepsRepository)
    {
      _repo = repo;
      _vaultsRepository = vaultsRepository;
      _keepsRepository = keepsRepository;
    }

    internal VaultKeep Create(VaultKeep newVaultKeep)
    {
      Vault vault = _vaultsRepository.GetVaultById(newVaultKeep.VaultId);
      if (vault.CreatorId != newVaultKeep.CreatorId)
      {
        throw new Exception("invalid Id");
      }
      Keep foundKeep = _keepsRepository.GetKeepById(newVaultKeep.KeepId);
      foundKeep.Keeps++;
      _keepsRepository.Update(foundKeep);
      return _repo.Create(newVaultKeep);
    }

    internal VaultKeep GetById(int id)
    {
      VaultKeep found = _repo.GetById(id);
      if (found == null)
      {
        throw new Exception("Invalid Id");
      }
      return found;
    }

    internal void Delete(int id, string userId)
    {
      VaultKeep toDelete = GetById(id);
      if (toDelete.CreatorId != userId)
      {
        throw new Exception("This isn't yours");
      }
      _repo.Delete(id);
    }
  }
}