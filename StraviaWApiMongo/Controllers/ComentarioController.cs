using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.OpenApi.Models;

[Route("api/[controller]")]
[ApiController]
public class ComentarioController : ControllerBase
{
    private readonly IMongoCollection<Comentario> _comentarioCollection;

    public ComentarioController(IMongoDatabase database)
    {
        _comentarioCollection = database.GetCollection<Comentario>("Comentario");
    }



    [HttpPost]
    public async Task<ActionResult<Comentario>> Create(Comentario comentario)
    {
        await _comentarioCollection.InsertOneAsync(comentario);
        return CreatedAtRoute("GetComentario", new { id = comentario.ComentarioID.ToString() }, comentario);
    }

    [HttpGet]
    public async Task<ActionResult<List<Comentario>>> Read()
    {
        return await _comentarioCollection.Find(comentario => true).ToListAsync();
    }

    [HttpGet("{id}", Name = "GetComentario")]
    public async Task<ActionResult<Comentario>> Read(string id)
    {
        var comentario = await _comentarioCollection.Find<Comentario>(comentario => comentario.ComentarioID == id).FirstOrDefaultAsync();

        if (comentario == null)
        {
            return NotFound();
        }

        return comentario;
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(string id, Comentario comentarioIn)
    {
        var comentario = await _comentarioCollection.Find<Comentario>(comentario => comentario.ComentarioID == id).FirstOrDefaultAsync();

        if (comentario == null)
        {
            return NotFound();
        }

        await _comentarioCollection.ReplaceOneAsync(comentario => comentario.ComentarioID == id, comentarioIn);

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        var comentario = await _comentarioCollection.Find<Comentario>(comentario => comentario.ComentarioID == id).FirstOrDefaultAsync();

        if (comentario == null)
        {
            return NotFound();
        }

        await _comentarioCollection.DeleteOneAsync(comentario => comentario.ComentarioID == id);

        return NoContent();
    }
}