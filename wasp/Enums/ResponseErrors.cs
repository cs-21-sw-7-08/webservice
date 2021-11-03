using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WASP.Enums
{
    public enum ResponseErrors
    {
        /*Response codes for categorization
         * 0-99 - System Errors
         * 100-199 Issue-related Errors
         * 200-299 Citizen-related Errors
         * 300-399 Municipality-related errors
         */

        //System errors
        AnExceptionOccurredInAController = 1,
        AnExceptionOccurredInTheDAL = 2,
        ChangesCouldNotBeAppliedToTheDatabase = 5,
        WASPUpdateListBadFormat = 50,
        //Issue-related errors
        CategoryDoesNotExist = 100,
        SubCategoryDoesNotExist = 101,
        PleaseProvideBothToAndFromTime = 102,
        IssueStateDoesNotExist = 103,
        IssueDoesNotExist = 104,
        DisallowedIssueStateChange = 105,
        IssueAlreadyVerifiedByThisCitizen = 106,
        ReportCategoryDoesNotExist = 107,
        IssueCannotBeVerifiedByItsCreator = 108,
        //Citizen-related errors
        CitizenDoesNotExist = 200,
        CitizenLoginBothEmailAndPhoneNumberCannotBeFilled = 201,
        CitizenWithTheseCredentialsHasNotBeenSignedUp = 202,
        CitizenAlreadyBlocked = 203,
        CitizenAlreadyUnblocked = 204,
        //Municipality-related errors
        MunicipalityDoesNotExist = 300,
        MunicipalityUserDoesNotExist = 301,
        MunicipalityUserEmailAndOrPasswordNotMatched = 302,
        MunicipalityUserMunicipalityIdDoesNotMatchIssueId = 303,
        ResponseDoesNotExist = 304,
    }
}
