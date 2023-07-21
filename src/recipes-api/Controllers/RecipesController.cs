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
[Route("recipe")]
public class RecipesController : ControllerBase
{    
    public readonly IRecipeService _service;
    
    public RecipesController(IRecipeService service)
    {
        this._service = service;        
    }

    // 1 - Sua aplicação deve ter o endpoint GET /recipe
    //Read
    [HttpGet]
    public IActionResult Get()
    {
        var recipes = new Services.RecipeService();
        return Ok(recipes.GetRecipes());  
    }

    // 2 - Sua aplicação deve ter o endpoint GET /recipe/:name
    //Read
    [HttpGet("{name}", Name = "GetRecipe")]
    public IActionResult Get(string name)
    {                
        var recipes = new Services.RecipeService();
        return Ok(recipes.GetRecipe(name));
    }

    // 3 - Sua aplicação deve ter o endpoint POST /recipe
    [HttpPost]
    public IActionResult Create([FromBody]Recipe recipe)
    {
        var recipes = new Services.RecipeService();
        recipes.AddRecipe(recipe);
        return new ObjectResult(recipe) { StatusCode = StatusCodes.Status201Created }; 
    }

    // 4 - Sua aplicação deve ter o endpoint PUT /recipe
    [HttpPut("{name}")]
    public IActionResult Update(string name, [FromBody]Recipe recipe)
    {
        var recipes = new Services.RecipeService();
        var itExists = recipes.RecipeExists(name);
        if(itExists && name.ToLower() == recipe.Name.ToLower()) 
        {
            recipes.UpdateRecipe(recipe);
            return new ObjectResult(recipe) { StatusCode = StatusCodes.Status204NoContent };
        }
        return new ObjectResult(recipe) { StatusCode = StatusCodes.Status400BadRequest };    
    }

    // 5 - Sua aplicação deve ter o endpoint DEL /recipe
    [HttpDelete("{name}")]
    public IActionResult Delete(string name)
    {
        var recipes = new Services.RecipeService();
        var itExists = recipes.RecipeExists(name);
        if(itExists) 
        {
            recipes.DeleteRecipe(name);
            return new ObjectResult(name) { StatusCode = StatusCodes.Status204NoContent };
        }
        return new ObjectResult(name) { StatusCode = StatusCodes.Status404NotFound };
    }    
}
