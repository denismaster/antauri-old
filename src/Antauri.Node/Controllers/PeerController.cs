using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Antauri.Core;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Antauri.Node.Controllers
{
    [Route("api/peers")]
    public class PeersController : Controller
    {
        private readonly PeerToPeerService _service;

        public PeersController(PeerToPeerService service)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
        }
        // GET api/peers/add
        [HttpPost("add")]
        public IActionResult AddPeer([FromBody] string peer)
        {
            _service.ConnectToPeer(peer).Wait();
            return Ok();
        }
    }
}