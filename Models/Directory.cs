using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Text.Json.Serialization;
using System.Xml.Linq;

namespace KSU_BProcesses.Models;

public class Directory
{
    private List<Directory> direct;

    //[BsonId]
    //[BsonRepresentation(BsonType.ObjectId)]
    public int num { get; set; }

    //[BsonElement("path_dir")]
    //[JsonPropertyName("path_dir")]
    public string path_dir { get; set; }


    public Directory(int _num, string _path_dir)
    {
        num = _num;
        path_dir = _path_dir;
    }

    public Directory(List<Directory> direct)
    {
        this.direct = direct;
    }
}

