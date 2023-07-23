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
[Route("user")]
public class UserController : ControllerBase
{    
    public readonly IUserService _service;
    
    public UserController(IUserService service)
    {
        this._service = service;        
    }

    // 6 - Sua aplicação deve ter o endpoint GET /user/:email
    [HttpGet("{email}", Name = "GetUser")]
    public IActionResult Get(string email)
    {                
        var users = new Services.UserService();
        var itExists = users.UserExists(email);
        if(itExists) {
            var user = users.GetUser(email);
            return new ObjectResult(user) { StatusCode = StatusCodes.Status200OK };
        }
        return new ObjectResult(email) { StatusCode = StatusCodes.Status404NotFound };

    }

    // 7 - Sua aplicação deve ter o endpoint POST /user
    [HttpPost]
    public IActionResult Create([FromBody]User user)
    {
        var users = new Services.UserService();
        users.AddUser(user);
        return new ObjectResult(user) { StatusCode = StatusCodes.Status201Created };
    }

    // "8 - Sua aplicação deve ter o endpoint PUT /user
    [HttpPut("{email}")]
    public IActionResult Update(string email, [FromBody]User user)
    {
        var users = new Services.UserService();
        var exists = users.UserExists(email);
        if (user.Email != email)
        {
            return new ObjectResult(user) { StatusCode = StatusCodes.Status400BadRequest };
        }
        else if (exists)
        {
            return new ObjectResult(user) { StatusCode = StatusCodes.Status200OK };
        }
        return new ObjectResult(user) { StatusCode = StatusCodes.Status404NotFound };
         
    }

    // 9 - Sua aplicação deve ter o endpoint DEL /user
    [HttpDelete("{email}")]
    public IActionResult Delete(string email)
    {
        var users = new Services.UserService();
        var exists = users.UserExists(email);
        if (exists) {
            users.DeleteUser(email);
            return new ObjectResult(email) { StatusCode = StatusCodes.Status204NoContent };
        }
        else {
            return new ObjectResult(email) { StatusCode = StatusCodes.Status404NotFound };
        }
    } 
}