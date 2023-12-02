using KSU_BProcesses.Models;
using KSU_BProcesses.Services;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace KSU_BProcesses.Controllers;

[ApiController]
[Route("directoryStorage/[controller]")]
public class DirectoryStorageController : ControllerBase
{


    private readonly DirectoryStorage _directoryStorageService;

    public DirectoryStorageController(DirectoryStorage directoryStorageService) =>
        _directoryStorageService = directoryStorageService;


    //public static bool Contains(this string s, char c)
    //{
    //    return s.IndexOf(c, StringComparison.CurrentCultureIgnoreCase) != -1;
    //}
    //public static bool ContainsIgnoreCase(this string s, char c)
    //{
    //    return s.IndexOf(c, StringComparison.InvariantCultureIgnoreCase) != -1;
    //}

    
    [HttpGet("Get")]
    [EnableCors]
    public async Task<List<DirectoryStorageModel>> Get() =>
        await _directoryStorageService.GetAsync();


    //[DisableCors]
    //[HttpGet]
    //public async Task<List<DirectoryStorageModel>> Get() =>
    //   await _directoryStorageService.GetAsync();



    
    [HttpGet("GetAgregation")]
    public async Task<List<Models.Directory>> GetAgregation()
    {

        var directiveStorageInfo = await _directoryStorageService.GetAsync();

        //int[] num = { };
        //string[] dir = { };
        List<Models.Directory> direct = new List<Models.Directory>();

        //myList = new List<KeyValuePair<int, string>>();
        //list = new List<string>();
        int i = 0;
        int y = 0;
        foreach (var item in directiveStorageInfo)
        {
            int cnt = 0;
            for (int x = 0; x < item.path_dir.Length; x++)
                if (item.path_dir[x] == '/') cnt++;


            // Проверяем колличество корневых директорий
            if (cnt == 1)
            {
                i = i + 1;

                //direct.Add(new Models.Directory(i, "Корневая дирректория" + item.path_dir));
                //Проверяем колличество вложенных директорий
                //Console.WriteLine(item);
                //list.Add(item.path_dir);
                //myList.Add(item.path_dir);
                //return myList;
                //int cnt = 0;
                //for (int x = 0; x < item.path_dir.Length; x++)
                //    if (item.path_dir[x] == '/') cnt++;


            }
            else
            {
                y = y + 1;
                //Console.WriteLine(item);
                //direct.Add(new Models.Directory(y, "Вложенная дирректория + { item.path_dir }"));
                //myList.Add(new KeyValuePair<int, string>(y++, item.path_dir));
                //myList.Add(item.path_dir);
                //return myList;
            }

        }
        direct.Add(new Models.Directory(i, "Кол-во корневых директкорий"));
        direct.Add(new Models.Directory(y, "Кол-во вложенных директкорий"));


        //string message2 = "Кол-во вложенных директкорий - " + y;
        //direct.Add(new Models.Directory(i, "Корневые дирректории"));

        //direct.Add(new Models.Directory(y, "Вложенные дирректории"));

        return (direct); throw new NotImplementedException(); ;
    }
    //public async Task<List</*KeyValuePair<int,*/ string>>
    //    GetAgregation(List<string> myList/*, List<string> list*/)
    //{




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








    
    [HttpPut("changeDirectivePathInfo/{id:length(24)}")]
    public async Task<IActionResult> Update(string id, DirectoryStorageModel updatedDirectoryStorageModel)
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








    
    [HttpGet("checkDirective/{path_dir}")]
    public async Task<ActionResult<DirectoryStorageModel>> GetCheckProfileIntoSystem(string path_dir)
    {
        var values = new List<string> { };



        var directiveStorageInfo = await _directoryStorageService.CheckProfileIntoSystem(path_dir);


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