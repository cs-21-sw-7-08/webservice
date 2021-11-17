using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WASP.Models;
using WASP.Models.DTOs;
using WASP.Objects;

namespace WASP.Interfaces
{
    public interface IDataService
    {

        // Issue Functions        
        /// <summary>
        /// This function is used when more information on a specific Issue is needed. It takes an issueId and returns an IssueDatailsDTO that contains the details of the given Issue. These Include the state, 
        /// the category and subcategory, the municipality and any municipality responses and the number of verifications.
        /// </summary>
        /// <param name="issueId">Int identifier for the issue </param>
        /// <returns></returns>
        public Task<DataResponse<IssueDetailsDTO>> GetIssueDetails(int issueId);
        /// <summary>
        /// Takes an IssuesOverviewFilter as input and returns a list of IssuesOverviewDTO. IssuesOverviewFilter filters Issues by properties of the issues including
        /// the issue state, the category and subcategory and a given time period. 
        /// </summary>
        /// <param name="issueOverviewFilter">A collection of properties that is used in the filter</param>
        /// <returns></returns>
        public Task<DataResponse<IEnumerable<IssuesOverviewDTO>>> GetIssueOverview(IssuesOverviewFilter issueOverviewFilter);
        /// <summary>
        /// Takes an IssueCreateDTO as input and returns a success or error DataResponse. 
        /// The IssueCreateDTO needs to contain an int CitizenId, a string Description, an int MunicipalityId, a subcategoryId and a location.
        /// </summary>
        /// <param name="issue">Contains a int CitizenId identifier a string Description, an int MunicipalityId identifier, a subcategoryId and a location </param>
        /// <returns></returns>
        public Task<DataResponse> CreateIssue(IssueCreateDTO issue);
        /// <summary>
        /// Takes an int IssueId and a list of WASPUpdate updates and returns a DataResponse. 
        /// The updates consist of a Name that identifies attributes being updated and a Value that is the updated value.   
        /// </summary>
        /// <param name="issueId">A int issueId identifier</param>
        /// <param name="updates">A list of value identifiers and the update value</param>
        /// <returns></returns>
        public Task<DataResponse> UpdateIssue(int issueId, IEnumerable<WASPUpdate> updates);
        /// <summary>
        /// Takes a int issueId that is then deleted and returns a DataResponse.
        /// </summary>
        /// <param name="issueId">Int issue identifier</param>
        /// <returns></returns>
        public Task<DataResponse> DeleteIssue(int issueId);
        /// <summary>
        /// Takes a int issueId and a int citizenId and returns a DataResponse. This function increments the value of verification of the issueId. 
        /// An issue created by a citizen cannot be verified by the same citizen, and it cannot be verified twice by the same person.
        /// </summary>
        /// <param name="issueId">Int issue identifier</param>
        /// <param name="citizenId">Int citizen identifier</param>
        /// <returns></returns>
        public Task<DataResponse> VerifyIssue(int issueId, int citizenId);
        /// <summary>
        /// Takes a int issueId and a int reportCategoryId and creates a report on given issue and returns a DataResponse. 
        /// If a report of the same category exists on the issue then a report TypeCounter is incremented instead.
        /// </summary>
        /// <param name="issueId">Int issue identifier</param>
        /// <param name="reportCategoryId">Int identfier for the report category</param>
        /// <returns></returns>
        public Task<DataResponse> ReportIssue(int issueId, int reportCategoryId);
        /// <summary>
        /// Returns a list of all the categories.
        /// </summary>
        /// <returns></returns>
        public Task<DataResponse<IEnumerable<CategoryListDTO>>> GetCategories();
        /// <summary>
        /// Takes an int issueId and a int issueStateId and returns a DataResponse. 
        /// This fuctions updates the state of the issue to the given issueStateId. 
        /// </summary>
        /// <param name="issueId">int identifier for the issue</param>
        /// <param name="issueStateId">int identifier for the issue state</param>
        /// <returns></returns>
        public Task<DataResponse> UpdateIssueStatus(int issueId, int issueStateId);

        /// <summary>
        /// This function returns a list of all the report categories. 
        /// </summary>
        /// <returns></returns>
        public Task<DataResponse<IEnumerable<ReportCategoryDTO>>> GetReportCategories();

        // Municipality Functions
        /// <summary>
        /// Returns a list of all the registered municipalities.
        /// </summary>
        /// <returns></returns>
        public Task<DataResponse<IEnumerable<MunicipalityDTO>>> GetMunicipalities();
        /// <summary>
        /// Takes a MunicipalityUserSignupInputDTO and returns a MunicipalityUserSignupInputDTO. This function creates a new municipality account in the system.
        /// The MunicipalityUserSignupInputDTO needs an Email, Password, Name and a valid Int MunicipalityId. During creation the account will be given a Id.
        /// </summary>
        /// <param name="muniUser">A set of properties used when signing up a municipality user. 
        /// These include an Email, Password, Name and a valid Int MunicipalityId </param>
        /// <returns></returns>
        public Task<DataResponse<MunicipalityUserSignUpOutputDTO>> MunicipalitySignUp(MunicipalityUserSignUpInputDTO muniUser);
        /// <summary>
        /// Takes a MunicipalityUserLogin DTO and returns a MunicipalityUserDTO. This function is used when a municipality user login.
        /// The MunicipalityUserLogin needs a email and a password. Neither is case sensitive. 
        /// </summary>
        /// <param name="muniUser">A set of properties used when a municipality. Contains an email and a password</param>
        /// <returns></returns>
        public Task<DataResponse<MunicipalityUserDTO>> MunicipalityLogIn(MunicipalityUserLoginDTO muniUser);
        /// <summary>
        /// Takes a MunicipalityResponseInputDTO and returns a MunicipalityResponseOutputDTO. This function creates a Municipality Response on a specified Issue
        /// The MunicipalityResponseInputDTO needs a int IssueId, a MunicipalityUserId and a Response message. 
        /// </summary>
        /// <param name="response">Contains the information nessecary to create a response.
        /// This includes a int issue identifier, a int municipalityuser identifier and a string response message</param>
        /// <returns></returns>
        public Task<DataResponse<MunicipalityResponseOutputDTO>> CreateResponse(MunicipalityResponseInputDTO response);
        /// <summary>
        /// Takes a int responseId and a list of WASPUpdate updates and returns the updated Municipality response. 
        /// This function updates a given municipality response values specified by the list of updates given.
        /// </summary>
        /// <param name="responseId">Int identifier for municipality response</param>
        /// <param name="updates">The list of updates for the response. 
        /// A single update contains a string Name identifier for the value and the actual updated value. </param>
        /// <returns></returns>
        public Task<DataResponse<MunicipalityResponseOutputDTO>> UpdateResponse(int responseId, IEnumerable<WASPUpdate> updates);
        /// <summary>
        /// Takes a int responseId and returns a dataresponse. This function deletes the given response. 
        /// </summary>
        /// <param name="responseId">Int identifier for a municipality response</param>
        /// <returns></returns>
        public Task<DataResponse> DeleteResponse(int responseId);

        // Citizen Functions
        /// <summary>
        /// Takes a CitizenSignUpDTO and returns a CitizenDTO. This function creates a citizen with the information given in the CitizenSignUpDTO.
        /// This includes an email or a phone number, a name and a Id. Only an email or a phone number may be given not both. It then returns a created citizen.
        /// </summary>
        /// <param name="citizen">A set of properties used when creating a new citizen user. It contains an email or a phone number, a name and a Id</param>
        /// <returns></returns>
        public Task<DataResponse<CitizenDTO>> CitizenSignUp(CitizenSignUpDTO citizen);
        /// <summary>
        /// Takes a CitizenLoginDTO and return a CitizenDTO. This function is used when a citizen login using the information given in the CitizenLoginDTO,
        /// which is a email or a phone number. It then returns the information of the loggedin citizen.
        /// 
        /// </summary>
        /// <param name="citizen">Contains either a phone number or an email.</param>
        /// <returns></returns>
        public Task<DataResponse<CitizenDTO>> CitizenLogIn(CitizenLoginDTO citizen);
        /// <summary>
        /// Takes a int citizen identifier and returns a data response. This function blocks the given citizen.
        /// 
        /// </summary>
        /// <param name="citizenId">Int identifier for citizen.</param>
        /// <returns></returns>
        public Task<DataResponse> BlockCitizen(int citizenId);
        /// <summary>
        /// Takes a int citizenId and returns a data response. This function unblocks a given citizen.
        /// </summary>
        /// <param name="citizenId">A int Identifier for citizen.</param>
        /// <returns></returns>
        public Task<DataResponse> UnblockCitizen(int citizenId);
        /// <summary>
        /// Takes a int citizenId and returns a data response. This function deletes a given citizen.
        /// </summary>
        /// <param name="citizenId">A int identifier for citizen.</param>
        /// <returns></returns>
        public Task<DataResponse> DeleteCitizen(int citizenId);
        /// <summary>
        /// This function takes a int citizenId and returns bool. This function checks if a given citizen is blocked or not. It then returns true or false.
        /// </summary>
        /// <param name="citizenId">A int identifier for citizen.</param>
        /// <returns></returns>
        public Task<DataResponse<bool>> IsBlockedCitizen(int citizenId);
    }
}
