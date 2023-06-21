﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.Errors;

namespace Talabat.Controllers
{
    [Route("errors/{code}")]
    [ApiController]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class ErorrsController : ControllerBase
    {
        public ActionResult Error(int code)
        {

            return NotFound(new ApiResponse(code));
        }
    }
}
