using Microsoft.AspNetCore.Mvc;
using StudentGradings.BLL.Interfaces;
using StudentGradings.BLL.Models;

namespace StudentGradings.API.Controllers;

[ApiController]
[Route("api/graduate")]
public class GraduateController(IGraduatesService graduatesService) : ControllerBase
{

    [HttpPost]
    public void SendGraduate()
    {
        // validate the data
        graduatesService.SendGraduate(new GraduateModelBll { UserId = 5 });
        // send a response
    }
}
