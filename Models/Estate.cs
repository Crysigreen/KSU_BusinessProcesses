using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Text.Json.Serialization;

namespace KSU_BProcesses.Models;

public class Estate
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }

    [BsonElement("type_estate")]
    [JsonPropertyName("type_estate")]
    public string type_estate { get; set; } = null!;


    [BsonElement("category_estate")]
    [JsonPropertyName("category_estate")]
    public string category_estate { get; set; } = null!;

    [BsonElement("estate_square")]
    [JsonPropertyName("estate_square")]
    public string estate_square { get; set; } = null!;

    [BsonElement("perchace_date")]
    [JsonPropertyName("perchace_date")]
    public string perchace_date { get; set; } = null!;



    [BsonElement("actualy_cost")]
    [JsonPropertyName("actualy_cost")]
    public string actualy_cost { get; set; } = null!;


    [BsonElement("assessed_cost")]
    [JsonPropertyName("assessed_cost")]
    public string assessed_cost { get; set; } = null!;



    [BsonElement("estate_location")]
    [JsonPropertyName("estate_location")]
    public string estate_location { get; set; } = null!;




    [BsonElement("tenant_info")]
    [JsonPropertyName("tenant_info")]
    public string tenant_info { get; set; } = null!;



    [BsonElement("status")]
    [JsonPropertyName("status")]
    public string status { get; set; } = null!;



    [BsonElement("rental_rate")]
    [JsonPropertyName("rental_rate")]
    public string rental_rate { get; set; } = null!;




    [BsonElement("image_path")]
    [JsonPropertyName("image_path")]
    public string[] image_path { get; set; } = null!;



    [BsonElement("comments")]
    [JsonPropertyName("comments")]
    public string comments { get; set; } = null!;


}

