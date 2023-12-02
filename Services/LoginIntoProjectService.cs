using System;
using KSU_BProcesses.Controllers.MongoDB;
using KSU_BProcesses.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace KSU_BProcesses.Services;

public class LoginIntoProject{

    private readonly IMongoCollection<LoginIntoProjectModel> _loginIntoProjectModel;

    public LoginIntoProject(
        IOptions<MongoDBSettings> loginStoreDatabaseSettings)
    {
        var mongoClient = new MongoClient(
            loginStoreDatabaseSettings.Value.ConnectionURI);

        var mongoDatabase = mongoClient.GetDatabase(
            loginStoreDatabaseSettings.Value.DatabaseName);

        _loginIntoProjectModel = mongoDatabase.GetCollection<LoginIntoProjectModel>(
            loginStoreDatabaseSettings.Value.CollectionName);
    }


    /**
     * Возвращает массив всех объектов (LoginIntoProjectModel)
     * [shchema: login_into_project]
     */

    public async Task<List<LoginIntoProjectModel>> GetAsync() =>
        await _loginIntoProjectModel.Find(_ => true).ToListAsync();

    /**
     * Возвращает объект по id (LoginIntoProjectModel) 
     * [shchema: login_into_project]
     */

    public async Task<LoginIntoProjectModel?> GetAsync(string id) =>
        await _loginIntoProjectModel.Find(x => x.Id == id).FirstOrDefaultAsync();

    /**
     * Создает объект (LoginIntoProjectModel)
     * [shchema: login_into_project]
     */

    public async Task CreateAsync(LoginIntoProjectModel newLogProfile) =>
        await _loginIntoProjectModel.InsertOneAsync(newLogProfile);

    /**
     * Проверяет наличие объекта по id, обновляет данные (LoginIntoProjectModel)
     * [shchema: login_into_project]
     */

    public async Task UpdateAsync(string id, LoginIntoProjectModel updatedLogin) =>
        await _loginIntoProjectModel.ReplaceOneAsync(x => x.Id == id, updatedLogin);

    /**
     * Проверяет наличие объекта по id, обновляет данные (LoginIntoProjectModel)
     * [shchema: login_into_project]
     */

    public async Task RemoveAsync(string id) =>
        await _loginIntoProjectModel.DeleteOneAsync(x => x.Id == id);

    /**
     * Проверяет сущетсвует ли пользак с таким паролем и логином (LoginIntoProjectModel)
     * [shchema: login_into_project]
     */

    public async Task<LoginIntoProjectModel?> CheckProfileIntoSystem(string login, string password) =>
     await _loginIntoProjectModel.Find(x => x.login == login && x.password == password).FirstOrDefaultAsync();

}

