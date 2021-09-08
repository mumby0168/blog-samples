using System;
using System.Text.Json;
using FluentAccountApi.Sample.Factories;
using FluentAccountApi.Sample.Models;

Console.WriteLine("Starting the account builder demo.");

Account account = AccountFactory
    .AccountBuilder()
    .WithEmailAddress("joe.bloggs@gmail.com")
    .WithPassword("Test123@")
    .VerifyPassword("Test123@")
    .WithRoles("admin", "super")
    .WithUsername("joe123")
    .Build();

Console.WriteLine("Built account ->");
Console.WriteLine(JsonSerializer.Serialize(account, new JsonSerializerOptions{WriteIndented = true}));