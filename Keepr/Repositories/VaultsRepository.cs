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

    public List<Vault> GetVaults()
    {
      string sql = @"
      SELECT * FROM vaults;";
      return _db.Query<Vault>(sql).ToList();
    }

    public Vault GetVaultById(int id)
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

    public Vault Create(Vault newVault)
    {
      string sql = @"
      INSERT INTO vaults
      (description, isPrivate, creatorId, name)
      VALUES
      (@description, @isPrivate, @CreatorId, @Name);
      SELECT LAST_INSERT_ID();";
      newVault.Id = _db.ExecuteScalar<int>(sql, newVault);
      return newVault;
    }

    public Vault Update(Vault original)
    {
      string sql = @"
        UPDATE vaults
        SET
            name = @Name,
            description = @Description, 
            isPrivate = @IsPrivate
        WHERE id = @Id;
      ";
      _db.Execute(sql, original);
      return GetVaultById(original.Id);
    }

    internal void Delete(int id)
    {
      string sql = "DELETE FROM vaults WHERE id = @id LIMIT 1;";
      _db.Execute(sql, new { id });
    }
  }
}