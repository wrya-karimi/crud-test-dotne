namespace Mc2.CrudTest.Domain.Entities
{
    public class Customer
    {
        public int Id { get; set; }
        public required string Firstname { get; set; }
        public required string Lastname { get; set; }
        public DateTime DateOfBirth { get; set; }
        public ulong PhoneNumber { get; set; }
        public required string Email { get; set; }
        public required string BankAccountNumber { get; set; }
    }
}