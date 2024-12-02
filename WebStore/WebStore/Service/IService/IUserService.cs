using Microsoft.AspNetCore.Identity.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebStore.DTO;
using WebStore.Entity;

namespace WebStore.Service.IService
{
    public interface IUserService
    {
        Task<bool> RegisterAsync(string username, string email, string password);

        Task<LoginResponse> LoginAsync(string email, string password);
        Task<bool> UserExists(string email);
        public int GetCurrentUserId();

        Task<List<UserDto>> GetAllUsersAsync();

    }
}

