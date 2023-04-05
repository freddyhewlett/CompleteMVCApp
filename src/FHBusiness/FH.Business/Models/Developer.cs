using System;

namespace FH.Business.Models
{
    public class Developer : Entity
    {
        public string Name { get; set; }
        public string Document { get; set; }
        public DeveloperType DeveloperType { get; set; }
        public Address Address { get; set; }
        public bool Active { get; set; }


        public IEnumerable<Game> Games { get; set; }
    }
}
