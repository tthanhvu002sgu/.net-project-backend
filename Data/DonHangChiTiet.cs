namespace DoAn_API.Data
{
    public class DonHangChiTiet
    {
        public Guid MaDonHang { get; set; }
        public Guid Id { get; set; }
        public int SoLuong { get; set; }
        public Double DonGia { get; set; }

        //relationship
        public DonHang DonHang { get; set; }
        public product product {  get; set; }

    }
}
