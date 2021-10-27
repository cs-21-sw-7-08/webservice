using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WASP.Models;

namespace WASP.DataAccessLayer
{
    public class TestData
    {
        public List<Issue> mockdata = new List<Issue>{
            new Issue(){ Id=1, Description = "TestDesc"},
            new Issue(){ Id=2, Description = "TestDesc2"}
        };
    }
}
