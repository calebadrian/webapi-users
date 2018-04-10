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
    public class KeepsController : Controller
    {
        private readonly KeepRepository _db;

        public KeepsController(KeepRepository repo)
        {
            _db = repo;
        }

        [HttpGet]
        public IEnumerable<Keep> GetAllKeeps()
        {
            return _db.GetAllKeeps();
        }

        [HttpGet("{id}")]
        public Keep GetKeep(string id)
        {
            return _db.GetKeep(id);
        }

        [HttpPost]
        public Keep AddKeep([FromBody] AddKeepModel addKeep)
        {
            if (ModelState.IsValid)
            {
                Keep keep = _db.AddKeep(addKeep);
                if (keep != null)
                {
                    return keep;
                }
            }
            return null;
        }

        [HttpPut]
        public Keep EditKeep([FromBody] Keep keep)
        {
            if (ModelState.IsValid)
            {
                Keep newKeep = _db.EditKeep(keep);
                if (newKeep != null)
                {
                    return newKeep;
                }
            }
            return null;
        }

    }
}