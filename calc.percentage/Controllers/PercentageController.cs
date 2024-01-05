using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Dapr.Client;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace calc.percentage.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PercentageController : ControllerBase
    {
        string MultiplicationEndpoint = null;
        string DivisionEndpoint = null;
        private readonly DaprClient _dapr;
        private readonly ILogger<PercentageController> _logger;

        public PercentageController(ILogger<PercentageController> logger, DaprClient dapr)
        {
            _logger = logger;
            _dapr = dapr;
        }
    
       
        [HttpPost]
        [Route("percentage")]
        public async Task<int> Percentage([FromBody]OperationParameters p)
        { 
            int[] operands = new int[2] { 0, 100 };
            return await Calculate(await Calculate(p.op1, p.op2, "multiplication", "multiplication/multiply"), operands[new Random().Next(0, 2)], "division", "division/divide"); 
        }

        async Task<int> Calculate(int op1, int op2,string api, string operation)
        {
            return Convert.ToInt32((await _dapr.InvokeMethodAsync<object, object>(
                api, // The name of the Dapr application
                operation, // The method to invoke
                new OperationParameters
                {
                    op1 = op1,
                    op2 = op2
                })).ToString());            
        }

      
    }

    public class OperationParameters
    {
        public int op1 { get; set; }
        public int op2 { get; set; }
    }
}
