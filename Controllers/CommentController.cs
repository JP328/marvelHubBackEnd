using FluentValidation;
using marvelHub.Model;
using marvelHub.Service;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;
using System.Xml.Linq;

namespace marvelHub.Controllers;

[ApiController, Route("~/comments")]
public class CommentController(ICommentService commentService, IValidator<Comment> commentValidator) : ControllerBase
{
    private readonly ICommentService _commentService = commentService;
    private readonly IValidator<Comment> _commentValidator = commentValidator;

    [HttpGet("post")]
    public async Task<ActionResult> GetByPost([FromBody] Post post)
    {
        return Ok(await _commentService.GetByPost(post));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult> GetById(long id)
    {
        var Response = await _commentService.GetById(id);

        if (Response is null)
            return NotFound();

        return Ok(Response);
    }

    [HttpGet("userid/{userId}")]
    public async Task<ActionResult> GetByUserId(string userId)
    {
        return Ok(await _commentService.GetByUserId(userId));
    }

    [HttpPost]
    [ActionName(nameof(GetById))]
    public async Task<ActionResult<Comment>> Create([FromBody] Comment comment)
    {
        var validateComment = await _commentValidator.ValidateAsync(comment);

        if (!validateComment.IsValid)
            return StatusCode(StatusCodes.Status400BadRequest, validateComment);
        
        var Response = await _commentService.Create(comment);

        if (Response is null)
            return BadRequest("Post not found!");

        return CreatedAtAction(nameof(GetById), new { id = comment.Id, comment });
    }

    [HttpPut]
    public async Task<ActionResult> Update([FromBody] Comment comment)
    {
        var validateComment = await _commentValidator.ValidateAsync(comment);
        
        if (comment.Id == 0)
            return BadRequest("Invalid comment Id!");

        if (!validateComment.IsValid)
            return StatusCode(StatusCodes.Status400BadRequest, validateComment);

        var Response = await _commentService.Update(comment);

        if(Response is null)
            return NotFound("Comment and/or post not found!");

        return Ok(Response);

    }


    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(long id)
    {
        var SearchComment = await _commentService.GetById(id);

        if (SearchComment is null)
            return NotFound("Comment not Found!");

        await _commentService.Delete(SearchComment);

        return NoContent();
    }
}