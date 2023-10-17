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
            try
            {
                _postNumberService.InsertNumber(dict);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
