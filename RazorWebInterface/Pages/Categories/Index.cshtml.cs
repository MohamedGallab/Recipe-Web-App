using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RazorWebInterface.Pages.Categories;

public class IndexModel : PageModel
{
	private readonly IHttpClientFactory _httpClientFactory;

	public IndexModel(IHttpClientFactory httpClientFactory) =>
		_httpClientFactory = httpClientFactory;

	public List<string> CategoryList { get; set; } = new();

	public async Task OnGetAsync()
	{
		var httpClient = _httpClientFactory.CreateClient("RecipeAPI");
		List<string>? categories = await httpClient.GetFromJsonAsync<List<string>>("categories");
		if (categories == null)
			return;
		CategoryList = categories;
	}
}
