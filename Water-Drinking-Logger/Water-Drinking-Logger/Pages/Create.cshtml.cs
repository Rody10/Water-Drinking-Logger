using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Water_Drinking_Logger.Models;
using Microsoft.Data.Sqlite;

namespace Water_Drinking_Logger.Pages
{
    public class CreateModel : PageModel
    {
        private readonly IConfiguration _configuration;
        public CreateModel(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public DrinkingWaterModel DrinkingWater { get; set; }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            using (var connection = new SqliteConnection(_configuration.GetConnectionString("ConnectionString")))
            {
                connection.Open();
                var tableCmd = connection.CreateCommand();
                tableCmd.CommandText =
                $"INSERT INTO drinking_water(date, quantity) VALUES('{DrinkingWater.Date}', {DrinkingWater.Quantity})";

                tableCmd.ExecuteNonQuery();
            }
            return RedirectToPage("./Index");
        }
        
    }
}
