using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using WebApi.Services;

namespace WebApi.Controllers
{
    public class PostNumberController : ApiController
    {
        readonly IPostNumberService _postNumberService;
        public PostNumberController()
        {
            _postNumberService = Program.Container.GetInstance<IPostNumberService>();
        }

        // POST: http://localhost:9000/api/postnumber/
        public IHttpActionResult Post(Dictionary<string, int> dict)
        {
            var number = dict.First().Value;

            try
            {
                _postNumberService.InsertNumber(number);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest($"Failed to post the {number} due to {e.Message}");
            }
        }
    }
}
