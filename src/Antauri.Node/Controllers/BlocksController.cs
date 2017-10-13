using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Antauri.Core;
using Microsoft.AspNetCore.Mvc;

namespace Antauri.Node.Controllers
{
    [Route("api/blocks")]
    public class BlocksController : Controller
    {
        private BlockChain _blockChain;

        public BlocksController(BlockChain blockChain){
            this._blockChain = blockChain ?? throw new ArgumentNullException(nameof(blockChain));
        }
        // GET api/blocks
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_blockChain.Blocks);
        }

        // GET api/blocks/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/blocks/mine
        [HttpPost("mine")]
        public void Post([FromBody]string value)
        {
        }
    }
}
