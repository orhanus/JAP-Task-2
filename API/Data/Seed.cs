using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using API.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class Seed
    {
        private static async Task<ICollection<Actor>> ExistingActorsInDb(ICollection<Actor> actors, DataContext context)
        {
            List<Actor> existingActorList = new();
            foreach(var actor in actors)
            {
                var existingActor = await context.Actors.FirstOrDefaultAsync(a => a.NameLastname == actor.NameLastname);
                if(existingActor != null)
                    existingActorList.Add(existingActor);
                else existingActorList.Add(actor);
            }
            return existingActorList;
        }
        public static async Task SeedShows(DataContext context)
        {
            if (await context.Shows.AnyAsync()) return;

            var showData = await System.IO.File.ReadAllTextAsync("Data/ShowSeedData.json");
            var shows = JsonSerializer.Deserialize<List<Show>>(showData);


            foreach (var show in shows)
            {
                show.Actors = await ExistingActorsInDb(show.Actors, context);
                context.Shows.Add(show);
            }

            await context.SaveChangesAsync();
        }

    }

}