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
                    await context.Issues.AddAsync(newIssue);
                    // Save changes in database
                    var changes = await context.SaveChangesAsync();
                    // Check that the number of changed entities is 1
                    // as one new Issue is added to the database
                    if (changes != 1)
                        return new DataResponse(((int)ResponseErrors.ChangesCouldNotBeAppliedToTheDatabase));

                    // Return success response
                    return new DataResponse();
                }
            );
        }

        public async Task<DataResponse> DeleteIssue(int issueId)
        {
            return await DataServiceUtil.GetResponse(ContextFactory,
                async (context) =>
                {
                    // Get issue
                    var issue = await context.Issues.FirstOrDefaultAsync(x => x.Id == issueId);
                    // Check if issue exist
                    if (issue == null)
                        return new DataResponse((int)ResponseErrors.IssueDoesNotExist);
                    // Get verifications
                    var verifications = await context.IssueVerifications
                                             .Where(x => x.IssueId == issueId)
                                             .ToListAsync();
                    // Remove any verifications
                    if (verifications.Count > 0)
                        context.IssueVerifications.RemoveRange(verifications);
                    // Get reports
                    var reports = await context.Reports
                                        .Where(x => x.IssueId == issueId)
                                        .ToListAsync();
                    // Remove any reports
                    if (reports.Count > 0)
                        context.Reports.RemoveRange(reports);
                    // Get municipality responses
                    var municipalityResponses = await context.MunicipalityResponses
                                                .Where(x => x.IssueId == issueId)
                                                .ToListAsync();
                    // Remove any municipality responses
                    if (municipalityResponses.Count > 0)
                        context.MunicipalityResponses.RemoveRange(municipalityResponses);
                    // Remove issue
                    context.Issues.Remove(issue);

                    // Save changes in database
                    var changes = await context.SaveChangesAsync();                    

                    // Return success response
                    return new DataResponse();
                }
            );
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

        public async Task<DataResponse> ReportIssue(int issueId, int reportCategoryId)
        {
            return await DataServiceUtil.GetResponse(ContextFactory,
                async (context) =>
                {
                    // Get issue
                    var issue = await context.Issues.FirstOrDefaultAsync(x => x.Id == issueId);
                    // Check if issue exist
                    if (issue == null)
                        return new DataResponse((int)ResponseErrors.IssueDoesNotExist);
                    // Get report category
                    var reportCategory = await context.ReportCategories.FirstOrDefaultAsync(x => x.Id == reportCategoryId);
                    // Check if report category exist
                    if (reportCategory == null)
                        return new DataResponse((int)ResponseErrors.ReportCategoryDoesNotExist);
                    // Get report
                    var report = await context.Reports
                                       .FirstOrDefaultAsync(x => x.IssueId == issueId && x.ReportCategoryId == reportCategoryId);
                    // Check if report exist
                    if (report == null)
                    {
                        // Add report
                        await context.Reports.AddAsync(new Report()
                        {
                            IssueId = issueId,
                            ReportCategoryId = reportCategoryId,
                            TypeCounter = 1
                        });
                    }
                    else
                    {
                        // Increase type counter to indicate another report has
                        // been created for the given report category
                        report.TypeCounter++;
                    }
                    
                    // Save changes to the database
                    await context.SaveChangesAsync();

                    // Return success response
                    return new DataResponse();
                }
            );
        }

        public async Task<DataResponse> UpdateIssue(int issueId, IEnumerable<WASPUpdate> updates)
        {
            // Check WASPUpdate list
            if (!DataServiceUtil.CheckWASPUpdateList(updates.ToList(), Issue.GetPropertiesThatAreAllowedToBeUpdated()))
                return new DataResponse((int)ResponseErrors.WASPUpdateListBadFormat);

            return await DataServiceUtil.GetResponse(ContextFactory,
               async (context) =>
               {
                   // Get issue
                   var issue = await context.Issues.FirstOrDefaultAsync(x => x.Id == issueId);
                   // Check if issue exist
                   if (issue == null)
                       return new DataResponse(((int)ResponseErrors.IssueDoesNotExist));
                   // Go through the updates
                   foreach (var update in updates)
                   {
                       // Update property value
                       DataServiceUtil.UpdateProperty(update.Value, update.Name, issue);

                       switch (update.Name)
                       {
                           // If SubCategoryId has been set, also update CategoryId
                           case nameof(Issue.SubCategoryId):
                               // Get SubCategory
                               var subCategory = await context.SubCategories.FirstOrDefaultAsync(x => x.Id == issue.SubCategoryId);
                               // Check if it exist
                               if (subCategory == null)
                                   return new DataResponse((int)ResponseErrors.SubCategoryDoesNotExist);
                               // Update CategoryId
                               issue.CategoryId = subCategory.CategoryId;
                               break;
                       }
                   }
                   // Save changes to the database
                   await context.SaveChangesAsync();
                   // Return success response
                   return new DataResponse();
               }
            );
        }

        public async Task<DataResponse> UpdateIssueStatus(int issueId, int issueStateId)
        {
            return await DataServiceUtil.GetResponse(ContextFactory,
               async (context) =>
               {
                   var issue = await context.Issues.FirstOrDefaultAsync(x => x.Id == issueId);
                   // Check if issue exist
                   if (issue == null)
                       return new DataResponse((int)ResponseErrors.IssueDoesNotExist);
                   var issueState = await context.IssueStates.FirstOrDefaultAsync(x => x.Id == issueStateId);
                   // Check if issue state exist
                   if (issueState == null)
                       return new DataResponse((int)ResponseErrors.IssueStateDoesNotExist);

                   IssueStates issueStateValue = issue.IssueStateId.ToEnum<IssueStates>();
                   IssueStates newIssueStateValue = issueState.Id.ToEnum<IssueStates>();

                   // Get flag deciding whether the state change is approved
                   // Created according to behaviour state diagram
                   bool stateChangeApproved = issueStateValue switch
                   {
                       IssueStates.Created => newIssueStateValue switch
                       {                           
                           IssueStates.Approved or 
                           IssueStates.Resolved => true,                           
                           _ => false
                       },
                       IssueStates.Approved => newIssueStateValue switch
                       {
                           IssueStates.Resolved => true,
                           _ => false
                       },
                       IssueStates.Resolved => newIssueStateValue switch
                       {
                           _ => false
                       },
                       _ => false
                   };
                   // Check if state change was approved
                   if (!stateChangeApproved)
                       return new DataResponse((int)ResponseErrors.DisallowedIssueStateChange);

                   // Return success response
                   return new DataResponse();
               }
           );
        }

        public async Task<DataResponse> VerifyIssue(int issueId, int citizenId)
        {
            return await DataServiceUtil.GetResponse(ContextFactory,
                async (context) =>
                {
                    // Get issue
                    var issue = await context.Issues.FirstOrDefaultAsync(x => x.Id == issueId);
                    // Check if issue exist
                    if (issue == null)
                        return new DataResponse((int)ResponseErrors.IssueDoesNotExist);
                    // Check issue creator
                    if (issue.CitizenId == citizenId)
                        return new DataResponse((int)ResponseErrors.IssueCannotBeVerifiedByItsCreator);
                    // Get citizen
                    var citizen = await context.Citizens.FirstOrDefaultAsync(x => x.Id == citizenId);
                    // Check if citizen exist
                    if (citizen == null)
                        return new DataResponse((int)ResponseErrors.CitizenDoesNotExist);
                    var issueVerification = await context.IssueVerifications
                                                  .FirstOrDefaultAsync(x => x.IssueId == issueId && x.CitizenId == citizenId);                   
                    // Check if issue verification exist
                    if (issueVerification != null)
                        return new DataResponse((int)ResponseErrors.IssueAlreadyVerifiedByThisCitizen);
                    // Add issue verification
                    await context.IssueVerifications.AddAsync(new IssueVerification()
                    {
                        CitizenId = citizenId,
                        IssueId = issueId
                    });
                    // Save changes to the database
                    await context.SaveChangesAsync();

                    // Return success response
                    return new DataResponse();
                }
            );
        }

        #endregion
    }
}
