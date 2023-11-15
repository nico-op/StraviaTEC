using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
public class Comentario
{
    [BsonId]
    public string ComentarioID { get; set; }

    [BsonElement("Contenido")]
    public string Contenido { get; set; }

    [BsonElement("FechaPublicacion")]
    public DateTime FechaPublicacion { get; set; }

    [BsonElement("NombreUsuario")]
    public string NombreUsuario { get; set; }

    [BsonElement("ActividadID")]
    public int ActividadID { get; set; }
}
