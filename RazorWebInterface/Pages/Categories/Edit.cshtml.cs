using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace RazorWebInterface.Pages.Categories;

public class EditModel : PageModel
{
	[FromRoute(Name = "category")]
	[Display(Name = "Old Category Name")]
	public string OldCategory { get; set; } = String.Empty;
	[BindProperty]
	[Required]
	[Display(Name = "New Category Name")]
	public string NewCategory { get; set; } = String.Empty;
	private readonly IHttpClientFactory _httpClientFactory;

	public EditModel(IHttpClientFactory httpClientFactory) =>
			_httpClientFactory = httpClientFactory;

	public void OnGet()
	{
	}
	public async Task<IActionResult> OnPostAsync()
	{
		if (!ModelState.IsValid)
			return Page();

		var httpClient = _httpClientFactory.CreateClient("RecipeAPI");
		var response = await httpClient.PutAsync($"categories?oldcategory={OldCategory}&editedcategory={NewCategory}", null);
		response.EnsureSuccessStatusCode();
		return RedirectToPage("./Index");
	}
}
