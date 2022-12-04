namespace API.Models.Entities
{
    public class Receipt
    {
        public Guid ReceiptID { get; set; }
        public int TotalAmount { get; set; }
        public DateTime Date { get; set; }

        //public virtual List<SoldItems> SoldItems { get;set; }

    }
}
