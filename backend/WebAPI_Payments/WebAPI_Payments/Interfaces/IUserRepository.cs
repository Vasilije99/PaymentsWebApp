using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI_Payments.Models;

namespace WebAPI_Payments.Interfaces
{
    public interface IUserRepository
    {
        Task<User> Authenticate(string email, string password);
        void Register(string name, string lastname, int phoneNumber, string email, string password);
        Task<User> FindUser(int userId);
        Task<User> FindUserByEmail(string email);
        Task<bool> UserAlreadyExists(string email);
    }
}
