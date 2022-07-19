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
	private readonly IHttpClientFactory _httpClientFactory;

	public CreateModel(IHttpClientFactory httpClientFactory) =>
			_httpClientFactory = httpClientFactory;
	public void OnGet()
	{
	}

	public async Task<IActionResult> OnPostAsync()
	{
		if (!ModelState.IsValid)
			return Page();

		var httpClient = _httpClientFactory.CreateClient("RecipeAPI");
		var response = await httpClient.PostAsJsonAsync("recipes", Recipe);
		response.EnsureSuccessStatusCode();
		return RedirectToPage("./Index");
	}
}
