﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WASP.Models;
using WASP.Objects;

namespace WASP.Interfaces
{
    public interface IDataService
    {

        // Issue Functions        
        public Task<DataResponse<Issue>> GetIssueDetails(int issueId);
        public Task<DataResponse<IEnumerable<IssuesOverviewDTO>>> GetIssueOverview(IssuesOverviewFilter issueOverviewFilter);
        public Task<DataResponse<Issue>> CreateIssue(Issue issue);
        public Task<DataResponse<Issue>> UpdateIssue(Issue issue);
        public Task<DataResponse> DeleteIssue(int issueId);
        public Task<DataResponse> VerifyIssue(int issueId);
        public Task<DataResponse> ReportIssue(int issueId);
        public Task<DataResponse<IEnumerable<Category>>> GetCategories();

        // Municipality Functions
        public Task<DataResponse<MunicipalityUser>> MunicipalSignUp(MunicipalityUser muniUser);
        public Task<DataResponse<MunicipalityUser>> MunicipalLogIn(MunicipalityUser muniUser);
        public Task<DataResponse<MunicipalityResponse>> CreateResponse(MunicipalityResponse response);
        public Task<DataResponse<MunicipalityResponse>> UpdateResponse(MunicipalityResponse response);
        public Task<DataResponse> DeleteResponse(int responseId);
        public Task<DataResponse<Issue>> UpdateIssueStatus(int issueId);

        // User Functions
        public Task<DataResponse<Citizen>> CitizenSignUp(Citizen citizen);
        public Task<DataResponse<Citizen>> CitizenLogIn(Citizen citizen);
        public Task<DataResponse> BlockCitizen(int citizenId);
        public Task<DataResponse> UnblockCitizen(int citizenId);
        public Task<DataResponse> DeleteCitizen(int citizenId);
    }
}
