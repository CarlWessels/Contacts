using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contacts.Common.DataObjects;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RestSharp;

namespace Contacts.Web.Pages
{
    public class PhoneBooksModel : PageModel
    {
        public List<PhoneBook> PhoneBooks;
        public void OnGet()
        {
            var client = new RestClient("http://localhost:5000");
            var request = new RestRequest("phonebooks", Method.GET);
            PhoneBooks = client.Execute<List<PhoneBook>>(request).Data;
        }

        public IActionResult AddPhoneBook()
        {
            return Redirect("/AddPhoneBook");
        }
    }
}
