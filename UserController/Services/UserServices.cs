﻿using Microsoft.EntityFrameworkCore;
using UserController.data;
using UserController.Models.EntityModel;
using UserController.Models.RequestModel;
using UserController.Models.ResponseModel;

namespace UserController.Services
{
    public class UserServices : IUserServices
    {
        UserDbContext _db;

        public UserServices(UserDbContext db)
        {
            _db = db;
        }
        public async Task<RegisterResponseModel> CreateUser(RegisterModel model)
        {
            User user = new User
            {
                Username = model.Username,
                Email = model.Email,
                Password = model.Password,
                Role = "User",
                Standard = model.Standard,
                Roll = model.Roll,
                DOB = model.DOB
            };

            if (user != null)
            {
                _db.Users.Add(user);
                await _db.SaveChangesAsync();

            }
            return new RegisterResponseModel
            {

                Email = user.Email,
                Username = user.Username,
                Id = user.Id,
                Role = user.Role,
                Standard = user.Standard,
                Roll= user.Roll,
                DOB= user.DOB
            };
        }

        public async Task<User> GetById(int id)
        {
            User user = await _db.Users.FindAsync(id);
            if (user != null)
            {
                return user;
            }
            return null;
        }


        public async Task<bool> Deleteuser(int id)
        {
            User user = await _db.Users.FindAsync(id);

            if (user != null)
            {
                _db.Users.Remove(user);
                _db.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<List<User>> GetAllUsers()
        {
            List<User> allUsers = await _db.Users.ToListAsync();
            if (allUsers.Count > 0) { return allUsers; }
            return null;
        }

        public async Task<User> UpdateUser(User demoUser)
        {
            User _user = await _db.Users.FindAsync(demoUser.Id);
            if (_user == null)
            {
                return null;
            }

            if (demoUser != null)
            {
                if (demoUser.Username != _user.Username)
                {
                    _user.Username = demoUser.Username;
                }
                if (demoUser.Email != _user.Email)
                {
                    _user.Email = demoUser.Email;
                }
                if (demoUser.Password != _user.Password)
                {
                    _user.Password = demoUser.Password;
                }
                if(demoUser.DOB != demoUser.DOB)
                {
                    _user.DOB = demoUser.DOB;
                }
                if(demoUser.Standard !=  demoUser.Standard)
                {
                    _user.Standard = demoUser.Standard;
                }
                if(demoUser.Roll != demoUser.Roll)
                {
                    _user.Roll = demoUser.Roll;
                }

            }
            await _db.SaveChangesAsync();
            return _user;
        }

        public async Task<User> LoginUser(LoginModel model)
        {
            var _user = await _db.Users.ToListAsync();

            var user = _db.Users.FirstOrDefault(u => u.Email == model.Email && u.Password == model.Password);

            if(user != null)
            {
                return user;
            }
            return null;

            //foreach (var item in _user)
            //{
            //    if (item.Email == model.Email)
            //    {
            //        if (item.Password != model.Password)
            //        {
            //            return 2;
            //        }

            //    }
            //    else if (item.Password == model.Password)
            //    {
            //        if (item.Email != model.Email)
            //        {
            //            return 1;
            //        }
            //    }

            //}
            //return 3;
        }


    }
}