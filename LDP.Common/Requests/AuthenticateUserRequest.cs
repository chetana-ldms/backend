﻿using LDP_APIs.Models;

namespace LDP_APIs.BL.APIRequests
{
    public class AuthenticateUserRequest
    {
        public string? UserName { get; set; }
        public string? Password { get; set; }

        public int OrgId { get; set; }

    }

    public class DeleteUserRequest
    {
        public int UserID { get; set; }
        public DateTime? DeletedDate { get; set; }
        public int DeletedUserId { get; set; }

    }

}
