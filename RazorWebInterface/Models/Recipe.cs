using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace RazorWebInterface.Models;

public class Recipe
{
	public Guid Id { get; set; } = Guid.NewGuid();
	[Required]
	[Display(Name = "Recipe Name")]
	public string Title { get; set; } = string.Empty;
	[Display(Name = "Recipe Ingredients")]
	public List<string> Ingredients { get; set; } = new();
	[Display(Name = "Recipe Instructions")]
	public List<string> Instructions { get; set; } = new();
	[Display(Name = "Recipe Categories")]
	public List<string> Categories { get; set; } = new();
}
