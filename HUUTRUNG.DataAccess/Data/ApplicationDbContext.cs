using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System;
using System.Globalization;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using HUUTRUNG.Models.Domain;

namespace HUUTRUNG.DataAccess.Data
{
    //Dung de ket noi CSDL voi Entity Framework core
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
    
		public DbSet<Character> Characters { get; set; }
		public DbSet<ComicCategory> ComicCategories { get; set; }
        public DbSet<Series> Series { get; set; }		
		public DbSet<Comic> Comics { get; set; }      
        public DbSet<Company> Companies { get; set; }
        public DbSet<ShoppingCart> ShoppingCarts { get; set; }	
		public DbSet<ApplicationUser> ApplicationUsers { get; set; }
		public DbSet<OrderHeader> OrderHeaders { get; set; }
		public DbSet<OrderDetail> OrderDetails { get; set; }
		public DbSet<News> News { get; set; }
        public DbSet<NewsCategory> NewsCategories{ get; set; }
		public DbSet<Genre> Genres { get; set; }
		public DbSet<MovieCategory> MovieCategories { get; set; }
        public DbSet<Movie> Movies { get; set; }
		public DbSet<Gallery> Galleries { get; set; }

		public DbSet<Page> Pages { get; set; }
		public DbSet<Rating> Ratings { get; set; }
		public DbSet<Comment> Comments { get; set; }

        public DbSet<Bookmark> Bookmarks { get; set; }



        #region du lieu du thua
        //public DbSet<Category>  Categories { get; set; }
        //public DbSet<Alignment> Alignment { get; set; }
        // public DbSet<District> Districts { get; set; }
        // public DbSet<Province> Provinces { get; set; }
        // public DbSet<Region> Regions { get; set; }
        #endregion
        protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
            #region hehe
            //			modelBuilder.Entity<Category>().HasData(
            //                new Category { Id = 1, Name = "Graphic Novel", DisplayOrder = 1 },
            //				new Category { Id = 2, Name = "Comic Book", DisplayOrder = 2 }			
            //			);
            //			modelBuilder.Entity<Character>().HasData(
            //			  new Character {
            //				  Id = 1,
            //				  Name = "Action Novel",
            //				  Thumbnail="hehe.jpg",
            //				  Power="super metal",
            //				  Alias="Wonder woman",
            //				  BaseOfOperations="Chiu",
            //				  FirstAppearance="abc",
            //                  Description = "Hal Jordan’s life was changed twice by" +
            //				  " crashing aircraft. The first time was when" +
            //				  " he witnessed the death of his father, " +
            //				  "pilot Martin Jordan. The second was when," +
            //				  " as an adult and trained pilot himself," +
            //				  " he was summoned to the crashed wreckage " +
            //				  "of a spaceship belonging to an alien named Abin Sur." +
            //				  " Abin explained that he was a member of " +
            //				  "the Green Lantern Corps, an organization " +
            //				  "of beings from across the cosmos, armed with power r" +
            //				  "ings fueled by the green energy of all willpower in " +
            //				  "the universe. Upon his death, Abin entrusted his" +
            //				  " ring and duties as the Green Lantern of Earth’s" +
            //				  " space sector to Hal Jordan.",
            //				  Occupation = "abc",
            //                  Created_at = DateTime.Now.Date,
            //                  Updated_at = DateTime.Now.Date,
            //              },
            //					new Character
            //					{
            //						Id = 2,
            //						Name = "Action Novel",
            //                        Thumbnail = "hehe.jpg",
            //						Power = "super metal",
            //						Alias = "Wonder woman",
            //						BaseOfOperations = "Chiu",
            //						FirstAppearance = "abc",
            //                        Description = "Hal Jordan’s life was changed twice by" +
            //				  " crashing aircraft. The first time was when" +
            //				  " he witnessed the death of his father, " +
            //				  "pilot Martin Jordan. The second was when," +
            //				  " as an adult and trained pilot himself," +
            //				  " he was summoned to the crashed wreckage " +
            //				  "of a spaceship belonging to an alien named Abin Sur." +
            //				  " Abin explained that he was a member of " +
            //				  "the Green Lantern Corps, an organization " +
            //				  "of beings from across the cosmos, armed with power r" +
            //				  "ings fueled by the green energy of all willpower in " +
            //				  "the universe. Upon his death, Abin entrusted his" +
            //				  " ring and duties as the Green Lantern of Earth’s" +
            //				  " space sector to Hal Jordan.",
            //						Occupation = "abc",
            //                        Created_at = DateTime.Now.Date,
            //                        Updated_at = DateTime.Now.Date,
            //                    },
            //						  new Character
            //						  {
            //							  Id = 3,
            //							  Name = "Action Novel",
            //                              Thumbnail = "hehe.jpg",
            //							  Power = "super metal",
            //							  Alias = "Wonder woman",
            //							  BaseOfOperations = "Chiu",
            //							  FirstAppearance = "abc",
            //                              Description = "Hal Jordan’s life was changed twice by" +
            //				  " crashing aircraft. The first time was when" +
            //				  " he witnessed the death of his father, " +
            //				  "pilot Martin Jordan. The second was when," +
            //				  " as an adult and trained pilot himself," +
            //				  " he was summoned to the crashed wreckage " +
            //				  "of a spaceship belonging to an alien named Abin Sur." +
            //				  " Abin explained that he was a member of " +
            //				  "the Green Lantern Corps, an organization " +
            //				  "of beings from across the cosmos, armed with power r" +
            //				  "ings fueled by the green energy of all willpower in " +
            //				  "the universe. Upon his death, Abin entrusted his" +
            //				  " ring and duties as the Green Lantern of Earth’s" +
            //				  " space sector to Hal Jordan.",
            //							  Occupation = "abc",
            //                              Created_at = DateTime.Now.Date,
            //                              Updated_at = DateTime.Now.Date,
            //                          },
            //								new Character
            //								{
            //									Id = 4,
            //									Name = "Action Novel",
            //                                    Thumbnail = "hehe.jpg",
            //									Power = "super metal",
            //									Alias = "Wonder woman",
            //									BaseOfOperations = "Chiu",
            //									FirstAppearance = "abc",
            //                                    Description = "Hal Jordan’s life was changed twice by" +
            //				  " crashing aircraft. The first time was when" +
            //				  " he witnessed the death of his father, " +
            //				  "pilot Martin Jordan. The second was when," +
            //				  " as an adult and trained pilot himself," +
            //				  " he was summoned to the crashed wreckage " +
            //				  "of a spaceship belonging to an alien named Abin Sur." +
            //				  " Abin explained that he was a member of " +
            //				  "the Green Lantern Corps, an organization " +
            //				  "of beings from across the cosmos, armed with power r" +
            //				  "ings fueled by the green energy of all willpower in " +
            //				  "the universe. Upon his death, Abin entrusted his" +
            //				  " ring and duties as the Green Lantern of Earth’s" +
            //				  " space sector to Hal Jordan.",
            //									Occupation = "abc",
            //                                    Created_at = DateTime.Now.Date,
            //                                    Updated_at = DateTime.Now.Date,
            //                                },
            //									  new Character
            //									  {
            //										  Id = 5,
            //										  Name = "Action Novel",
            //                                          Thumbnail = "hehe.jpg",
            //										  Power = "super metal",
            //										  Alias = "Wonder woman",
            //										  BaseOfOperations = "Chiu",
            //										  FirstAppearance = "abc",
            //                                          Description = "Hal Jordan’s life was changed twice by" +
            //				  " crashing aircraft. The first time was when" +
            //				  " he witnessed the death of his father, " +
            //				  "pilot Martin Jordan. The second was when," +
            //				  " as an adult and trained pilot himself," +
            //				  " he was summoned to the crashed wreckage " +
            //				  "of a spaceship belonging to an alien named Abin Sur." +
            //				  " Abin explained that he was a member of " +
            //				  "the Green Lantern Corps, an organization " +
            //				  "of beings from across the cosmos, armed with power r" +
            //				  "ings fueled by the green energy of all willpower in " +
            //				  "the universe. Upon his death, Abin entrusted his" +
            //				  " ring and duties as the Green Lantern of Earth’s" +
            //				  " space sector to Hal Jordan.",
            //										  Occupation = "abc",
            //                                          Created_at = DateTime.Now.Date,
            //                                          Updated_at = DateTime.Now.Date,
            //                                      }

            //		  );
            //			modelBuilder.Entity<TypeComic>().HasData(
            //				  new TypeComic { Id = 1, Name = "Graphic Novel", DisplayOrder = 1, Description = "heheh" },
            //				  new TypeComic { Id = 2, Name = "Comic Book", DisplayOrder = 2, Description = "heheh" }
            //			  );
            //			modelBuilder.Entity<Comic>().HasData(
            //			  new Comic {
            //				  Id = 1,
            //				  Name = "ABSOLUTE BATMAN NOIR EDITION #1 ",
            //				  Description = "Without the mansion…without the money…without the butler…what’s left is the Absolute Dark Knight! Noir Edition.",
            //				  Thumbnail = "hehe.jpg",
            //				  Writer = "Scott Snyder",
            //				  ArtBy = "Nick Dragotta",
            //				  Cover = "Frank Martin, Nick Dragotta",
            //				  Colorist = "Frank Martin",
            //				  Price = 3.5,
            //				  IsFree = true,
            //				  IsNew=false,                
            //                  OnSaleDate = DateTime.ParseExact("23-10-2024", "dd-MM-yyyy", CultureInfo.InvariantCulture),
            //                  PageCount = 36,
            //				  Rated = "Teen",
            //				  Created_at = DateTime.Now.Date,
            //                  Updated_at = DateTime.Now.Date,
            //				  TypeComicId=1,				
            //				  SeriesId=1
            //              },
            //				new Comic
            //				{
            //					Id = 2,
            //					Name = "ABSOLUTE BATMAN NOIR EDITION #1 ",
            //					Description = "Without the mansion…without the money…without the butler…what’s left is the Absolute Dark Knight! Noir Edition.",
            //					Thumbnail = "hehe.jpg",
            //					Writer = "Scott Snyder",
            //					ArtBy = "Nick Dragotta",
            //					Cover = "Frank Martin, Nick Dragotta",
            //					Colorist = "Frank Martin",
            //					Price = 3.5,
            //					IsFree = false,
            //					IsNew = false,
            //                    OnSaleDate = DateTime.ParseExact("23-10-2024", "dd-MM-yyyy", CultureInfo.InvariantCulture),
            //                    PageCount = 36,
            //					Rated = "Teen",
            //					Created_at = DateTime.Now.Date,
            //					Updated_at = DateTime.Now.Date,
            //					TypeComicId = 2,				
            //					SeriesId = 2
            //				},
            //				new Comic
            //				{
            //					Id = 3,
            //					Name = "ABSOLUTE BATMAN NOIR EDITION #1 ",
            //					Description = "Without the mansion…without the money…without the butler…what’s left is the Absolute Dark Knight! Noir Edition.",
            //					Thumbnail = "hehe.jpg",
            //					Writer = "Scott Snyder",
            //					ArtBy = "Nick Dragotta",
            //					Cover = "Frank Martin, Nick Dragotta",
            //					Colorist = "Frank Martin",
            //					Price = 3.5,
            //					IsFree = true,
            //					IsNew = false,
            //                    OnSaleDate = DateTime.ParseExact("23-10-2024", "dd-MM-yyyy", CultureInfo.InvariantCulture),
            //                    PageCount = 36,
            //					Rated = "Teen",
            //					Created_at = DateTime.Now.Date,
            //					Updated_at = DateTime.Now.Date,
            //					TypeComicId = 2,					
            //					SeriesId = 1
            //				}


            //		  );
            //            modelBuilder.Entity<Company>().HasData(
            //                  new Company {
            //					  Id = 1,
            //					  Name = "Nha sach Fahasa",
            //					  City = "Ho Chi Minh",
            //					  Province = "", 
            //					  StreetAddress="804 Le Trong Tan, Binh Hung Hoa",
            //					  Postal="07003",
            //                      PhoneNumber="034262119"
            //                  },
            //                  new Company
            //                  {
            //                      Id = 2,
            //                      Name = "Nha sach Binh Tan",
            //                      City = "Ho Chi Minh",
            //                      Province = "",
            //                      StreetAddress = "804 Le Trong Tan, Binh Hung Hoa",
            //                      Postal = "07001",
            //                      PhoneNumber = "034262119"
            //                  }

            //              );
            //			modelBuilder.Entity<TypeNews>().HasData(
            //				  new TypeNews { Id = 1, Name = "INTERVIEWS" },
            //				  new TypeNews { Id = 2, Name = "FEATURES" }
            //			  );
            //			modelBuilder.Entity<News>().HasData(
            //				new News
            //				{
            //					Id = 1,
            //					Tittle = "Breaking: New Tech Advances",
            //					PublishDate = new DateTime(2024, 12, 1),
            //					Author = "John Doe",
            //					Content = "The latest breakthrough in technology has been announced.",
            //					Thumbnail = "breaking-news.jpg",
            //					//TypeNewsId = null
            //				},
            //				new News
            //				{
            //					Id = 2,
            //					Tittle = "Official Update from the Government",
            //					PublishDate = new DateTime(2024, 12, 2),
            //					Author = "Jane Smith",
            //					Content = "The government has released an important update.",
            //					Thumbnail = "official-update.jpg",
            //					//Type = "OFFICIAL"
            //				},
            //				new News
            //				{
            //					Id = 3,
            //					Tittle = "Interview with Tech Pioneer",
            //					PublishDate = new DateTime(2024, 12, 3),
            //					Author = "Emily Johnson",
            //					Content = "An exclusive interview with the pioneer of modern AI.",
            //					Thumbnail = "interview-tech.jpg",
            //					TypeNewsId = 1
            //				},
            //				new News
            //				{
            //					Id = 4,
            //					Tittle = "Feature: How Technology Shapes Education",
            //					PublishDate = new DateTime(2024, 12, 4),
            //					Author = "Michael Brown",
            //					Content = "Exploring the impact of technology on the education system.",
            //					Thumbnail = "feature-education.jpg",
            //                    TypeNewsId = 1
            //                },
            //				new News
            //				{
            //					Id = 5,
            //					Tittle = "Exclusive: Inside the Space Mission",
            //					PublishDate = new DateTime(2024, 12, 5),
            //					Author = "Sarah Lee",
            //					Content = "A detailed look inside the latest space exploration mission.",
            //					Thumbnail = "space-mission.jpg",
            //                    TypeNewsId = 1
            //                },
            //				new News
            //				{
            //					Id = 6,
            //					Tittle = "New Policy Announced",
            //					PublishDate = new DateTime(2024, 12, 6),
            //					Author = "Chris Green",
            //					Content = "Details of the newly announced policy changes.",
            //					Thumbnail = "policy-update.jpg",
            //                    TypeNewsId = 1
            //                },
            //				new News
            //				{
            //					Id = 7,
            //					Tittle = "Interview: CEO of Tech Giant",
            //					PublishDate = new DateTime(2024, 12, 7),
            //					Author = "Anna White",
            //					Content = "The CEO shares insights into the future of the tech industry.",
            //					Thumbnail = "ceo-interview.jpg",
            //                    TypeNewsId = 2
            //                },
            //				new News
            //				{
            //					Id = 8,
            //					Tittle = "Breaking: Market Trends to Watch",
            //					PublishDate = new DateTime(2024, 12, 8),
            //					Author = "Robert Black",
            //					Content = "Key market trends that could shape the next year.",
            //					Thumbnail = "market-trends.jpg",
            //                    TypeNewsId = 2
            //                },
            //				new News
            //				{
            //					Id = 9,
            //					Tittle = "Feature: The Rise of Renewable Energy",
            //					PublishDate = new DateTime(2024, 12, 9),
            //					Author = "Laura Blue",
            //					Content = "An in-depth look at the growth of renewable energy.",
            //					Thumbnail = "renewable-energy.jpg",
            //                    TypeNewsId = 2
            //                },
            //				new News
            //				{
            //					Id = 10,
            //					Tittle = "Exclusive: Behind the Scenes of Innovation",
            //					PublishDate = new DateTime(2024, 12, 10),
            //					Author = "David Yellow",
            //					Content = "Discover what goes into creating groundbreaking innovations.",
            //					Thumbnail = "innovation.jpg",
            //                    TypeNewsId = 2
            //                }
            //);

            //modelBuilder.Entity<Alignment>().HasData(
            //	  new Alignment { Id = 1, Name = "BIRDS OF PREY " },
            //	  new Alignment { Id = 2, Name = "BATGIRL " }
            //  );

            #endregion
        }
    }       
   
}
