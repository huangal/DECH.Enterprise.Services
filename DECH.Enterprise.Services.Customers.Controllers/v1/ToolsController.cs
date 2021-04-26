using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using DECH.Enterprise.Services.Customers.Engines;
using DECH.Enterprise.Services.Customers.Engines.Security;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DECH.Enterprise.Services.Customers.Controllers.v1
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("v{version:apiVersion}/[controller]")]
    public class ToolsController : ControllerBase
    {
        [HttpPost("json/Minify")]
        [Consumes("application/json")]
        public IActionResult JsonMinifier([FromBody] object document)
        {
            if (document == null) return BadRequest();

            //var options = new System.Text.Json.JsonSerializerOptions();
            //options.AllowTrailingCommas = true;

            string jsonText = System.Text.Json.JsonSerializer.Serialize(document);


            var content = Regex.Replace(jsonText, @"(""[^""\\]*(?:\\.[^""\\]*)*"")|\s+", "$1");


            var response = Regex.Replace(jsonText, @"\s(?=([^""]*""[^""]*"")*[^""]*$)", string.Empty);



            return Ok(jsonText);


        }

        [HttpPost("json/Minify1")]
        [Consumes("application/json")]
        public IActionResult JsonMinifier1([FromBody] object document)
        {
            if (document == null) return BadRequest();

            //var options = new System.Text.Json.JsonSerializerOptions();
            //options.AllowTrailingCommas = true;

            string jsonText = System.Text.Json.JsonSerializer.Serialize(document);


            var content = Regex.Replace(jsonText, @"(""[^""\\]*(?:\\.[^""\\]*)*"")|\s+", "$1");

            return Ok(content);


        }

        [HttpPost("json/Minify2")]
        [Consumes("application/json")]
        public IActionResult JsonMinifier2([FromBody] object document)
        {
            if (document == null) return BadRequest();

            string jsonText = System.Text.Json.JsonSerializer.Serialize(document);

            var response = Regex.Replace(jsonText, @"\s(?=([^""]*""[^""]*"")*[^""]*$)", string.Empty);

            return Ok(response);


        }


        [HttpPost("test")]
        public IActionResult JsonMinify([FromBody] string value)
        {
            if (value == null) return BadRequest();

            value = "[	{		\"id\": \"0001\",		\"type\": \"donut\",		\"name\": \"Cake\",		\"ppu\": 0.55,		\"batters\":			{				\"batter\":					[						{ \"id\": \"1001\", \"type\": \"Regular\" },						{ \"id\": \"1002\", \"type\": \"Chocolate\" },						{ \"id\": \"1003\", \"type\": \"Blueberry\" },						{ \"id\": \"1004\", \"type\": \"Devil's Food\" }					]			},		\"topping\":			[				{ \"id\": \"5001\", \"type\": \"None\" },				{ \"id\": \"5002\", \"type\": \"Glazed\" },			{ \"id\": \"5005\", \"type\": \"Sugar\" },				{ \"id\": \"5007\", \"type\": \"Powdered Sugar\" },				{ \"id\": \"5006\", \"type\": \"Chocolate with Sprinkles\" },				{ \"id\": \"5003\", \"type\": \"Chocolate\" },			{ \"id\": \"5004\", \"type\": \"Maple\" }			]	},	{		\"id\": \"0002\",		\"type\": \"donut\",		\"name\": \"Raised\",		\"ppu\": 0.55,		\"batters\":			{				\"batter\":					[						{ \"id\": \"1001\", \"type\": \"Regular\" }					]			},		\"topping\":			[				{ \"id\": \"5001\", \"type\": \"None\" },				{ \"id\": \"5002\", \"type\": \"Glazed\" },				{ \"id\": \"5005\", \"type\": \"Sugar\" },				{ \"id\": \"5003\", \"type\": \"Chocolate\" },				{ \"id\": \"5004\", \"type\": \"Maple\" }			]	},	{		\"id\": \"0003\",		\"type\": \"donut\",		\"name\": \"Old Fashioned\",		\"ppu\": 0.55,		\"batters\":			{				\"batter\":					[						{ \"id\": \"1001\", \"type\": \"Regular\" },						{ \"id\": \"1002\", \"type\": \"Chocolate\" }					]			},		\"topping\":			[				{ \"id\": \"5001\", \"type\": \"None\" },				{ \"id\": \"5002\", \"type\": \"Glazed\" },				{ \"id\": \"5003\", \"type\": \"Chocolate\" },				{ \"id\": \"5004\", \"type\": \"Maple\" }			]	}]";

			var details = System.Text.Json.JsonSerializer.Deserialize<object>(value);           //JObject.Parse(value);

            string jsonText = System.Text.Json.JsonSerializer.Serialize(details);


            var content = Regex.Replace(value, @"(""[^""\\]*(?:\\.[^""\\]*)*"")|\s+", "$1");


            var response = Regex.Replace(value, @"\s(?=([^""]*""[^""]*"")*[^""]*$)", string.Empty);



            return Ok(jsonText);



        }



        [HttpPost("Encrypt")]
        public IActionResult Encrypt(string data)
        {
            string secretKey = "dR7s?ufreWa=r*G_ZEphA*Ra";

            var symmetricEncryptDecrypt = new SymmetricTripleDesCipher();
            CryptoItem response = symmetricEncryptDecrypt.Encrypt(data, secretKey);
            return Ok(new { Value = response.EncryptedData, IV = response.IV});

        }

        [HttpPost("Decrypt")]
        public IActionResult Decrypt( string data, string iv)
        {
            string secretKey = "dR7s?ufreWa=r*G_ZEphA*Ra"; //  "xpcNNt61zjnjIaIZGfsrLUIyJ1hCORcL";

            var symmetricEncryptDecrypt = new SymmetricTripleDesCipher();
            var decryptedText = symmetricEncryptDecrypt.Decrypt(data, iv, secretKey);

            return Ok(decryptedText);

        }


    }
}