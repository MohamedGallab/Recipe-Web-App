using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorWebInterface.Models;

namespace RazorWebInterface.Pages.Recipes;

public class DeleteModel : PageModel
{
	[BindProperty(SupportsGet = true)]
	public Guid RecipeId { get; set; } = Guid.Empty;
	public Recipe Recipe { get; set; } = new();
	private readonly IHttpClientFactory _httpClientFactory;

	public DeleteModel(IHttpClientFactory httpClientFactory) =>
			_httpClientFactory = httpClientFactory;

	public async Task<IActionResult> OnGet()
	{
		var httpClient = _httpClientFactory.CreateClient("RecipeAPI");
		Recipe = await httpClient.GetFromJsonAsync<Recipe>($"recipes/{RecipeId}");
		if(Recipe == null)
			return NotFound();
		return Page();
		}

	public async Task<IActionResult> OnPostAsync()
	{
		var httpClient = _httpClientFactory.CreateClient("RecipeAPI");
		var response = await httpClient.DeleteAsync("recipes?id=" + RecipeId);
		response.EnsureSuccessStatusCode();
		return RedirectToPage("./Index");
	}
}
