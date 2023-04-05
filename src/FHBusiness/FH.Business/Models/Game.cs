using System;

namespace FH.Business.Models
{
    public class Game : Entity
    {
        public Guid DeveloperId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public decimal Value { get; set; }
        public DateTime RegisterDate { get; set; }
        public bool Active { get; set; }

        public Developer Developer { get; set; }
    }
}
