using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using API.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class Seed
    {
        public static async Task SeedShows(DataContext context)
        {
            if(await context.Shows.AnyAsync()) return;

            var showData = await System.IO.File.ReadAllTextAsync("Data/ShowSeedData.json");
            var shows = JsonSerializer.Deserialize<List<Show>>(showData);

            foreach(var show in shows)
            {
                context.Shows.Add(show);
            }

            await context.SaveChangesAsync();
        }
        
    }
}