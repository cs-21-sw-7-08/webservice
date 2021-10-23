using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace wasp.Models
{
    public class Issue
    {
        public int Id { get; set; } 
        public string Name { get; set; }
        public string Description { get; set; }
        public int Status { get; set; }

        public override bool Equals(object obj)
        {
            if (obj is not Issue)
                return false;
            var objIssue = obj as Issue;
            return objIssue.Id == Id && objIssue.Name == Name && objIssue.Description == Description && objIssue.Status == Status;
        }
    }
}
