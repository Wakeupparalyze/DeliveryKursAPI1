﻿namespace DeliveryKursAPI1.DTO
{
    public class UserDTO
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public string Password { get; set; } = null!;

        public string Lastname { get; set; } = null!;

        public string Login { get; set; } = null!;
    }
}