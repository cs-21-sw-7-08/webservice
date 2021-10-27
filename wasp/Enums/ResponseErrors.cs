using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WASP.Enums
{
    public enum ResponseErrors
    {
        AnExceptionOccurredInAController = 1,
        AnExceptionOccurredInTheDAL = 2,

        CategoryDoesNotExist = 100,
        SubCategoryDoesNotExist = 101,
        MunicipalityDoesNotExist = 102,
        PleaseProvideBothToAndFromTime = 103,
        IssueStateDoesNotExist = 104,
        CitizenDoesNotExist = 105,
        IssueDoesNotExist = 106
    }
}
