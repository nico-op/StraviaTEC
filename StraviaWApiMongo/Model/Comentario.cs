using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Microsoft.OpenApi.Models;
public class Comentario
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
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
