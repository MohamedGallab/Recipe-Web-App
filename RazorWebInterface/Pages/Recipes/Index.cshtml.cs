using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Net.Http.Headers;
using RazorWebInterface.Models;

namespace RazorWebInterface.Pages.Recipes;

public class IndexModel : PageModel
{
	private readonly IHttpClientFactory _httpClientFactory;

	public IndexModel(IHttpClientFactory httpClientFactory) =>
		_httpClientFactory = httpClientFactory;

	public List<Recipe> RecipeList { get; set; } = new();

	public async Task OnGetAsync()
	{
		var httpClient = _httpClientFactory.CreateClient("RecipeAPI");
		List<Recipe>? recipes = await httpClient.GetFromJsonAsync<List<Recipe>>("recipes");
		if(recipes == null)
			return;
		RecipeList = recipes;
	}
}
