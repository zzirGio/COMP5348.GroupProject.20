namespace Bank.Business.Components.Model
{
    public class FundTransferTransaction
    {
        public double Amount { get; set; }
        public int FromAcctNumber { get; set; }
        public int ToAcctNumber { get; set; }
    }
}