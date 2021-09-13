using System;
using Keepr.Models;
using Keepr.Repositories;

namespace Keepr.Services
{
  public class VaultKeepsService
  {
    private readonly VaultKeepsRepository _repo;

    public VaultKeepsService(VaultKeepsRepository repo)
    {
      _repo = repo;
    }

    internal VaultKeep Create(VaultKeep newVaultKeep)
    {
      VaultKeep vaultKeep = _repo.Create(newVaultKeep);
      if (vaultKeep == null)
      {
        throw new Exception("invalid Id");
      }
      return vaultKeep;
    }
  }
}