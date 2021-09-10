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
      *
      FROM keeps";
      return _db.Query<Keep>(sql).ToList();
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
  }
}