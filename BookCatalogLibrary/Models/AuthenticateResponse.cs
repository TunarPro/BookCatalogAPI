﻿namespace BookCatalogLibrary.Models
{
    public class AuthenticateResponse
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Role { get; set; }
        public string GivenName { get; set; }
        public string Token { get; set; }
    }
}
