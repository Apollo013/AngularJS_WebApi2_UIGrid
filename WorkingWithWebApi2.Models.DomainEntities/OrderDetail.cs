namespace WorkingWithWebApi2.Models.DomainEntities
{
    public class OrderDetail
    {
        public int OrderID { get; set; }
        public int ProductID { get; set; }
        public decimal UnitPrice { get; set; } = 0.00M;
        public short Quantity { get; set; } = 1;
        public decimal Discount { get; set; } = 0.00M;

        public virtual Order Order { get; set; }
        public virtual Product Product { get; set; }
    }
}
