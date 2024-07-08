using Common.Enums;
using CTEMS.Lib.Model;
using System;
using System.Collections.Generic;

namespace CTEMS.Service.DTO
{
    public class AuthenticateResponse
    {
        public long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Avatar { get; set; }
        public string Token { get; set; }
        public DateTime? TokenExpiryTime { get; set; }
        public string RefreshToken { get; set; }
        public IList<CTERole> Roles { get; set; }

        public AuthenticateResponse(User user, string token)
        {
            Id = user.Id;
            FirstName = user.Employee.FirstName;
            LastName = user.Employee.LastName;
            UserName = user.UserName;
            Email = user.Employee.Email;
            Avatar = "";
            Token = token;
        }
    }
}