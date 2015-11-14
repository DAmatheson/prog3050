using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.CompilerServices;
using EfEnumToLookup.LookupGenerator;
using Veil.DataModels;
using Veil.DataModels.Models;
using Veil.DataModels.Models.Identity;

[assembly: InternalsVisibleTo("Veil.DataAccess.Tests")]
namespace Veil.DataAccess.Migrations
{
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<VeilDataContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "Veil.DataAccess.VeilDataContext";
        }

        protected override void Seed(VeilDataContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.

            context.ESRBRatings.AddOrUpdate(
                er => er.RatingId,
                new ESRBRating
                {
                    RatingId = "EC",
                    Description = "Early Childhood",
                    ImageURL = "https://esrbstorage.blob.core.windows.net/esrbcontent/images/ratingsymbol_ec.png"
                },
                new ESRBRating
                {
                    RatingId = "E",
                    Description = "Everyone",
                    ImageURL = "https://esrbstorage.blob.core.windows.net/esrbcontent/images/ratingsymbol_e.png"
                },
                new ESRBRating
                {
                    RatingId = "E10+",
                    Description = "Everyone 10+",
                    ImageURL = "https://esrbstorage.blob.core.windows.net/esrbcontent/images/ratingsymbol_e10.png"
                },
                new ESRBRating
                {
                    RatingId = "T",
                    Description = "Teen",
                    ImageURL = "https://esrbstorage.blob.core.windows.net/esrbcontent/images/ratingsymbol_t.png"
                },
                new ESRBRating
                {
                    RatingId = "M",
                    Description = "Mature",
                    ImageURL = "https://esrbstorage.blob.core.windows.net/esrbcontent/images/ratingsymbol_m.png"
                },
                new ESRBRating
                {
                    RatingId = "AO",
                    Description = "Adults Only",
                    ImageURL = "https://esrbstorage.blob.core.windows.net/esrbcontent/images/ratingsymbol_ao.png"
                },
                new ESRBRating
                {
                    RatingId = "RP",
                    Description = "Rating Pending",
                    ImageURL = "https://esrbstorage.blob.core.windows.net/esrbcontent/images/ratingsymbol_rp.png"
                }
            );

            int ecdId = 0;

            context.ESRBContentDescriptors.AddOrUpdate(
                ecd => ecd.Id,
                new ESRBContentDescriptor { Id = ++ecdId, DescriptorName = "Alcohol Reference" },
                new ESRBContentDescriptor { Id = ++ecdId, DescriptorName = "Animated Blood" },
                new ESRBContentDescriptor { Id = ++ecdId, DescriptorName = "Blood" },
                new ESRBContentDescriptor { Id = ++ecdId, DescriptorName = "Blood and Gore" },
                new ESRBContentDescriptor { Id = ++ecdId, DescriptorName = "Cartoon Violence" },
                new ESRBContentDescriptor { Id = ++ecdId, DescriptorName = "Comic Mischief" },
                new ESRBContentDescriptor { Id = ++ecdId, DescriptorName = "Crude Humor" },
                new ESRBContentDescriptor { Id = ++ecdId, DescriptorName = "Drug Reference" },
                new ESRBContentDescriptor { Id = ++ecdId, DescriptorName = "Fantasy Violence" },
                new ESRBContentDescriptor { Id = ++ecdId, DescriptorName = "Intense Violence" },
                new ESRBContentDescriptor { Id = ++ecdId, DescriptorName = "Language" },
                new ESRBContentDescriptor { Id = ++ecdId, DescriptorName = "Lyrics" },
                new ESRBContentDescriptor { Id = ++ecdId, DescriptorName = "Mature Humor" },
                new ESRBContentDescriptor { Id = ++ecdId, DescriptorName = "Nudity" },
                new ESRBContentDescriptor { Id = ++ecdId, DescriptorName = "Partial Nudity" },
                new ESRBContentDescriptor { Id = ++ecdId, DescriptorName = "Real Gambling" },
                new ESRBContentDescriptor { Id = ++ecdId, DescriptorName = "Sexual Content" },
                new ESRBContentDescriptor { Id = ++ecdId, DescriptorName = "Sexual Themes" },
                new ESRBContentDescriptor { Id = ++ecdId, DescriptorName = "Sexual Violence" },
                new ESRBContentDescriptor { Id = ++ecdId, DescriptorName = "Simulated Gambling" },
                new ESRBContentDescriptor { Id = ++ecdId, DescriptorName = "Strong Language" },
                new ESRBContentDescriptor { Id = ++ecdId, DescriptorName = "Strong Lyrics" },
                new ESRBContentDescriptor { Id = ++ecdId, DescriptorName = "Strong Sexual Content" },
                new ESRBContentDescriptor { Id = ++ecdId, DescriptorName = "Suggestive Themes" },
                new ESRBContentDescriptor { Id = ++ecdId, DescriptorName = "Tobacco Reference" },
                new ESRBContentDescriptor { Id = ++ecdId, DescriptorName = "Use of Alcohol" },
                new ESRBContentDescriptor { Id = ++ecdId, DescriptorName = "Use of Drugs" },
                new ESRBContentDescriptor { Id = ++ecdId, DescriptorName = "Use of Tobacco" },
                new ESRBContentDescriptor { Id = ++ecdId, DescriptorName = "Violence" },
                new ESRBContentDescriptor { Id = ++ecdId, DescriptorName = "Violent References" }
            );

            context.Countries.AddOrUpdate(
                c => c.CountryCode,
                new Country
                {
                    CountryCode = "CA",
                    CountryName = "Canada",
                    FederalTaxAcronym = "GST",
                    FederalTaxRate = 0.05m
                },
                new Country
                {
                    CountryCode = "US",
                    CountryName = "United States of America",
                    FederalTaxRate = 0
                }
            );

            context.Provinces.AddOrUpdate(
                p => new { p.ProvinceCode, p.CountryCode },
                // Canadian Provinces and Territories
                new Province
                {
                    CountryCode = "CA",
                    ProvinceCode = "AB",
                    Name = "Alberta",
                    ProvincialTaxRate = 0
                },
                new Province
                {
                    CountryCode = "CA",
                    ProvinceCode = "BC",
                    Name = "British Columbia",
                    ProvincialTaxAcronym = "PST",
                    ProvincialTaxRate = 0.07m
                },
                new Province
                {
                    CountryCode = "CA",
                    ProvinceCode = "MB",
                    Name = "Manitoba",
                    ProvincialTaxAcronym = "PST",
                    ProvincialTaxRate = 0.08m
                },
                new Province
                {
                    CountryCode = "CA",
                    ProvinceCode = "NB",
                    Name = "New Brunswick",
                    ProvincialTaxAcronym = "HST",
                    ProvincialTaxRate = 0.08m
                },
                new Province
                {
                    CountryCode = "CA",
                    ProvinceCode = "NL",
                    Name = "Newfoundland and Labrador",
                    ProvincialTaxAcronym = "HST",
                    ProvincialTaxRate = 0.08m
                },
                new Province
                {
                    CountryCode = "CA",
                    ProvinceCode = "NT",
                    Name = "Northwest Territories",
                    ProvincialTaxRate = 0
                },
                new Province
                {
                    CountryCode = "CA",
                    ProvinceCode = "NS",
                    Name = "Nova Scotia",
                    ProvincialTaxAcronym = "HST",
                    ProvincialTaxRate = 0.10m
                },
                new Province
                {
                    CountryCode = "CA",
                    ProvinceCode = "NU",
                    Name = "Nunavut",
                    ProvincialTaxRate = 0
                },
                new Province
                {
                    CountryCode = "CA",
                    ProvinceCode = "ON",
                    Name = "Ontario",
                    ProvincialTaxAcronym = "HST",
                    ProvincialTaxRate = 0.08m
                },
                new Province
                {
                    CountryCode = "CA",
                    ProvinceCode = "PE",
                    Name = "Prince Edward Island",
                    ProvincialTaxAcronym = "HST",
                    ProvincialTaxRate = 0.09m
                },
                new Province
                {
                    CountryCode = "CA",
                    ProvinceCode = "QC",
                    Name = "Quebec",
                    ProvincialTaxAcronym = "PST",
                    ProvincialTaxRate = 0.09975m
                },
                new Province
                {
                    CountryCode = "CA",
                    ProvinceCode = "SK",
                    Name = "Saskatchewan",
                    ProvincialTaxAcronym = "PST",
                    ProvincialTaxRate = 0.05m
                },
                new Province
                {
                    CountryCode = "CA",
                    ProvinceCode = "YT",
                    Name = "Yukon",
                    ProvincialTaxRate = 0
                },

                // US States
                new Province
                {
                    CountryCode = "US",
                    ProvinceCode = "NY",
                    Name = "New York",
                    ProvincialTaxRate = 0
                }
            );

            context.LocationTypes.AddOrUpdate(
                lt => lt.LocationTypeName, 
                new LocationType
                {
                    LocationTypeName = "Office"
                },
                new LocationType
                {
                    LocationTypeName = "Store"
                }
            );

            context.Locations.AddOrUpdate(
                l => l.LocationNumber,
                new Location
                {
                    City = "Waterloo",
                    CountryCode = "CA",
                    LocationNumber = 1,
                    LocationTypeName = "Office",
                    PhoneNumber = "555-555-1199",
                    SiteName = "Veil HQ",
                    PostalCode = "N2L 6R2",
                    ProvinceCode = "ON",
                    StreetAddress = "123 Fake Way"
                }
            );

            int deptId = 0;

            context.Departments.AddOrUpdate(
                d => d.Id,
                new Department
                {
                    Id = ++deptId,
                    Name = "Retail Operations"
                },
                new Department
                {
                    Id = ++deptId,
                    Name = "Purchasing"
                },
                new Department
                {
                    Id = ++deptId,
                    Name = "Online Operations"
                }
            );

            context.Platforms.AddOrUpdate(
                p => p.PlatformCode,
                new Platform
                {
                    PlatformCode = "PC",
                    PlatformName = "PC"
                }, 
                new Platform
                {
                    PlatformCode = "PS4",
                    PlatformName = "PlayStation 4"
                }, 
                new Platform
                {
                    PlatformCode = "XONE",
                    PlatformName = "Xbox One"
                },
                new Platform
                {
                    PlatformCode = "WIIU",
                    PlatformName = "Wii U"
                },
                new Platform
                {
                    PlatformCode = "PS3",
                    PlatformName = "PlayStation 3"
                }, 
                new Platform
                {
                    PlatformCode = "X360",
                    PlatformName = "Xbox 360"
                }
            );

            // The GUIDs for these were taken from SQL Server as it generates sequential ones
            context.Companies.AddOrUpdate(
                c => c.Name,
                new Company { Name = "Activision Blizzard" },
                new Company { Name = "Electronic Arts" },
                new Company { Name = "Ubisoft" },
                new Company { Name = "Take-Two" },
                new Company { Name = "2K Games" },
                new Company { Name = "Blizzard Entertainment" },
                new Company { Name = "EA DICE" },
                new Company { Name = "Rockstar Games" },
                new Company { Name = "Nintendo" },
                new Company { Name = "Sony Computer Entertainment" },
                new Company { Name = "Microsoft Studios" },
                new Company { Name = "Bungie" },
                new Company { Name = "Treyarch" }
            );

            context.Tags.AddOrUpdate(
                t => t.Name,
                new Tag { Name = "First Person" },
                new Tag { Name = "Third Person" },
                new Tag { Name = "Shooter" },
                new Tag { Name = "Simulation" },
                new Tag { Name = "RTS" },
                new Tag { Name = "Racing" },
                new Tag { Name = "RPG" },
                new Tag { Name = "MMO" },
                new Tag { Name = "Action" },
                new Tag { Name = "Adventure" },
                new Tag { Name = "Side Scroller" },
                new Tag { Name = "2D" },
                new Tag { Name = "3D" },
                new Tag { Name = "Turn-Based" },
                new Tag { Name = "Roguelike" }
            );

            context.Roles.AddOrUpdate(
                r => r.Id,
                new GuidIdentityRole
                {
                    Id = Guid.ParseExact("455b072e-de7d-e511-80df-001cd8b71da6", "D"),
                    Name = VeilRoles.ADMIN_ROLE
                },
                new GuidIdentityRole
                {
                    Id = Guid.ParseExact("465b072e-de7d-e511-80df-001cd8b71da6", "D"),
                    Name = VeilRoles.EMPLOYEE_ROLE
                },
                new GuidIdentityRole
                {
                    Id = Guid.ParseExact("475b072e-de7d-e511-80df-001cd8b71da6", "D"),
                    Name = VeilRoles.MEMBER_ROLE
                }
            );

    #region Debug Only Seed Values
            /* TODO: Remove this when we are done testing */
            Game halo5 = new Game
            {
                Name = "Halo 5: Guardians",
                ESRBRatingId = "T",
                ShortDescription = "An unstoppable force threatens the galaxy, and the Master Chief is missing.",
                LongDescription = "A mysterious and unstoppable force threatens the galaxy, the Master Chief is missing and his loyalty questioned. Experience the most dramatic Halo story to date in a 4-player cooperative epic that spans three worlds. Challenge friends and rivals in new multiplayer modes: Warzone, massive 24-player battles, and Arena, pure 4-vs-4 competitive combat.",
                MinimumPlayerCount = 1,
                MaximumPlayerCount = 24,
                PrimaryImageURL = "http://edge.alluremedia.com.au/m/k/2015/10/halo-1980x1080.jpg"
            };

            Game vermintide = new Game
            {
                Name = "Warhammer: End Times - Vermintide",
                ESRBRatingId = "M",
                LongDescription = "Vermintide takes place in and around Ubersreik, a city overrun by Skaven. You will assume the role of one of five heroes, each featuring different play-styles, abilities, gear and personality. Working cooperatively, you must use their individual attributes to survive an apocalyptic invasion from the hordes of relentless rat-men, known as the Skaven. Battles will take place across a range of environments stretching from the top of the Magnus Tower to the bowels of the Under Empire.",
                ShortDescription = "Vermintide is an epic co-operative action combat adventure set in the End Times of the iconic Warhammer Fantasy world.",
                MinimumPlayerCount = 1,
                MaximumPlayerCount = 4,
                PrimaryImageURL = "http://cdn.akamai.steamstatic.com/steam/apps/235540/header.jpg?t=1446475925",
                TrailerURL = "https://www.youtube.com/embed/KxTaQmhztVQ"
            };

            Game fallout4 = new Game
            {
                Name = "Fallout 4",
                ESRBRatingId = "M",
                LongDescription = "Bethesda Game Studios, the award-winning creators of Fallout 3 and The Elder Scrolls V: Skyrim, welcome you to the world of Fallout 4 � their most ambitious game ever, and the next generation of open-world gaming. As the sole survivor of Vault 111, you enter a world destroyed by nuclear war.Every second is a fight for survival, and every choice is yours.Only you can rebuild and determine the fate of the Wasteland.Welcome home.",
                ShortDescription = "Bethesda Game Studios welcome you to the world of Fallout 4 � their most ambitious game ever, and the next generation of open-world gaming.",
                MinimumPlayerCount = 1,
                MaximumPlayerCount = 1,
                PrimaryImageURL = "http://cdn.akamai.steamstatic.com/steam/apps/377160/header.jpg?t=1446248342",
                TrailerURL = "http://cdn.akamai.steamstatic.com/steam/apps/256657338/movie480.webm?t=1444922899"
            };

            context.Games.AddOrUpdate(
                g => g.Name,
                halo5,
                vermintide,
                fallout4
            );

            Tag shooterTag = context.Tags.Find("Shooter");
            Tag simulationTag = context.Tags.Find("Simulation");

            halo5.Tags = halo5.Tags ?? new List<Tag>();
            vermintide.Tags = vermintide.Tags ?? new List<Tag>();
            fallout4.Tags = fallout4.Tags ?? new List<Tag>();

            halo5.Tags.Add(simulationTag);
            vermintide.Tags.Add(simulationTag);
            vermintide.Tags.Add(shooterTag);
            fallout4.Tags.Add(shooterTag);
#endregion Debug Only Seed Values

            /* Note: Uncomment this to regenerate the EnumToLookup script used in AddEnumToLookupMigration
                     After running Update-Database, copy the SQL Script from the exception message
            */
            /*EnumToLookup enumToLookup = new EnumToLookup
            {
                TableNamePrefix = "",
                TableNameSuffix = "_Lookup",
                NameFieldLength = 64,
                UseTransaction = true
            };

            var migrationSQL = enumToLookup.GenerateMigrationSql(context);
            
            throw new Exception(migrationSQL);*/
        }
    }
}
