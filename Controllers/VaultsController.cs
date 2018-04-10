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
    public class VaultsController : Controller
    {
        private readonly VaultRepository _db;

        public VaultsController(VaultRepository repo)
        {
            _db = repo;
        }

        [HttpGet("{id}")]
        public Vault GetVault(string id)
        {
            return _db.GetVault(id);
        }

        [HttpGet("users/{userId}")]
        public IEnumerable<Vault> GetUserVaults(string userId)
        {
            IEnumerable<Vault> vaults = _db.GetUserVaults(userId);
            return vaults;
        }
        [HttpPost]
        public Vault AddVault([FromBody] AddVaultModel addVault)
        {
            if (ModelState.IsValid)
            {
                Vault vault = _db.AddVault(addVault);
                if (vault != null)
                {
                    return vault;
                }
            }
            return null;
        }

        [HttpDelete("{id}")]
        public string DeleteVault(string id)
        {
            return _db.DeleteVault(id);
        }

    }
}