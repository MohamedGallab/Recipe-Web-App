using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RazorWebInterface.Pages.Recipes;

public class DeleteModel : PageModel
{
	[FromRoute(Name = "category")]
	public string Category { get; set; } = String.Empty;
	private readonly IHttpClientFactory _httpClientFactory;

	public DeleteModel(IHttpClientFactory httpClientFactory) =>
			_httpClientFactory = httpClientFactory;

	public void OnGet()
	{
	}

	public async Task<IActionResult> OnPostAsync()
	{
		var httpClient = _httpClientFactory.CreateClient("RecipeAPI");
		var response = await httpClient.DeleteAsync("categories?category=" + Category);
		response.EnsureSuccessStatusCode();
		return RedirectToPage("./Index");
	}
}
