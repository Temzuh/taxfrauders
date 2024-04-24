using System.ComponentModel.DataAnnotations;

namespace taxfrauders.Models
{
    public class Workplace { 
    
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }

		public int UserId { get; set; } 
		public User User { get; set; } = null!; 
	}
}

