﻿namespace DDDinner.Domain.Entities
{
    public class User
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public byte[] PasswordHash { get; set; }    
        public byte[] PasswordSalt { get; set; }
    }
}
