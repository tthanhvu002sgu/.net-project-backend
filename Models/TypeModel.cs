using System.ComponentModel.DataAnnotations;

namespace DoAn_API.Models
{
    public class TypeModel
    {
        [Required]
        [MaxLength(100)]
        public string NameType { get; set; }
    }
}
