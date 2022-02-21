using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using WebAPI_Payments.Interfaces;
using WebAPI_Payments.Models;

namespace WebAPI_Payments.Data.Repo
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext dc;

        public UserRepository(DataContext dc)
        {
            this.dc = dc;
        }
        public async Task<User> Authenticate(string email, string password)
        {
            var user = await dc.Users.FirstOrDefaultAsync(x => x.Email == email);

            if (user == null || user.PasswordKey == null)
                return null;
            if (!MatchPasswordHash(password, user.Password, user.PasswordKey))
                return null;
            return user;
        }

        private bool MatchPasswordHash(string passwordText, byte[] password, byte[] passwordKey)
        {
            using(var hmac = new HMACSHA512(passwordKey))
            {
                var passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(passwordText));
                for(int i = 0; i < passwordHash.Length; i++)
                {
                    if (passwordHash[i] != password[i])
                        return false;
                }

                return true;
            }
        }

        public void Register(string name, string lastname, int phoneNumber, string email, string password)
        {
            byte[] passwordHash, passwordKey;

            using(var hmac = new HMACSHA512())
            {
                passwordKey = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }

            User user = new User();
            user.Name = name;
            user.Lastname = lastname;
            user.PhoneNumber = phoneNumber;
            user.Email = email;
            user.Password = passwordHash;
            user.PasswordKey = passwordKey;
            user.Budget = 0;
            user.IsVerified = false;

            dc.Users.Add(user);
        }

        public async Task<bool> UserAlreadyExists(string email)
        {
            return await dc.Users.AnyAsync(x => x.Email == email);
        }

        public async Task<User> FindUser(int userId)
        {
            return await dc.Users.FindAsync(userId);
        }

        public async Task<User> FindUserByEmail(string email)
        {
            User user = await dc.Users.FirstOrDefaultAsync(item => item.Email == email);
            
            return user;
        }
    }
}
