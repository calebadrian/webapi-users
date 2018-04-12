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
        public IEnumerable<VaultKeepReturnModel> GetKeepsByVault(string vaultId)
        {
            return _db.Query<VaultKeepReturnModel>(@"
            SELECT
                vk.id vkId,
                k.id,
                k.name,
                k.description,
                k.pic,
                k.private,
                k.userId,
                k.viewCount,
                k.shareCount,
                k.keepCount
            FROM vaultkeeps vk
            JOIN keeps k ON k.id = vk.keepId
            WHERE vaultId = @vaultId", new { vaultId });
        }

        public Vault GetVaultForKeep(string keepId, string userId)
        {
            return _db.QueryFirstOrDefault<Vault>(@"
            SELECT
                v.id,
                v.name,
                v.description,
                v.private,
                v.userId
            FROM vaultkeeps vk
            JOIN vaults v on v.id = vk.vaultId
            WHERE keepId = @keepId AND vk.userId = @userId", new {keepId, userId});
        }

        public int DeleteVaultKeep(string id)
        {
            var success = _db.Execute(@"
            DELETE FROM vaultkeeps
            WHERE id = @id", new { id });
            if (success < 1)
            {
                throw new Exception("COULD NOT DELETE");
            }
            return success;
        }


    }
}