﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sort.Service.Interface;

namespace Sort.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SortingController : ControllerBase
    {
        private readonly IWebHostEnvironment _env;
        private readonly ISortService _sortService;

        public SortingController(IWebHostEnvironment env, ISortService sortService)
        {
            _env = env;
            _sortService = sortService;
        }

        [HttpGet]
        [Route("/api/sorting/ping")]
        public IActionResult Ping()
        {
            return Ok(new
            {
                Service = Assembly.GetEntryAssembly().GetName().Name.ToString(),
                Version = Assembly.GetEntryAssembly().GetName().Version.ToString(),
                Env = _env.EnvironmentName,
            });
        }

        [HttpGet]
        [Route("/api/sorting/insertionsort/")]
        public IActionResult InsertSort([FromQuery]int arraySize)
        {
            var result = _sortService.InsertionSort(arraySize);
            return Ok(result);
        }
        
        [HttpGet]
        [Route("/api/sorting/multisort/")]
        public IActionResult MultiSort([FromQuery]int arraySize, [FromQuery]int iterations, [FromQuery]int sortType)
        {
            var result = _sortService.MultiSort(arraySize, iterations, sortType);
            return Ok(result);
        }

        [HttpGet]
        [Route("/api/sorting/mergesort/")]
        public IActionResult MergeSort([FromQuery]int arraySize)
        {
            var result = _sortService.MergeSort(arraySize);
            return Ok(result);
        }

        [HttpGet]
        [Route("/api/sorting/quicksort/")]
        public IActionResult QuickSort([FromQuery]int arraySize)
        {
            var result = _sortService.QuickSort(arraySize);
            return Ok(result);
        }
    }
}
