using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RazorWebInterface.Pages.Categories;

public class DeleteModel : PageModel
{
	[BindProperty(SupportsGet = true)]
	public string? Category { get; set; }
	private readonly IHttpClientFactory _httpClientFactory;
	public DeleteModel(IHttpClientFactory httpClientFactory) =>
			_httpClientFactory = httpClientFactory;

	public IActionResult OnGetAsync(string? category)
	{
		if (category == null)
		{
			return NotFound();
		}

		return Page();
	}

	public async Task<IActionResult> OnPostAsync(string? category)
	{
		if (category == null)
		{
			return NotFound();
		}

		//Movie = await _context.Movie.FindAsync(category);

		//if (Movie != null)
		//{
		//    _context.Movie.Remove(Movie);
		//    await _context.SaveChangesAsync();
		//}

		return RedirectToPage("./Index");
	}
}
