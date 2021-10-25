using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using To_Do_List_Library.Core.Application.Contracts.Persistence;
using To_Do_List_Library.Core.Application.Models.Settings;
using To_Do_List_Library.Core.Domain.Entities;
using To_Do_List_Library.Infrastructure.Persistence.Configuration;

namespace To_Do_List_Library.Infrastructure.Persistence.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        private readonly ToDoDbContext _toDoDbContext;
        private readonly AppSettings _appSettings;
        public UserRepository(ToDoDbContext toDoDbContext, IOptions<AppSettings> appSettings) : base(toDoDbContext)
        {
            _toDoDbContext = toDoDbContext;
            _appSettings = appSettings.Value;
        }

        public User EncryptPassword(User entity)
        {
            var newPassword = Convert.ToBase64String(KeyDerivation.Pbkdf2(
            password: entity.Password,
            salt: Encoding.ASCII.GetBytes(_appSettings.PasswordSalt),
            prf: KeyDerivationPrf.HMACSHA256,
            iterationCount: 100000,
            numBytesRequested: 256 / 8));

            entity.Password = newPassword;

            return entity;
        }

        public bool IsUserEmailUnique(string email)
        {
            var user = _toDoDbContext.User.Where(x => x.Email == email).FirstOrDefault();
            if(user == null)
            {
                return true;
            }
            return false;
        }

        public User LoginUser(User entity)
        {
            var user = _toDoDbContext.User.Where(x => x.Email == entity.Email && x.Password == entity.Password).FirstOrDefault();
            if(user == null)
            {
                throw new Exception("Incorrect Credentials");
            }
            return user;
        }
    }
}
