using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sort.Service.Interface;
using Sort.Entities.Models;

namespace Sort.Api.Controllers
{
    [Route("sort/api/[controller]")]
    [ApiController]
    public class DBSortingController : ControllerBase
    {
        private readonly IWebHostEnvironment _env;
        private readonly IDBSortingService _service;

        public DBSortingController(IWebHostEnvironment env, IDBSortingService Service)
        {
            _env = env;
            _service = Service;
        }

        [HttpGet]
        [Route("GetPessoas")]
        public IActionResult GetPessoas()
        {
            var result = _service.GetPessoas();
            return Ok(result);
        }

        [HttpGet]
        [Route("GetPessoasSorted")]
        public IActionResult GetPessoasSorted()
        {
            var result = _service.GetPessoasSorted();
            return Ok(result);
        }

        [HttpPost]
        [Route("PostPessoas")]
        public IActionResult PostPessoas([FromBody]Pessoa pessoa)
        {
            var result = _service.PostPessoa(pessoa);
            return Ok(result);
        }
        
    }
}
