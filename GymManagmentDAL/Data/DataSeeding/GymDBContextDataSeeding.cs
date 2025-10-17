using GymManagmentDAL.Data.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace GymManagmentDAL.Data.DataSeeding
{
    public static class GymDBContextDataSeeding
    {
        public static bool SeedData(GymDBContext dBContext)
        {
            try
            {
                var HasPlans = dBContext.Plans.Any();
                var HAsCategories = dBContext.Categories.Any();

                if (HasPlans && HAsCategories) return false;

                if (!HAsCategories)
                {
                    var Categories = LoadDataFromJsonFile<Entities.Category>("categories.json");
                    if (Categories.Any())
                        dBContext.Categories.AddRange(Categories);
                }
                if (!HasPlans)
                {
                    var Plans = LoadDataFromJsonFile<Entities.Plan>("plans.json");
                    if (Plans.Any())
                        dBContext.Plans.AddRange(Plans);
                }
                return dBContext.SaveChanges() > 0;
            }
            catch (Exception ex) 
            {
                Console.WriteLine($"Error Occured During Data Seeding Process  {ex}");
                return false;
            }
            
        }

        private static List<T> LoadDataFromJsonFile<T>(string FileName)
        {
            var Filepath = Path.Combine(Directory.GetCurrentDirectory(),"wwwroot\\Files",FileName);
            if (!File.Exists(Filepath)) throw new FileNotFoundException();

            var jsonData = File.ReadAllText(Filepath);
            var Options = new JsonSerializerOptions()
            {
                PropertyNameCaseInsensitive = true,
            };

            return JsonSerializer.Deserialize<List<T>>(jsonData, Options) ?? new List<T>();
        }
    }
}
