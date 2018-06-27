
using System.ComponentModel.DataAnnotations;

namespace Waes.Core
{
    public class Entity
    {
        [Required]
        [Key]
        public int Id {get; set;}
        public string Left {get; set;}
        public string Right {get; set;}
    }
}
