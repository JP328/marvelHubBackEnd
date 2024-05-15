using FluentValidation;
using marvelHub.Model;
using marvelHub.Service;
using Microsoft.AspNetCore.Mvc;

namespace marvelHub.Controllers;

[ApiController, Route("~/posts")]
public class PostController(IPostService postService, IValidator<Post> postValidator) : ControllerBase
{
    private readonly IPostService _postService = postService;
    private readonly IValidator<Post> _postValidator = postValidator;

    [HttpGet] 
    public async Task<ActionResult> GetAll()
    {
        return Ok(await _postService.GetAll());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult> GetById(long id)
    {
        var Response = await _postService.GetById(id);

        if (Response is null)
            return NotFound();
        
        return Ok(Response);
    }

    [HttpGet("title/{title}")]
    public async Task<ActionResult> GetByTitle(string title)
    {
        return Ok(await _postService.GetByTitle(title));
    }

    [HttpPost]
    [ActionName(nameof(GetById))]
    public async Task<ActionResult> Create([FromBody] Post post)
    {
        var validatePost = await _postValidator.ValidateAsync(post);

        if (!validatePost.IsValid)
            return StatusCode(StatusCodes.Status400BadRequest, validatePost);

        var Response = await _postService.Create(post);

        if (Response is null)
            return BadRequest("Theme not found!");

        return CreatedAtAction(nameof(GetById), new {id = post.Id, post});
    }

    [HttpPut]
    public async Task<ActionResult> Update([FromBody] Post post)
    {
        if (post.Id == 0)
            return BadRequest("Invalid post Id!");

        var validatePost = await _postValidator.ValidateAsync(post);

        if (!validatePost.IsValid)
            return StatusCode(StatusCodes.Status400BadRequest, validatePost);

        var Response = await _postService.Update(post);

        if (Response is null)
            return NotFound("Post and/or theme not found!");

        return Ok(Response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(long id)
    {
        var SearchPost = await _postService.GetById(id);

        if (SearchPost is null) 
            return NotFound("Post not Found!");

        await _postService.Delete(SearchPost);

        return NoContent();
    }
}