using System;
using KSU_BProcesses.Controllers.MongoDB;
using KSU_BProcesses.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace KSU_BProcesses.Services;

public class EstateService
{
    private readonly IMongoCollection<Estate> _estateModel;

    public EstateService(
        IOptions<MongoDBSettings> directoryStoreDatabaseSettings)
    {

        var mongoClient = new MongoClient(
            directoryStoreDatabaseSettings.Value.ConnectionURI);

        var mongoDatabase = mongoClient.GetDatabase(
            directoryStoreDatabaseSettings.Value.DatabaseName);

        _estateModel = mongoDatabase.GetCollection<Estate>(
                directoryStoreDatabaseSettings.Value.EstateCollectionName);
    }
    /**
    * 
    * 
    */

    public async Task<List<Estate>> GetAsync() =>
        await _estateModel.Find(_ => true).ToListAsync();




    //public async Task<List<DirectoryStorageModel>> GetAgregation() =>
    //   await _directoryStorageModel.Find(_ => true).ToListAsync();



    /**
     * 
     * 
     */

    public async Task<Estate?> GetAsync(string id) =>
        await _estateModel.Find(x => x.Id == id).FirstOrDefaultAsync();

    /**
     * 
     * 
     */

    public async Task CreateAsync(Estate newDirectory) =>
        await _estateModel.InsertOneAsync(newDirectory);

    /**
     * 
     * 
     */

    public async Task UpdateAsync(string id, Estate updatedPath) =>
        await _estateModel.ReplaceOneAsync(x => x.Id == id, updatedPath);

    /**
     * 
     * 
     */

    public async Task RemoveAsync(string id) =>
        await _estateModel.DeleteOneAsync(x => x.Id == id);

    /**
     * 
     * 
     */

    public async Task<Estate?> GetInfoEstateByIdIntoSystem(string status) =>
     await _estateModel.Find(x => x.Id == status).FirstOrDefaultAsync();


    public async Task<Estate?> GetEstateByAddress(string address) =>
    await _estateModel.Find(x => x.estate_location == address).FirstOrDefaultAsync();

    
}

