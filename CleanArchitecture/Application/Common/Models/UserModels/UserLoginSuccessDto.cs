﻿namespace Application.Common.Models.UserModels
{
    public class UserLoginSuccessDto
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string FirstName  { get; set; }
        public string LastName  { get; set; }
        public string Token  { get; set; }
    }
}
