using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WASP.Enums;
using WASP.Interfaces;
using WASP.Models;
using WASP.Objects;

namespace WASP.DataAccessLayer
{
    public partial class DataService : IDataService
    {
        #region Methods

        public Task<DataResponse<Issue>> CreateIssue(Issue issue)
        {
            throw new NotImplementedException();
        }

        public Task<DataResponse> DeleteIssue(int issueId)
        {
            throw new NotImplementedException();
        }

        public async Task<DataResponse<IEnumerable<CategoryListDTO>>> GetCategories()
        {
            using (var context = ContextFactory.CreateDbContext())
            {
                var categories = await context.Categories
                    .AsNoTracking()
                    .Include(category => category.SubCategories)
                    .Select(category => new CategoryListDTO(category))
                    .ToListAsync();
                return new DataResponse<IEnumerable<CategoryListDTO>>(categories);
            }
        }

        public async Task<DataResponse<IssueDetailsDTO>> GetIssueDetails(int issueId)
        {
            using (var context = ContextFactory.CreateDbContext())
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
        }

        public async Task<DataResponse<IEnumerable<IssuesOverviewDTO>>> GetIssueOverview(IssuesOverviewFilter filter)
        {
            using (var context = ContextFactory.CreateDbContext())
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
        }

        public Task<DataResponse> ReportIssue(int issueId)
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

        public Task<DataResponse> VerifyIssue(int issueId)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
