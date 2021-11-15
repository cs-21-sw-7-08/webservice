using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WASP.Models;

namespace WASP.Test.Model
{
    public class MockHiveContext : HiveContext
    {
        public MockHiveContext(DbContextOptions<HiveContext> options, bool resetDatabase = false) : base(options)
        {
            if (resetDatabase)
                this.Database.EnsureDeleted();
            AddMockData();
        }

        /// <summary>
        /// Add mock data
        /// </summary>
        private void AddMockData()
        {
            if (Citizens.Any())
                return;

            // Citizens
            Citizens.AddRange(
                new Citizen()
                {
                    Id = 1,
                    Email = "email@email.dk",
                    PhoneNo = null,
                    Name = "Hans",
                    IsBlocked = false
                },
                new Citizen()
                {
                    Id = 2,
                    Email = null,
                    PhoneNo = "12345678",
                    Name = "Birte",
                    IsBlocked = false
                },
                new Citizen()
                {
                    Id = 3,
                    Email = "anders@email.dk",
                    PhoneNo = null,
                    Name = "Anders",
                    IsBlocked = false
                },
                new Citizen()
                {
                    Id = 4,
                    Email = "mikkel@email.dk",
                    PhoneNo = null,
                    Name = "Mikkel",
                    IsBlocked = false
                },
                new Citizen()
                {
                    Id = 5,
                    Email = "yolo@email.dk",
                    PhoneNo = null,
                    Name = "Fredinand",
                    IsBlocked = true,
                }
            );
            // Categories
            Categories.AddRange(
                new Category()
                {
                    Id = 1,
                    Name = "Sidewalk"
                },
                new Category()
                {
                    Id = 2,
                    Name = "Road"
                }
            );
            // SubCategories
            SubCategories.AddRange(
                new SubCategory()
                {
                    Id = 1,
                    CategoryId = 1,
                    Name = "Hole in sidewalk"
                },
                new SubCategory()
                {
                    Id = 2,
                    CategoryId = 1,
                    Name = "Slippery sidewalk"
                },
                new SubCategory()
                {
                    Id = 3,
                    CategoryId = 2,
                    Name = "Tree on road"
                },
                new SubCategory()
                {
                    Id = 4,
                    CategoryId = 2,
                    Name = "Hole in road"
                }
            );
            // IssueStates
            IssueStates.AddRange(
                new IssueState()
                {
                    Id = 1,
                    Name = "Created"
                },
                new IssueState()
                {
                    Id = 2,
                    Name = "Approved"
                },
                new IssueState()
                {
                    Id = 3,
                    Name = "Resolved"
                },
                new IssueState()
                {
                    Id = 4,
                    Name = "Ikke Løst"
                }
            );
            // Municipalities
            Municipalities.AddRange(
                new Municipality()
                {
                    Id = 1,
                    Name = "Aalborg"
                },
                new Municipality()
                {
                    Id = 2,
                    Name = "Vesthimmerland"
                },
                new Municipality()
                {
                    Id = 3,
                    Name = "Randers"
                }
            );
            // MunicipalityUsers
            MunicipalityUsers.AddRange(
                new MunicipalityUser()
                {
                    Id = 1,
                    Email = "grete@aalborg.dk",
                    Password = "12345678",
                    Name = "Grete",
                    MunicipalityId = 1
                },
                new MunicipalityUser()
                {
                    Id = 2,
                    Email = "bente@aalborg.dk",
                    Password = "12345678",
                    Name = "Bente",
                    MunicipalityId = 1
                },
                new MunicipalityUser()
                {
                    Id = 3,
                    Email = "annette@aalborg.dk",
                    Password = "12345678",
                    Name = "Annette",
                    MunicipalityId = 1
                }
            );
            // Issues
            Issues.AddRange(
                new Issue()
                {
                    Id = 1,
                    CitizenId = 1,
                    MunicipalityId = 1,
                    IssueStateId = 1,
                    CategoryId = 1,
                    SubCategoryId = 1,
                    Description = "Jeg hader huller i fortorvet",
                    DateCreated = DateTime.Parse("2021-10-21 13:44:15"),
                    LocationPlaceHolder = new Objects.Location(57.012218, 9.994330),
                    Address = "Alfred Nobels Vej 27, 9200 Aalborg, Danmark"
                },
                new Issue()
                {
                    Id = 2,
                    CitizenId = 2,
                    MunicipalityId = 2,
                    IssueStateId = 2,
                    CategoryId = 1,
                    SubCategoryId = 2,
                    Description = "Jeg hader glatte fortorv",
                    DateCreated = DateTime.Parse("2021-10-21 13:44:15"),
                    LocationPlaceHolder = new Objects.Location(56.952687, 9.241946),
                    Address = "Sjægten 1, 9670 Løgstør, Danmark"
                },
                new Issue()
                {
                    Id = 3,
                    CitizenId = 3,
                    MunicipalityId = 3,
                    IssueStateId = 3,
                    CategoryId = 2,
                    SubCategoryId = 3,
                    Description = "Jeg hader træer på vejen",
                    DateCreated = DateTime.Parse("2021-10-21 13:44:15"),
                    LocationPlaceHolder = new Objects.Location(56.456943, 10.029387),
                    Address = "Hobrovej 126, 9530 Støvring, Danmark"
                }
            );
            // MunicipalityResponses
            MunicipalityResponses.AddRange(
                new MunicipalityResponse()
                {
                    Id = 1,
                    IssueId = 2,
                    MunicipalityUserId = 2,
                    Response = "Det kigger vi på straks, der skal nok komme salt på fortorvet",
                    DateCreated = DateTime.Parse("2021-10-22 13:44:15"),                    
                },
                new MunicipalityResponse()
                {
                    Id = 2,
                    IssueId = 3,
                    MunicipalityUserId = 3,
                    Response = "Vi har taget os af det meget farlige træ",
                    DateCreated = DateTime.Parse("2021-10-22 13:44:15"),
                }
            );
            // ReportCategories
            ReportCategories.AddRange(
                new ReportCategory()
                {
                    Id = 1,
                    Name = "Ikke relevant"
                }
            );
            // Reports
            Reports.AddRange(
                new Report()
                {
                    Id = 1,
                    IssueId = 1,
                    ReportCategoryId = 1,
                    TypeCounter = 1
                }
            );
            // IssueVerifications
            IssueVerifications.AddRange(
                new IssueVerification()
                {
                    Id = 1,
                    IssueId = 1,
                    CitizenId = 2
                },
                new IssueVerification()
                {
                    Id = 2,
                    IssueId = 2,
                    CitizenId = 1
                }
            );

            // Save the data
            SaveChanges();
        }
    }
}
