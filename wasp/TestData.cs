using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using wasp.Models;

namespace wasp.DataAccessLayer
{
    public class TestData
    {
        public List<Issue> mockdata = new List<Issue>{
            new Issue(){ Name = "?"},
            new Issue()
        };
    }
}
