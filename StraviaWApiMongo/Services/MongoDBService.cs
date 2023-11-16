using System.Threading.Tasks;
using System;
using MongoDB.Bson.Serialization.Attributes;
using Microsoft.OpenApi.Models;
using MongoDB.Driver;
using MongoDB.Bson;


namespace StraviaWApiMongo.Services{
    public class MongoDBService
    {
        private readonly IMongoCollection<Comentario> _comentarioCollection;

        public MongoDBService(IMongoDBSettings mongoDBSettings)
        {
            MongoClient client = new MongoClient(mongoDBSettings.MongoDBConnection);
            IMongoDatabase database = client.GetDatabase(mongoDBSettings.DatabaseName);
            _comentarioCollection = database.GetCollection<Comentario>(mongoDBSettings.CollectionName);
        }

        public async Task<List<Comentario>> GetAsync()
        {
            return await _comentarioCollection.Find(comentario => true).ToListAsync();
        }

        public async Task CreateAsync(Comentario comentario)
        {
            comentario.ComentarioID = ObjectId.GenerateNewId().ToString();
            await _comentarioCollection.InsertOneAsync(comentario);
        }

        public async Task<Comentario> GetComentarioAsync(string id)
        {
            var filter = Builders<Comentario>.Filter.Eq("_id", ObjectId.Parse(id));
            return await _comentarioCollection.Find(filter).FirstOrDefaultAsync();
        }


        public async Task UpdateAsync(string id, Comentario comentarioIn)
        {
            var filter = Builders<Comentario>.Filter.Eq("_id", ObjectId.Parse(id));
            await _comentarioCollection.ReplaceOneAsync(filter, comentarioIn);
        }

        public async Task DeleteAsync(string id)
        {
            var filter = Builders<Comentario>.Filter.Eq("_id", ObjectId.Parse(id));
            await _comentarioCollection.DeleteOneAsync(filter);
        }
    }
}