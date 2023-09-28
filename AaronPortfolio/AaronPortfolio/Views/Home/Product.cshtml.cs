using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AaronPortfolio.Views.Home
{
    public class ProductModel : PageModel
    {
        public void OnGet()
        {
            var productId = Request.Form["data-product-id"];
        }
    }
}
