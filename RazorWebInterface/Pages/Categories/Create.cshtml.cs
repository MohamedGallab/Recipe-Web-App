using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RazorWebInterface.Pages.Categories
{
    public class CreateModel : PageModel
    {
		[BindProperty]        
		public string Category { get; set; }
		private readonly IHttpClientFactory _httpClientFactory;

		public CreateModel(IHttpClientFactory httpClientFactory) =>
				_httpClientFactory = httpClientFactory;
		public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
		{
			var httpClient = _httpClientFactory.CreateClient("RecipeAPI");
			var response = await httpClient.PostAsJsonAsync("categories?category=" + Category, Category);
			response.EnsureSuccessStatusCode();
			return RedirectToPage("./Index");
		}
    }
}
