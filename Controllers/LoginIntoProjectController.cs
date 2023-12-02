using System;
using System.Xml.Linq;
using KSU_BProcesses.Models;
using KSU_BProcesses.Services;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;


namespace KSU_BProcesses.Controllers;


[ApiController]
[Route("login/[controller]")]
public class LoginIntoProjectController : ControllerBase
{

    private readonly LoginIntoProject _loginService;

    public LoginIntoProjectController(LoginIntoProject loginService) =>
        _loginService = loginService;

    [DisableCors]
    [HttpGet]
    public async Task<List<LoginIntoProjectModel>> Get() =>
        await _loginService.GetAsync();


    [DisableCors]
    [HttpGet("getUserById/{id:length(24)}")]
    public async Task<ActionResult<LoginIntoProjectModel>> Get(string id)
    {
        var loginInfo = await _loginService.GetAsync(id);

        if (loginInfo is null)
        {
            return NotFound();
        }

        return loginInfo;
    }




    [HttpPost("addNewUser")]
    public async Task<IActionResult> Post(LoginIntoProjectModel newLogin)
    {
        await _loginService.CreateAsync(newLogin);

        return CreatedAtAction(nameof(Get), new { id = newLogin.Id }, newLogin);
    }








    [DisableCors]
    [HttpPut("changeUserInfo/{id:length(24)}")]
    public async Task<IActionResult> Update(string id, LoginIntoProjectModel updatedLoginIntoProjectModel)
    {
        var loginInfo = await _loginService.GetAsync(id);

        if (loginInfo is null)
        {
            return NotFound();
        }

        updatedLoginIntoProjectModel.Id = loginInfo.Id;

        await _loginService.UpdateAsync(id, updatedLoginIntoProjectModel);

        return NoContent();
    }


    [DisableCors]
    [HttpDelete("deleteUser/{id:length(24)}")]
    public async Task<IActionResult> Delete(string id)
    {
        var loginInfo = await _loginService.GetAsync(id);

        if (loginInfo is null)
        {
            return NotFound();
        }

        await _loginService.RemoveAsync(id);

        return NoContent();
    }



    [DisableCors]
    [HttpGet("checkUserAutorize/{login}/{password}")]
    public async Task<ActionResult<LoginIntoProjectModel>> GetCheckProfileIntoSystem(string login, string password)
    {
        var loginInfo = await _loginService.CheckProfileIntoSystem(login, password);

        //var res = new{
        //    user_name = loginInfo.user_name,
        //    second_name = loginInfo.second_name,
        //    role = loginInfo.role
        //};


        //string jsonString = JsonConvert.SerializeObject(res);


        if (loginInfo is null)
        {
            return NotFound();
        }

        return loginInfo;
    }





}

