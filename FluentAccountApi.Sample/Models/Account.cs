// Copyright (c) IEvangelist. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Collections.Generic;

namespace FluentAccountApi.Sample.Models
{
    public class Account
    {
        internal Account()
        {

        }

        public string Id { get; set; } = Guid.NewGuid().ToString();

        public string Email { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;

        public string? Username { get; set; }

        public List<string> Roles { get; set; } = new();
    }
}