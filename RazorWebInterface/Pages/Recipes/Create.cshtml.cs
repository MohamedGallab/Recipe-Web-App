using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorWebInterface.Models;
using System.ComponentModel.DataAnnotations;

namespace RazorWebInterface.Pages.Recipes;

public class CreateModel : PageModel
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

	public CreateModel(IHttpClientFactory httpClientFactory) =>
			_httpClientFactory = httpClientFactory;
	public async Task<IActionResult> OnGetAsync()
	{
		var httpClient = _httpClientFactory.CreateClient("RecipeAPI");
		Categories = await httpClient.GetFromJsonAsync<IEnumerable<string>>("categories");
		return Page();
	}

	public async Task<IActionResult> OnPostAsync()
	{
		Recipe.Id = Guid.NewGuid();
		Recipe.Categories = (List<string>)ChosenCategories;
		Recipe.Ingredients = Ingredients.Split(Environment.NewLine).ToList();
		Recipe.Instructions = Instructions.Split(Environment.NewLine).ToList();
		if (!ModelState.IsValid)
			return Page();

		var httpClient = _httpClientFactory.CreateClient("RecipeAPI");
		var response = await httpClient.PostAsJsonAsync("recipes", Recipe);
		response.EnsureSuccessStatusCode();
		return RedirectToPage("./Index");
	}
}
