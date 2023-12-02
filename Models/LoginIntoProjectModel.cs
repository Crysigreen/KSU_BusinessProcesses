using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Text.Json.Serialization;

namespace KSU_BProcesses.Models;

	public class LoginIntoProjectModel
	{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }

    [BsonElement("user_name")]
    [JsonPropertyName("user_name")]
    public string user_name { get; set; } = null!;

    [BsonElement("second_name")]
    [JsonPropertyName("second_name")]
    public string second_name { get; set; } = null!;

    [BsonElement("login")]
    [JsonPropertyName("login")]
    public string login { get; set; } = null!;

    [BsonElement("password")]
    [JsonPropertyName("password")]
    public string password { get; set; } = null!;


    [BsonElement("role")]
    [JsonPropertyName("role")]
    public string role { get; set; } = null!;

    [BsonElement("token")]
    [JsonPropertyName("token")]
    public string token { get; set; } = null!;
}

