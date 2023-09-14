using WebApplication4.Models;

namespace WebApplication4.data
{
    public class AppInitilizer
    {
        public static void Seed(IApplicationBuilder applicationBuilder)
        {
            using(var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<AppDbContext>();
                context.Database.EnsureCreated();
                if(!context.Book.Any())
                {
                    context.Book.AddRange(new List<BookModel>()
                    {
                        new BookModel()
                        {
                            Name="Hello",
                            Publication="Test",
                            Genre=Genre.Horror,
                            PreferredAge=14,
                            Photos="https://swooshed.co/img/brands/adlv.svg",
                            Price=100,
                            Summary="Nice"
                        }
                    }) ;
                    context.SaveChanges();
                }
          
                
            }
        }
    }
}
