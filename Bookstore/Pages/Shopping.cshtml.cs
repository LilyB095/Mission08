using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bookstore.Infrastructure;
using Bookstore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Bookstore.Pages
{
    public class ShoppingModel : PageModel
    {
        private IBookstoreRepository repo { get; set; }
        public ShoppingModel (IBookstoreRepository temp)
        {
            repo = temp;
        }

        public Basket basket { get; set; }

        public void OnGet()
        {
            basket = HttpContext.Session.GetJson<Basket>("basket") ?? new Basket();
        }

        public IActionResult OnPost(int BookId)
        {
            Book b = repo.Books.FirstOrDefault(x => x.BookId == BookId);

            basket = HttpContext.Session.GetJson<Basket>("basket") ?? new Basket();

            basket.AddItem(b, 1);

            HttpContext.Session.SetJson("basket", basket);

            return RedirectToPage(basket);

        }
    }
}
