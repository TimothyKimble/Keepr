using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using Keepr.Models;

namespace Keepr.Repositories
{
  public class KeepsRepository
  {
    private readonly IDbConnection _db;

    public KeepsRepository(IDbConnection db)
    {
      _db = db;
    }

    internal List<Keep> GetKeeps()
    {
      string sql = @"
      SELECT
      a.*,
      k.*
      FROM keeps k
      JOIN accounts a ON a.id = k.creatorId;";
      return _db.Query<Profile, Keep, Keep>(sql, (prof, keep) =>
      {
        keep.Creator = prof;
        return keep;
      }, splitOn: "id").ToList<Keep>();
    }

    public Keep Create(Keep newKeep)
    {
      string sql = @"
      INSERT INTO keeps
      (name, description, img, creatorId, views, shares, keeps)
      VALUES
      (@Name, @Description, @Img, @CreatorId, @Views, @Shares, @Keeps);
      SELECT LAST_INSERT_ID();";
      newKeep.Id = _db.ExecuteScalar<int>(sql, newKeep);
      return newKeep;

    }

    internal Keep GetKeepById(int id)
    {
      string sql = @"
      SELECT
      a.*,
      k.*
      FROM keeps k 
      JOIN accounts a ON a.id = k.creatorId
      WHERE k.id = @id";
      return _db.Query<Profile, Keep, Keep>(sql, (prof, keep) =>
     {
       keep.Creator = prof;
       return keep;
     }, new { id }, splitOn: "id").FirstOrDefault();
    }

    internal Keep Update(Keep original)
    {
      string sql = @"
        UPDATE keeps
        SET
            name = @Name,
            description = @Description, 
            img = @Img,
            views = @Views,
            keeps = @Keeps
        WHERE id = @Id;
      ";
      _db.Execute(sql, original);
      return GetKeepById(original.Id);
    }

    internal List<Keep> GetKeepsByCreator(string id)
    {
      string sql = @"
      SELECT
      a.*,
      k.*
      FROM keeps k
      JOIN accounts a ON a.id = k.creatorId
      WHERE k.creatorId = @id;";
      return _db.Query<Profile, Keep, Keep>(sql, (prof, keep) =>
      {
        keep.Creator = prof;
        return keep;
      }, new { id }, splitOn: "id").ToList<Keep>();
    }

    internal List<KeepViewModel> GetKeepByVaultId(int id)
    {
      string sql = @"
      SELECT
      a.*,
      k.*,
      vk.id AS vaultKeepId
      FROM vaultKeeps vk
      JOIN keeps k on k.id = vk.keepId
      JOIN accounts a ON a.id = k.creatorId
      WHERE vaultId = @id;";
      return _db.Query<Profile, KeepViewModel, KeepViewModel>(sql, (prof, keep) =>
      {
        keep.Creator = prof;
        return keep;
      }, new { id }, splitOn: "id").ToList<KeepViewModel>();
    }

    internal void Delete(int id)
    {
      string sql = "DELETE FROM keeps WHERE id = @id LIMIT 1;";
      _db.Execute(sql, new { id });
    }
  }
}