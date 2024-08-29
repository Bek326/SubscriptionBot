﻿using SubscriptionBot.Domain.Entities;

namespace SubscriptionBot.Domain.Interfaces;

public interface IUserRepository
{
    Task<User?> GetUserByIdAsync(int id);
    Task<IEnumerable<User?>> GetAllUsersAsync();
    Task AddUserAsync(User? user);
    Task UpdateUserAsync(User user);
    Task DeleteUserAsync(int id);
}