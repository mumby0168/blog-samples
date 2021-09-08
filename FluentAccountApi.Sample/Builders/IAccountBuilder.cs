// Copyright (c) IEvangelist. All rights reserved.
// Licensed under the MIT License.

using System.Collections.Generic;
using FluentAccountApi.Sample.Models;

namespace FluentAccountApi.Sample.Builders
{
    public interface IAccountBuilder
    {
        IAccountBuilder WithUsername(string username);

        IAccountBuilder WithRole(string role);

        IAccountBuilder WithRoles(params string[] roles);

        IAccountBuilder WithRoles(IEnumerable<string> roles);

        Account Build();
    }
}