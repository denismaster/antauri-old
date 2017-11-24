using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Antauri.Core;
using Microsoft.Extensions.Logging;

namespace Antauri_WebExplorer.Controllers
{
    [Route("api/[controller]")]
    public class SampleDataController : Controller
    {
        private SimpleBlockChain _blockChain;
        private readonly ILogger<SampleDataController> _logger;
        private readonly IBlockFactory<SimpleBlock, string> _blockFactory;

        public SampleDataController(SimpleBlockChain blockChain,
        ILogger<SampleDataController> logger,
        IBlockFactory<SimpleBlock, string> blockFactory
        )
        {
            _blockChain = blockChain ?? throw new ArgumentNullException(nameof(blockChain));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _blockFactory = blockFactory;
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
            return Ok(_blockChain.Blocks.FirstOrDefault(b => b.Index == id));
        }

        // POST api/blocks/mine
        [HttpPost("mine")]
        public async void Post([FromBody]string value)
        {
            SimpleBlock lastBlock = _blockChain.LatestBlock;
            SimpleBlock newBlock = _blockFactory.CreateBlock(lastBlock, value);
            _blockChain.Add(newBlock);
            //await _p2PService.Broadcast(_p2PService.ResponseLatestMessage());
            string s = JsonConvert.SerializeObject(newBlock);
            _logger.LogInformation("Block added: " + s);
        }

        [HttpGet("[action]")]
        public IEnumerable<BlockDto> WeatherForecasts()
        {
            return _blockChain.Blocks.Select(block => new BlockDto()
            {
                Hash = block.Hash,
                Index = block.Index,
                Timestamp = block.TimeStamp,
                PrevHash = block.PreviousHash,
                Data = block.Data
            });
        }

        public class BlockDto
        {
            public string Hash { get; set; }
            public string PrevHash { get; set; }
            public int Index { get; set; }
            public string Data { get; set; }
            public long Timestamp { get; set; }
        }
    }
}
