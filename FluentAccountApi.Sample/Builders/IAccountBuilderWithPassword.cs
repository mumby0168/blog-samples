// Copyright (c) IEvangelist. All rights reserved.
// Licensed under the MIT License.

namespace FluentAccountApi.Sample.Builders
{
    public interface IAccountBuilderWithPassword
    {
        IAccountBuilderWithVerifiedPassword WithPassword(string password);
    }
}