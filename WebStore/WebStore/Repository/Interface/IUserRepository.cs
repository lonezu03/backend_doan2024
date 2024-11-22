﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebStore.Entity;

namespace WebStore.Reposiroty.Interface
{
    public interface IUserRepository
    {
        Task AddAsync(Users user); // Thêm người dùng
        Task<Users> GetByEmailAsync(string email); // Lấy người dùng qua email

        Task<List<Users>> GetAllUsersAsync();
    }
}
