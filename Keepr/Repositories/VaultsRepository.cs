using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using Keepr.Models;

namespace Keepr.Repositories
{
  public class VaultsRepository
  {
    private readonly IDbConnection _db;

    public VaultsRepository(IDbConnection db)
    {
      _db = db;
    }

    internal Vault GetVaultById(int id)
    {
      string sql = @"
      SELECT
      a.*,
      v.*
      FROM vaults v 
      JOIN accounts a ON a.id = v.creatorId
      WHERE v.id = @id";
      return _db.Query<Profile, Vault, Vault>(sql, (prof, vault) =>
     {
       vault.Creator = prof;
       return vault;
     }, new { id }, splitOn: "id").FirstOrDefault();
    }

    internal Vault Create(Vault newVault)
    {
      string sql = @"
      INSERT INTO vaults
      (description, isPrivate, creatorId, name, img)
      VALUES
      (@description, @isPrivate, @CreatorId, @Name, @Img);
      SELECT LAST_INSERT_ID();";
      newVault.Id = _db.ExecuteScalar<int>(sql, newVault);
      return newVault;
    }

    internal List<Vault> GetByCreator(string id)
    {
      string sql = @"
      SELECT
      a.*,
      v.*
      FROM vaults v
      JOIN accounts a ON a.id = v.creatorId
      WHERE v.creatorId = @id;";
      return _db.Query<Profile, Vault, Vault>(sql, (prof, vault) =>
      {
        vault.Creator = prof;
        return vault;
      }, new { id }, splitOn: "id").ToList<Vault>();
    }

    internal Vault Update(Vault original)
    {
      string sql = @"
        UPDATE vaults
        SET
            name = @Name,
            description = @Description,
            img = @Img, 
            isPrivate = @IsPrivate
        WHERE id = @Id;
      ";
      _db.Execute(sql, original);
      return GetVaultById(original.Id);
    }

    internal KeepViewModel GetVaultKeepsById(int id)
    {
      string sql = @"
      SELECT
      k.*,
      v.id AS vaultKeepId,
      a.*
      FROM vaults v
      JOIN keeps k ON k.id = v.keepId
      JOIN accounts a ON a.id = k.creatorId
      WHERE vaultId = @id;";
      return _db.Query<Profile, KeepViewModel, KeepViewModel>(sql, (prof, vault) =>
           {
             vault.Creator = prof;
             return vault;
           }, new { id }, splitOn: "id").FirstOrDefault();

    }

    internal void Delete(int id)
    {
      string sql = "DELETE FROM vaults WHERE id = @id LIMIT 1;";
      _db.Execute(sql, new { id });
    }
  }
}