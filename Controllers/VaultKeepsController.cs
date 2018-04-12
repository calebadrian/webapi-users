using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using keepr.Models;
using keepr.Repositories;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace keepr.Controllers
{
    [Route("api/[controller]")]
    public class VaultKeepsController : Controller
    {
        private readonly VaultKeepRepository _db;

        public VaultKeepsController(VaultKeepRepository repo)
        {
            _db = repo;
        }

        [HttpPost]
        public VaultKeep AddVaultKeep([FromBody] AddVaultKeepModel addVaultKeep)
        {
            if (ModelState.IsValid)
            {
                VaultKeep vaultKeep = _db.AddVaultKeep(addVaultKeep);
                if (vaultKeep != null)
                {
                    return vaultKeep;
                }
            }
            return null;
        }

        [HttpGet("{vaultId}")]
        public IEnumerable<VaultKeepReturnModel> GetKeepsByVault(string vaultId)
        {
            return _db.GetKeepsByVault(vaultId);
        }

        [HttpDelete("{id}")]

        public int DeleteVaultKeep(string id)
        {
            return _db.DeleteVaultKeep(id);
        }

        [HttpGet("{keepId}/{userId}")]
        public Vault GetVaultForKeep(string keepId, string userId)
        {
            return _db.GetVaultForKeep(keepId, userId);
        }
    }
}