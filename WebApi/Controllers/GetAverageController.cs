using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using WebApi.Services;

namespace WebApi.Controllers
{
    public class GetAverageController : ApiController
    {
        readonly IGetAverageService _getAverageService;

        public GetAverageController()
        {
            _getAverageService = Program.Container.GetInstance<IGetAverageService>();
        }

        // GET http://localhost:9000/api/getaverage/
        public Dictionary<string, string> Get()
        {
            return _getAverageService.GetAverage();
        }
    }
}
