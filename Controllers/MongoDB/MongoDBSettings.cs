using System;
namespace KSU_BProcesses.Controllers.MongoDB
{
	public class MongoDBSettings{
        public string ConnectionURI { get; set; } = null!;
        public string DatabaseName { get; set; } = null!;
        public string CollectionName { get; set; } = null!;
        public string SecondCollectionName { get; set; } = null!;
        public string EstateCollectionName { get; set; } = null!;

    }

}

