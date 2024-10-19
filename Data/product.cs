using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DoAn_API.Data
{
    //nay la mot entity
    [Table("product")]
    public class product
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
        [Range(0,int.MaxValue)]
        public int Price { get; set; }
        public string Description { get; set; }

        public int? IdType { get; set; }
        [ForeignKey("IdType")]
        public Type Type { get; set; }

        public ICollection<DonHangChiTiet> DonHangChiTiets { get; set; }
        public product()
        {
            DonHangChiTiets = new List<DonHangChiTiet>();
        }

    }
}
