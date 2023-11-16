using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.OpenApi.Models;

using StraviaWApiMongo.Services;

namespace StraviaWApiMongo.Controllers;
[ApiController]
[Route("api/[controller]")]
public class ComentarioController : ControllerBase
{
    private readonly MongoDBService _mongoDBService;
    

    public ComentarioController(MongoDBService mongoDBService) {
        _mongoDBService = mongoDBService;
    }

    [HttpGet]
    public async Task<ActionResult<List<Comentario>>> Get()
    {
        var comentarios = await _mongoDBService.GetAsync();
        return Ok(comentarios);
    }

    [HttpGet("{id}", Name = "GetComentario")]
    public async Task<ActionResult<Comentario>> GetComentario(string id)
    {
        var comentario = await _mongoDBService.GetComentarioAsync(id);
        if (comentario == null)
        {
            return NotFound();
        }
        return Ok(comentario);
    }

    [HttpPost]
    public async Task<ActionResult<Comentario>> Create(Comentario comentario)
    {
        await _mongoDBService.CreateAsync(comentario);
        return CreatedAtRoute("GetComentario", new { id = comentario.ComentarioID.ToString() }, comentario);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(string id, Comentario comentarioIn)
    {
        var comentario = await _mongoDBService.GetComentarioAsync(id);

        if (comentario == null)
        {
            return NotFound();
        }

        await _mongoDBService.UpdateAsync(id, comentarioIn);

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        var comentario = await _mongoDBService.GetComentarioAsync(id);

        if (comentario == null)
        {
            return NotFound();
        }

        await _mongoDBService.DeleteAsync(id);

        return NoContent();
    }

}