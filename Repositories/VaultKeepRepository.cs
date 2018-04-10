using System;
using System.Data;
using keepr.Models;
using Dapper;
using MySql.Data.MySqlClient;
using System.Collections.Generic;

namespace keepr.Repositories
{
    public class VaultKeepRepository : DbContext
    {
        public VaultKeepRepository(IDbConnection db) : base(db)
        {
        }

        public VaultKeep AddVaultKeep(AddVaultKeepModel addVaultKeep)
        {
            try
            {
                Guid g = Guid.NewGuid();
                string id = g.ToString();
                VaultKeep vaultKeep = new VaultKeep()
                {
                    Id = id,
                    VaultId = addVaultKeep.VaultId,
                    KeepId = addVaultKeep.KeepId,
                    userId = addVaultKeep.userId
                };
                var success = _db.Execute(@"
                INSERT INTO vaultkeeps(
                    id,
                    vaultId,
                    keepId,
                    userId
                ) VALUES (
                    @Id,
                    @VaultId,
                    @KeepId,
                    @userId
                )", vaultKeep);
                if (success < 1)
                {
                    throw new Exception("VAULT KEEP ALREADY EXISTS");
                }
                return vaultKeep;
            }
            catch (MySqlException e)
            {
                Console.WriteLine("ERROR: " + e.Message);
                return null;
            }
        }
        public IEnumerable<Keep> GetKeepsByVault(string vaultId)
        {
            return _db.Query<Keep>(@"
            SELECT
                k.id,
                k.name,
                k.description,
                k.userId,
                k.viewCount,
                k.shareCount,
                k.keepCount
            FROM vaultkeeps vk
            JOIN keeps k ON k.id = vk.keepId
            WHERE vaultId = @vaultId", new {vaultId});
        }


    }
}