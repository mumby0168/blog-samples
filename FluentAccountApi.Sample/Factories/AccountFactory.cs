// Copyright (c) IEvangelist. All rights reserved.
// Licensed under the MIT License.

using FluentAccountApi.Sample.Builders;

namespace FluentAccountApi.Sample.Factories
{
    public static class AccountFactory
    {
        public static IAccountBuilderWithEmail AccountBuilder() => new AccountBuilder();
    }
}