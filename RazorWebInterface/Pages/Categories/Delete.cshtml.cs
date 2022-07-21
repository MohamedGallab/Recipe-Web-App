using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RazorWebInterface.Pages.Categories;

public class DeleteModel : PageModel
{
	[TempData]
	public string? ActionResult { get; set; }
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
		try
		{
			var httpClient = _httpClientFactory.CreateClient("RecipeAPI");
			var response = await httpClient.DeleteAsync("categories?category=" + Category);
			response.EnsureSuccessStatusCode();
			ActionResult = "Created successfully";
		}
		catch (Exception)
		{
			ActionResult = "Something went wrong, Try again later";
		}
		return RedirectToPage("./Index");
	}
}
