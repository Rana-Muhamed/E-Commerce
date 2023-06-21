using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using talabat.Repository.Data;
using Talabat.Errors;

namespace Talabat.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BuggyController : BaseApiController
    {
        private readonly StoreContext _dbcontext;

        public BuggyController(StoreContext dbcontext)
        {
            _dbcontext = dbcontext;
        }
        [HttpGet("notfound")]
        public ActionResult GetNotFoundRequest() {
            var product = _dbcontext.Products.Find(100);
            if(product is null) return NotFound(new ApiResponse(404));
            return Ok(product);
        }

        [HttpGet("servererror")]//get : api/buggy/servererror
        public ActionResult GetServerError()
        {
            var product = _dbcontext.Products.Find(100);
            var productToReturn = product.ToString();//throw null exception

            return Ok(productToReturn);
        }
        [HttpGet("badrequest")]
        public ActionResult GetBadRequest()
        {
             return BadRequest(new ApiResponse(400));
        }
        [HttpGet("badrequest/{id}")]
        public ActionResult GetBadRequest(int id)// will send to it string thats validation error
        {
            return Ok();
        }

    }
}
