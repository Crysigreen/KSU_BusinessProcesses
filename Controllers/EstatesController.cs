using System;
using System.Xml.Linq;
using KSU_BProcesses.Models;
using KSU_BProcesses.Services;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;


namespace KSU_BProcesses.Controllers;


[ApiController]
[Route("estate/[controller]")]
public class EstatesController : ControllerBase
{
    private readonly EstateService _directoryStorageService;

    public EstatesController(EstateService directoryStorageService) =>
        _directoryStorageService = directoryStorageService;


    //public static bool Contains(this string s, char c)
    //{
    //    return s.IndexOf(c, StringComparison.CurrentCultureIgnoreCase) != -1;
    //}
    //public static bool ContainsIgnoreCase(this string s, char c)
    //{
    //    return s.IndexOf(c, StringComparison.InvariantCultureIgnoreCase) != -1;
    //}

    [DisableCors]
    [HttpGet("Get")]
    public async Task<List<Estate>> Get() =>
        await _directoryStorageService.GetAsync();


    //[DisableCors]
    //[HttpGet]
    //public async Task<List<DirectoryStorageModel>> Get() =>
    //   await _directoryStorageService.GetAsync();



   




    //private List<Models.Directory> Ok(List<Models.Directory> data)
    //{
    //    throw new NotImplementedException();
    //}




    //[DisableCors]
    //[HttpGet("getPathDirectiveId/{id:length(24)}")]
    //public async Task<ActionResult<DirectoryStorageModel>> Get(string id)
    //{
    //    var loginInfo = await _directoryStorageService.GetAsync(id);

    //    if (loginInfo is null)
    //    {
    //        return NotFound();
    //    }

    //    return loginInfo;
    //}




    //[HttpPost("addNewPAthDirective")]
    //public async Task<IActionResult> Post(DirectoryStorageModel newLogin)
    //{
    //    await _directoryStorageService.CreateAsync(newLogin);

    //    return CreatedAtAction(nameof(Get), new { id = newLogin.Id }, newLogin);
    //}








    [DisableCors]
    [HttpPut("changeDirectivePathInfo/{id:length(24)}")]
    public async Task<IActionResult> Update(string id, Estate updatedDirectoryStorageModel)
    {
        var loginInfo = await _directoryStorageService.GetAsync(id);

        if (loginInfo is null)
        {
            return NotFound();
        }

        updatedDirectoryStorageModel.Id = loginInfo.Id;

        await _directoryStorageService.UpdateAsync(id, updatedDirectoryStorageModel);

        return NoContent();
    }


    [DisableCors]
    [HttpDelete("deletePath/{id:length(24)}")]
    public async Task<IActionResult> Delete(string id)
    {
        var loginInfo = await _directoryStorageService.GetAsync(id);

        if (loginInfo is null)
        {
            return NotFound();
        }

        await _directoryStorageService.RemoveAsync(id);

        return NoContent();
    }








    [DisableCors]
    [HttpGet("getEstateById/{id}")]
    public async Task<ActionResult<Estate>> GetCheckProfileIntoSystem(string id)
    {
        var values = new List<string> { };



        var directiveStorageInfo = await _directoryStorageService.GetInfoEstateByIdIntoSystem(id);


        if (directiveStorageInfo is null)
        {
            return NotFound();
        }

        return directiveStorageInfo;
    }




    [DisableCors]
    [HttpGet("getEstateByAddress/{address}")]
    public async Task<ActionResult<Estate>> GetEstateByAddress(string address)
    {
        var values = new List<string> { };



        var directiveStorageInfo = await _directoryStorageService.GetEstateByAddress(address);


        if (directiveStorageInfo is null)
        {
            return NotFound();
        }

        return directiveStorageInfo;
    }
}

//internal record struct NewStruct(string message1, string message2, List<Models.Directory> direct)
//{
//    public static implicit operator (string message1, string message2, List<Models.Directory> direct)(NewStruct value)
//    {
//        return (value.message1, value.message2, value.direct);
//    }

//    public static implicit operator NewStruct((string message1, string message2, List<Models.Directory> direct) value)
//    {
//        return new NewStruct(value.message1, value.message2, value.direct);
//    }
//}



