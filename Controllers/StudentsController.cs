using Assignment3.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Assignment3.Controllers
{
    public class StudentsController : Controller
    {
        private readonly IHttpClientFactory _clientFactory;

        public StudentsController(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        // get students through api
        public async Task<IActionResult> Students()
        {
            var client = _clientFactory.CreateClient("api");
            var response = await client.GetAsync("api/StudentsApi");

            // if successful, deserisalise and return view with students
            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                var students = JsonConvert.DeserializeObject<List<Students>>(jsonString);
                return View(students);
            }

            //error message
            return View(new List<Students>());
        }
    }
}