using Microsoft.AspNetCore.Mvc;

namespace Controllers.V1
{
    [ApiController]
    [Route("senior-globaltec-challenge/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    public class BaseController : ControllerBase { }
}