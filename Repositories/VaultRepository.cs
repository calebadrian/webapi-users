using System;
using System.Data;
using keepr.Models;
using Dapper;
using MySql.Data.MySqlClient;
using System.Collections.Generic;

namespace keepr.Repositories
{
    public class VaultRepository : DbContext
    {
        public VaultRepository(IDbConnection db) : base(db)
        {
        }

        public Vault GetVault(string id)
        {
            return _db.QueryFirstOrDefault<Vault>(@"
            SELECT * FROM vaults
            WHERE id = @id", new { id });
        }

        public IEnumerable<Vault> GetUserVaults(string userId)
        {
            return _db.Query<Vault>(@"
            SELECT * FROM vaults
            WHERE userId = @userId", new { userId });
        }

        public Vault AddVault(AddVaultModel addVault)
        {
            try
            {
                Guid g = Guid.NewGuid();
                string id = g.ToString();
                Vault vault = new Vault()
                {
                    Id = id,
                    Name = addVault.Name,
                    Description = addVault.Description,
                    Private = addVault.Private,
                    userId = addVault.userId,
                };
                var success = _db.Execute(@"
                INSERT INTO vaults(
                    id,
                    name,
                    description,
                    private,
                    userId
                ) VALUES (
                    @Id,
                    @Name,
                    @Description,
                    @Private,
                    @userId
                )", vault);
                if (success < 1)
                {
                    throw new Exception("VAULT ALREADY EXISTS");
                }
                return vault;
            }
            catch (MySqlException e)
            {
                System.Console.WriteLine("ERROR: " + e.Message);
                return null;
            }
        }

        public string DeleteVault(string id)
        {
            var success = _db.Execute(@"
            DELETE FROM vaults
            WHERE id = @id", new { id });
            if (success < 1)
            {
                return null;
            }
            else
            {
                return "Successfully deleted!";
            }
        }
    }
}