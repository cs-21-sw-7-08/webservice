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
            new Issue(){ Id=1, Name = "Graffiti", Description = "TestDesc", Status=0},
            new Issue(){ Id=2, Name = "Hul i vejen", Description = "TestDesc2", Status=1}
        };
    }
}
