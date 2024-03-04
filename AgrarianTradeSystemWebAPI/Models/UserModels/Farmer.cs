﻿using System.ComponentModel.DataAnnotations;

namespace AgrarianTradeSystemWebAPI.Models.UserModels
{
    public class Farmer
    {
        public string Username { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        [Key] public string Email { get; set; } = string.Empty;
        public bool EmailVerified { get; set; } = false;
        public string NIC { get; set; } = string.Empty;
        public string AddL1 { get; set; } = string.Empty;
        public string AddL2 { get; set; } = string.Empty;
        public string AddL3 { get; set; } = string.Empty;
        public string ProfileImg { get; set; } = string.Empty;
        public string VerificationToken { get; set; } = string.Empty;
        public DateTime VerifiedAt { get; set; }
        public string PasswordResetToken { get; set; } = string.Empty;
        public DateTime? ResetTokenExpireAt { get; set; }
        public string RefreshToken { get; set; } = string.Empty;
        public DateTime RefreshTokenExpiryTime { get; set; }
        public string CropDetails { get; set; } = string.Empty;
        public string NICFrontImg { get; set; } = string.Empty;
        public string NICBackImg { get; set; } = string.Empty;
        public string GSLetterImg { get; set; } = string.Empty;
    }
}