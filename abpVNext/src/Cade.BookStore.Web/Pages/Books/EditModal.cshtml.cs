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
    public class EditModalModel : BookStorePageModel
    {
        //HiddenInput Attribute used to indicate whether a property or field value should be rendered as a hidden input element
        //Default true
        //"SupportsGet" used to be able to get Id value from query string parameter of the request.
        [HiddenInput]
        [BindProperty(SupportsGet = true)]
        public Guid Id { get; set; }

        [BindProperty]
        public CreateUpdateBookDto CUBookDto { get; set; }

        private readonly IBookAppService _bookAppService;

        public EditModalModel(IBookAppService bookAppService)
        {
            _bookAppService = bookAppService;
        }

        public async Task OnGetAsync()
        {
            var bookDto = await _bookAppService.GetAsync(Id);
            CUBookDto = ObjectMapper.Map<ReadBookDto, CreateUpdateBookDto>(bookDto);
        }

        public async Task<IActionResult> OnPostAsync()
        {
            await _bookAppService.UpdateAsync(Id, CUBookDto);
            return NoContent();
        }

        //public void OnGet()
        //{
        //}
    }
}
