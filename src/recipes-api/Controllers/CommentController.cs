using Microsoft.AspNetCore.Mvc;
using recipes_api.Services;
using recipes_api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace recipes_api.Controllers;

[ApiController]
[Route("comment")]
public class CommentController : ControllerBase
{  
    public readonly ICommentService _service;
    
    public CommentController(ICommentService service)
    {
        this._service = service;        
    }

    // 10 - Sua aplicação deve ter o endpoint POST /comment
    [HttpPost]
    public IActionResult Create([FromBody]Comment comment)
    {
        var comments = new Services.CommentService();
        comments.AddComment(comment);
        return new ObjectResult(comment) { StatusCode = StatusCodes.Status201Created }; 
    }

    // 11 - Sua aplicação deve ter o endpoint GET /comment/:recipeName
    [HttpGet("{name}", Name = "GetComment")]
    public IActionResult Get(string name)
    {                
        var comments = new Services.CommentService();
        var payload = comments.GetComments(name);
        return new ObjectResult(payload) { StatusCode = StatusCodes.Status200OK };       
    }
}