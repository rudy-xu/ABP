using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cade.BookStore.Books;
using Cade.BookStore.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Cade.BookStore.Web.Pages.Books
{
    public class CreateModalModel : BookStorePageModel
    {
        //Bind post request data to this property.
        [BindProperty]
        public CreateUpdateBookDto CUBookDto { get; set; }

        public readonly IBookAppService _bookAppService;

        public CreateModalModel(IBookAppService bookAppService)
        {
            _bookAppService = bookAppService;
        }

        public void OnGet()
        {
            CUBookDto = new CreateUpdateBookDto();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            await _bookAppService.CreateAsync(CUBookDto);
            return NoContent();
        }
    }
}
