using System;
using System.Collections.Generic;
using FluentAccountApi.Sample.Models;

namespace FluentAccountApi.Sample.Builders
{
    class AccountBuilder :
        IAccountBuilderWithEmail,
        IAccountBuilderWithPassword,
        IAccountBuilderWithVerifiedPassword,
        IAccountBuilder
    {
        readonly Account _account = new();

        public IAccountBuilderWithPassword WithEmailAddress(string email)
        {
            _account.Email = email;
            return this;
        }

        public IAccountBuilderWithVerifiedPassword WithPassword(string password)
        {
            if (password.Length < 6)
                throw new InvalidOperationException("A password must be at least 6 characters.");

            if (password.Contains('@') is false && password.Contains('$') is false)
                throw new InvalidOperationException("A password must contain one of the special characters '@$'.");

            _account.Password = password;
            return this;
        }

        public IAccountBuilder VerifyPassword(string password)
        {
            if (_account.Password != password)
                throw new InvalidOperationException("The passwords provided do not match.");

            return this;
        }

        public IAccountBuilder WithUsername(string username)
        {
            _account.Username = username;
            return this;
        }

        public IAccountBuilder WithRole(string role)
        {
            _account.Roles.Add(role);
            return this;
        }

        public IAccountBuilder WithRoles(params string[] roles)
        {
            _account.Roles.AddRange(roles);
            return this;
        }

        public IAccountBuilder WithRoles(IEnumerable<string> roles)
        {
            _account.Roles.AddRange(roles);
            return this;
        }

        public Account Build() => _account;
    }
}