using System;
using System.Data;
using keepr.Models;
using Dapper;
using MySql.Data.MySqlClient;
using System.Collections.Generic;

namespace keepr.Repositories
{
    public class KeepRepository : DbContext
    {
        public KeepRepository(IDbConnection db) : base(db)
        {
        }

        public IEnumerable<Keep> GetAllKeeps()
        {
            return _db.Query<Keep>("SELECT * FROM keeps WHERE private = 0");
        }

        public Keep GetKeep(string id)
        {
            return _db.QueryFirstOrDefault<Keep>(@"
            SELECT * FROM keeps
            WHERE id = @id", new { id });
        }

        public Keep AddKeep(AddKeepModel addKeep)
        {
            try
            {
                Guid g = Guid.NewGuid();
                string id = g.ToString();
                Keep keep = new Keep()
                {
                    Id = id,
                    Name = addKeep.Name,
                    Description = addKeep.Description,
                    Pic = addKeep.Pic,
                    Private = addKeep.Private,
                    userId = addKeep.userId,
                    keepCount = addKeep.keepCount,
                    shareCount = addKeep.shareCount,
                    viewCount = addKeep.viewCount
                };
                var success = _db.Execute(@"
                INSERT INTO keeps(
                    id,
                    name,
                    description,
                    pic,
                    private,
                    userId,
                    keepCount,
                    shareCount,
                    viewCount
                ) VALUES (
                    @Id,
                    @Name,
                    @Description,
                    @Pic,
                    @Private,
                    @userId,
                    @keepCount,
                    @shareCount,
                    @viewCount
                )", keep);
                if (success < 1)
                {
                    throw new Exception("KEEP ALREADY EXISTS");
                }
                return keep;
            }
            catch (MySqlException e)
            {
                Console.WriteLine("ERROR: " + e.Message);
                return null;
            }
        }

        public Keep EditKeep(Keep keep)
        {
            var success = _db.Execute(@"
            UPDATE keeps
            SET
            name = @Name,
            description = @Description,
            keepCount = @keepCount,
            shareCount = @shareCount,
            viewCount = @viewCount
            WHERE id = @Id", keep);
            if (success < 1)
            {
                throw new Exception("COULD NOT UPDATE");
            }
            return keep;
        }

        public IEnumerable<Keep> GetUserKeeps(string id)
        {
            return _db.Query<Keep>(@"
            SELECT * FROM keeps
            WHERE userId = @id", new {id});
        }


    }
}