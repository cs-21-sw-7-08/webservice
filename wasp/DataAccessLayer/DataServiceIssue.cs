using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using WASP.Enums;
using WASP.Interfaces;
using WASP.Models;
using WASP.Objects;
using WASP.Utilities;

namespace WASP.DataAccessLayer
{
    public partial class DataService : IDataService
    {
        #region Methods

        public async Task<DataResponse> CreateIssue(IssueCreateDTO issue)
        {
            return await DataServiceUtil.GetResponse(ContextFactory,
                async (context) =>
                {
                    // Create new issue
                    Issue newIssue = new();

                    var subCategory = await context.SubCategories.FirstOrDefaultAsync(x => x.Id == issue.SubCategoryId);
                    // Check if subcategory with the given ID exist
                    if (subCategory == null)
                        return new DataResponse(((int)ResponseErrors.SubCategoryDoesNotExist));

                    // Update properties
                    DataServiceUtil.UpdateProperties(issue, newIssue);
                    // Set date created
                    newIssue.DateCreated = DateTime.Now;
                    // Set category ID                
                    newIssue.CategoryId = subCategory.CategoryId;
                    // Set state ID
                    newIssue.IssueStateId = 1;

                    // Add new issue
                    var response = await context.Issues.AddAsync(newIssue);
                    // Save changes in database
                    var changes = await context.SaveChangesAsync();
                    // Return success response
                    return new DataResponse();
                }
            );
        }

        public Task<DataResponse> DeleteIssue(int issueId)
        {
            throw new NotImplementedException();
        }

        public async Task<DataResponse<IEnumerable<CategoryListDTO>>> GetCategories()
        {
            return await DataServiceUtil.GetResponse(ContextFactory,
                async (context) =>
                {
                    var categories = await context.Categories
                    .AsNoTracking()
                    .Include(category => category.SubCategories)
                    .Select(category => new CategoryListDTO(category))
                    .ToListAsync();
                    return new DataResponse<IEnumerable<CategoryListDTO>>(categories);
                }
            );
        }

        public async Task<DataResponse<IssueDetailsDTO>> GetIssueDetails(int issueId)
        {
            return await DataServiceUtil.GetResponse(ContextFactory,
                async (context) =>
                {
                    // Get issue
                    var issue = await context.Issues
                    .AsNoTracking()
                    .Include(issue => issue.MunicipalityResponses)
                    .Include(issue => issue.IssueState)
                    .Include(issue => issue.Category)
                    .Include(issue => issue.SubCategory)
                    .Include(issue => issue.Municipality)
                    .Select(issue => new IssueDetailsDTO(issue)
                    {
                        Id = issue.Id
                    })
                    .FirstOrDefaultAsync(x => x.Id == issueId);
                    // Make error checks
                    if (issue == null)
                        return new DataResponse<IssueDetailsDTO>((int)ResponseErrors.IssueDoesNotExist);
                    // Return success response
                    return new DataResponse<IssueDetailsDTO>(issue);
                }
            );
        }

        public async Task<DataResponse<IEnumerable<IssuesOverviewDTO>>> GetIssueOverview(IssuesOverviewFilter filter)
        {
            return await DataServiceUtil.GetResponse(ContextFactory,
                async (context) =>
                {
                    // Get issues
                    var list = await context.Issues
                    .AsNoTracking()
                    .Select(issue => new IssuesOverviewDTO(issue)
                    {
                        DateCreated = issue.DateCreated,
                        MunicipalityId = issue.MunicipalityId,
                        IssueStateId = issue.IssueStateId,
                        CategoryId = issue.CategoryId,
                        SubCategoryId = issue.SubCategoryId
                    })
                    // Filter -> FromTime
                    .Where(issue =>
                        filter.FromTime == null ||
                        (filter.FromTime != null && DateTime.Compare(filter.FromTime.Value, issue.DateCreated) <= 0)
                    )
                    // Filter -> ToTime
                    .Where(issue =>
                        filter.ToTime == null ||
                        (filter.ToTime != null && DateTime.Compare(filter.ToTime.Value, issue.DateCreated) > 0)
                    )
                    // Filter -> IssueState
                    .Where(issue =>
                        filter.IssueStateId == null ||
                        (filter.IssueStateId != null && issue.IssueStateId == filter.IssueStateId)
                    )
                    // Filter ->  Municipality
                    .Where(issue =>
                        filter.MunicipalityId == null ||
                        (filter.MunicipalityId != null && issue.MunicipalityId == filter.MunicipalityId)
                    )
                    // Filter -> SubCategory
                    .Where(issue =>
                        filter.SubCategoryId == null ||
                        (filter.SubCategoryId != null && issue.SubCategoryId == filter.SubCategoryId)
                    )
                    // Filter -> Category
                    .Where(issue =>
                        filter.CategoryId == null ||
                        (filter.CategoryId != null && issue.CategoryId == filter.CategoryId)
                    )
                    .ToListAsync();
                    // Return success response
                    return new DataResponse<IEnumerable<IssuesOverviewDTO>>(list.AsEnumerable());
                }
            );
        }

        public Task<DataResponse> ReportIssue(int issueId)
        {
            throw new NotImplementedException();
        }

        // TODO: Add error handling and constraints such as which properties may be updated
        public async Task<DataResponse> UpdateIssue(int issueId, IEnumerable<WASPUpdate> updates)
        {
            return await DataServiceUtil.GetResponse(ContextFactory,
               async (context) =>
               {                   
                   // Get issue
                   var issue = await context.Issues.FirstOrDefaultAsync(x => x.Id == issueId);
                   // Check if issue exist
                   if (issue == null)
                       return new DataResponse(((int)ResponseErrors.IssueDoesNotExist));

                   foreach (var update in updates)
                   {
                       DataServiceUtil.UpdateProperty(update.Value, update.Name, issue);
                   }

                   return new DataResponse();
               }
            );
        }

        public Task<DataResponse<Issue>> UpdateIssueStatus(int issueId)
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
