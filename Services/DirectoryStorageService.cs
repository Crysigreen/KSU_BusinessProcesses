using System;
using KSU_BProcesses.Controllers.MongoDB;
using KSU_BProcesses.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace KSU_BProcesses.Services;

public class DirectoryStorage{

        private readonly IMongoCollection<DirectoryStorageModel> _directoryStorageModel;

        public DirectoryStorage(
            IOptions<MongoDBSettings> directoryStoreDatabaseSettings){

            var mongoClient = new MongoClient(
                directoryStoreDatabaseSettings.Value.ConnectionURI);

            var mongoDatabase = mongoClient.GetDatabase(
                directoryStoreDatabaseSettings.Value.DatabaseName);

        _directoryStorageModel = mongoDatabase.GetCollection<DirectoryStorageModel>(
                directoryStoreDatabaseSettings.Value.SecondCollectionName);
        }
    /**
    * 
    * 
    */

    public async Task<List<DirectoryStorageModel>> GetAsync() =>
        await _directoryStorageModel.Find(_ => true).ToListAsync();




    //public async Task<List<DirectoryStorageModel>> GetAgregation() =>
    //   await _directoryStorageModel.Find(_ => true).ToListAsync();



    /**
     * 
     * 
     */

    public async Task<DirectoryStorageModel?> GetAsync(string id) =>
        await _directoryStorageModel.Find(x => x.Id == id).FirstOrDefaultAsync();

    /**
     * 
     * 
     */

    public async Task CreateAsync(DirectoryStorageModel newDirectory) =>
        await _directoryStorageModel.InsertOneAsync(newDirectory);

    /**
     * 
     * 
     */

    public async Task UpdateAsync(string id, DirectoryStorageModel updatedPath) =>
        await _directoryStorageModel.ReplaceOneAsync(x => x.Id == id, updatedPath);

    /**
     * 
     * 
     */

    public async Task RemoveAsync(string id) =>
        await _directoryStorageModel.DeleteOneAsync(x => x.Id == id);

    /**
     * 
     * 
     */

    public async Task<DirectoryStorageModel?> CheckProfileIntoSystem(string path_dir) =>
     await _directoryStorageModel.Find(x => x.path_dir == path_dir).FirstOrDefaultAsync();

}


