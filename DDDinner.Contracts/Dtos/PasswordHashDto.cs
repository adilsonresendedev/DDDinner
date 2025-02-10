namespace DDDinner.Contracts.Dtos
{
    public class PasswordHashDto
    {
        public byte[] PassworHash { get; set; }
        public byte[] PasswordSalt { get; set; }
    }
}
