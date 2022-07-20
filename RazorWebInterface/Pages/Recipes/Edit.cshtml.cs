using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorWebInterface.Models;
using System.ComponentModel.DataAnnotations;

namespace RazorWebInterface.Pages.Recipes;

public class EditModel : PageModel
{
	[BindProperty]
	[Required]
	public Recipe Recipe { get; set; } = new();
	public IEnumerable<string> Categories { get; set; } = Enumerable.Empty<string>();
	[BindProperty]
	public IEnumerable<string> ChosenCategories { get; set; } = Enumerable.Empty<string>();
	[BindProperty]
	public string Ingredients { get; set; } = string.Empty;
	[BindProperty]
	public string Instructions { get; set; } = string.Empty;
	private readonly IHttpClientFactory _httpClientFactory;

	public EditModel(IHttpClientFactory httpClientFactory) =>
			_httpClientFactory = httpClientFactory;
	public async Task<IActionResult> OnGetAsync(Guid recipeId)
	{
		var httpClient = _httpClientFactory.CreateClient("RecipeAPI");
		Categories = await httpClient.GetFromJsonAsync<IEnumerable<string>>("categories");
		Recipe = await httpClient.GetFromJsonAsync<Recipe>($"recipes/{recipeId}");
		if (Recipe == null)
			return NotFound();
		return Page();
	}

	public async Task<IActionResult> OnPostAsync(Guid recipeId)
	{
		Recipe.Id = recipeId;
		Recipe.Categories = (List<string>)ChosenCategories;
		Recipe.Ingredients = Ingredients.Split(Environment.NewLine).ToList();
		Recipe.Instructions = Instructions.Split(Environment.NewLine).ToList();
		if (!ModelState.IsValid)
			return Page();

		var httpClient = _httpClientFactory.CreateClient("RecipeAPI");
		var response = await httpClient.PutAsJsonAsync("recipes", Recipe);
		response.EnsureSuccessStatusCode();
		return RedirectToPage("./Index");
	}
}
