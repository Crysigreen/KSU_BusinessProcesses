using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Text.Json.Serialization;

namespace KSU_BProcesses.Models;

public class DirectoryStorageModel
{

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("path_dir")]
        [JsonPropertyName("path_dir")]
        public string path_dir { get; set; } = null!;


    [BsonElement("creation_date")]
    [JsonPropertyName("creation_date")]
    public string creation_date { get; set; } = null!;

    [BsonElement("project_name")]
    [JsonPropertyName("project_name")]
    public string project_name { get; set; } = null!;

    [BsonElement("estate_name")]
    [JsonPropertyName("estate_name")]
    public string estate_name { get; set; } = null!;

    [BsonElement("status")]
    [JsonPropertyName("status")]
    public string status { get; set; } = null!;

    [BsonElement("clodes_dete")]
    [JsonPropertyName("clodes_dete")]
    public string clodes_dete { get; set; } = null!;


}
