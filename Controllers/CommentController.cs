using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieApi.Interfaces.Services;
using MovieApi.Model.DTOs;

namespace MovieApi.Controllers;


[ApiController]
[Route("/api/v1/[controller]")]
public class CommentController : ControllerBase
{
    private readonly ICommentService _commentService;

    public CommentController(ICommentService commentService)
    {
        _commentService = commentService;
    }

    [HttpGet]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(IEnumerable<CommentDto>))]
    public async Task<ActionResult<IEnumerable<CommentDto?>?>> GetCommentsAsync()
    {
        if (!ModelState.IsValid)
            return BadRequest();

        var comments = await _commentService.GetCommentsAsync();
        return Ok(comments);
    }
    
    [HttpGet("{id:guid}")]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(CommentDto))]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<ActionResult<CommentDto>> GetCommentAsync(Guid id)
    {
        if (!ModelState.IsValid)
            return BadRequest();
        var comment = await _commentService.GetCommentByIdAsync(id);
        if (comment is null)
            return NotFound();
        return Ok(comment);
    }

    [HttpPost]
    [ProducesResponseType((int)HttpStatusCode.Created, Type = typeof(CreateCommentDto))]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<ActionResult<CommentDto>> CreateCommentAsync(CreateCommentDto commentDto)
    {
        if (!ModelState.IsValid)
            return BadRequest();
        var newComment = await _commentService.CreateCommentAsync(commentDto);
        return Ok(newComment);
    }

    [HttpDelete("{id:guid}")]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(EntityState))]
    public async Task<ActionResult<EntityState>> DeleteCommentAsync(Guid id)
    {
        if(!ModelState.IsValid)
            return BadRequest();
        var removed = await _commentService.DeleteCommentAsync(id);
        if (removed is null)
            return NotFound();
        return Ok(removed);
    }
}