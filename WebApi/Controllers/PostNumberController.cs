using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace WebApi.Controllers
{
    public class PostNumberController : ApiController
    {
        // POST: http://localhost:9000/api/postnumber/
        public IHttpActionResult Post(Dictionary<string, int> dict)
        {
            var number = dict.First().Value;

            if (ApplicationContext.UsedNumbers.Contains(number))
            {
                return BadRequest($"The number {number} is already posted and is rejected");
            }

            ApplicationContext.UsedNumbers.Add(number);

            return Ok();
        }
    }
}
