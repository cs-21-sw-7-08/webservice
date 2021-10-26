using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WASP.Interfaces;
using WASP.Models;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using WASP.Objects;
using WASP.Enums;

namespace WASP.DataAccessLayer
{
    public class DataService : IDataService
    {
        #region Properties

        public IDbContextFactory<HiveContext> ContextFactory { get; set; }

        #endregion

        #region Constructor

        public DataService(IDbContextFactory<HiveContext> contextFactory)
        {
            ContextFactory = contextFactory;
        }

        #endregion

        #region Methods

        public async Task<DataResponse> BlockCitizen(int citizenId)
        {
            using (var context = ContextFactory.CreateDbContext())
            {
                // Get citizen
                Citizen citizen = await context.Citizens.FirstOrDefaultAsync(x => x.Id == citizenId);
                // Make error checks
                if (citizen == null)
                    return new DataResponse(((int)ResponseErrors.CitizenDoesNotExist));
                // Set flag
                citizen.IsBlocked = true;
                // Save the changes
                await context.SaveChangesAsync();
                // Return success response
                return new DataResponse();
            }
        }

        public Task<DataResponse<Citizen>> CitizenLogIn(Citizen citizen)
        {
            throw new NotImplementedException();
        }

        public Task<DataResponse<Citizen>> CitizenSignUp(Citizen citizen)
        {
            throw new NotImplementedException();
        }

        public Task<DataResponse<Issue>> CreateIssue(Issue issue)
        {
            throw new NotImplementedException();
        }

        public Task<DataResponse<MunicipalityResponse>> CreateResponse(MunicipalityResponse response)
        {
            throw new NotImplementedException();
        }

        public Task<DataResponse> DeleteIssue(int issueId)
        {
            throw new NotImplementedException();
        }

        public Task<DataResponse> DeleteResponse(int responseId)
        {
            throw new NotImplementedException();
        }

        public Task<DataResponse> DeleteCitizen(int citizenId)
        {
            throw new NotImplementedException();
        }

        public Task<DataResponse<IEnumerable<Category>>> GetCategories()
        {
            throw new NotImplementedException();
        }

        public async Task<DataResponse<Issue>> GetIssueDetails(int issueId)
        {
            using (var context = ContextFactory.CreateDbContext())
            {
                var issue = await context.Issues
                    .AsNoTracking()
                    .Include(issue => issue.MunicipalityResponses)
                    .Include(issue => issue.IssueState)
                    .FirstOrDefaultAsync();
                return new DataResponse<Issue>(issue);
            }
        }

        public async Task<DataResponse<IEnumerable<IssuesOverviewDTO>>> GetIssueOverview(IssuesOverviewFilter filter)
        {
            using (var context = ContextFactory.CreateDbContext())
            {                
                var list = await context.Issues.Select(issue => new IssuesOverviewDTO
                {
                    Id = issue.Id,
                    Description = issue.Description,
                    MunicipalityId = issue.MunicipalityId,
                    CategoryId = issue.CategoryId,
                    CitizenId = issue.CitizenId,
                    DateCreated = issue.DateCreated,
                    IssueStateId = issue.IssueStateId,
                    Location = issue.Location,
                    SubCategoryId = issue.SubCategoryId
                })
                .ToListAsync();
                return new DataResponse<IEnumerable<IssuesOverviewDTO>>(list.AsEnumerable());
            }
        }

        public Task<DataResponse<MunicipalityUser>> MunicipalLogIn(MunicipalityUser muniUser)
        {
            throw new NotImplementedException();
        }

        public Task<DataResponse<MunicipalityUser>> MunicipalSignUp(MunicipalityUser muniUser)
        {
            throw new NotImplementedException();
        }

        public Task<DataResponse> ReportIssue(int issueId)
        {
            throw new NotImplementedException();
        }

        public Task<DataResponse> UnblockCitizen(int citizenId)
        {
            throw new NotImplementedException();
        }

        public Task<DataResponse<Issue>> UpdateIssue(Issue issue)
        {
            /*Issue modIssue = testy.mockdata.FirstOrDefault(x => x.Id == issue.Id);
            if (modIssue == null)
            {
                return null;
            }
            PropertyInfo[] properties = typeof(Issue).GetProperties();
            foreach (PropertyInfo property in properties)
            {
                if (property.GetValue(modIssue).ToString() != property.GetValue(issue).ToString())
                    property.SetValue(modIssue, property.GetValue(issue));
            }*/

            throw new NotImplementedException();
        }

        public Task<DataResponse<Issue>> UpdateIssueStatus(int issueId)
        {
            throw new NotImplementedException();
        }

        public Task<DataResponse<MunicipalityResponse>> UpdateResponse(MunicipalityResponse response)
        {
            throw new NotImplementedException();
        }

        public Task<DataResponse> VerifyIssue(int issueId)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
