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
            return _db.Query<Keep>("SELECT * FROM keeps");
        }

        public Keep GetKeep(string id)
        {
            return _db.QueryFirstOrDefault(@"
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
                    userId = addKeep.userId
                };
                var success = _db.Execute(@"
                INSERT INTO keeps(
                    id,
                    name,
                    description,
                    userId
                ) VALUES (
                    @Id,
                    @Name,
                    @Description,
                    @userId
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


    }
}