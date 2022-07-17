using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace RazorWebInterface.Pages.Categories;

public class CreateModel : PageModel
{
	[BindProperty]
	[Required]
	[Display(Name = "Category Name")]
	public string Category { get; set; } = String.Empty;
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
		var response = await httpClient.PostAsJsonAsync("categories?category=" + Category, Category);
		response.EnsureSuccessStatusCode();
		return RedirectToPage("./Index");
	}
}
