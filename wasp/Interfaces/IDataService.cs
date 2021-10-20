using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using wasp.Models;

namespace wasp.Interfaces
{
    internal interface IDataService
    {

        //Issue Functions
        public WASPResponse<Issue> GetIssue();
        public WASPResponse<Issue> CreateIssue();
        public WASPResponse<Issue> DeleteIssue();
        public WASPResponse<Issue> VerifyIssue();
    }
}
