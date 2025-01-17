﻿
using System.ComponentModel.DataAnnotations;

namespace TechChallenge1.Core.DTO
{
    public class ContactDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public StateDto State { get; set; }
    }
}