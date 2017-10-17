using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Antauri.Core;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Antauri.Node.Controllers
{
    [Route("api/blocks")]
    public class BlocksController : Controller
    {
        private BlockChain _blockChain;
        private readonly PeerToPeerService _p2PService;

        public BlocksController(BlockChain blockChain, PeerToPeerService p2pService){
            _blockChain = blockChain ?? throw new ArgumentNullException(nameof(blockChain));
            _p2PService = p2pService ?? throw new ArgumentNullException(nameof(p2pService));
        }
        // GET api/blocks
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_blockChain.Blocks);
        }

        // GET api/blocks/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            return Ok(_blockChain.Blocks.FirstOrDefault(b=>b.Index==id));
        }

        // POST api/blocks/mine
        [HttpPost("mine")]
        public async void Post([FromBody]string value)
        {
            Block newBlock = _blockChain.MineBlock(value);
            _blockChain.Add(newBlock);
            await _p2PService.Broadcast(_p2PService.ResponseLatestMessage());
            string s = JsonConvert.SerializeObject(newBlock);
            Console.WriteLine("block added: " + s);
        }
    }
}
