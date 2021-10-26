using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WASP.Models
{
    public class IssueStateDTO
    {
        public IssueStateDTO(IssueState issueState)
        {
            Id = issueState.Id;
            Name = issueState.Name;
        }

        public int Id { get; set; }
        public string Name { get; set; }
    }
}
