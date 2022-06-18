﻿using System;

namespace BTech.ExpenseSystem.Domain.Entities
{
    public sealed class User
    {
        public string Id { get; set; } = null!;

        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public string Currency { get; set; } = null!;
    }
}