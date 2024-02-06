using Microsoft.EntityFrameworkCore;
using TeacherWebApplication.Data;
using TeacherWebApplication.Models.EntityModels;
using TeacherWebApplication.Models.RequestModels;
using TeacherWebApplication.Models.ResponseModels;

namespace TeacherWebApplication.Services
{
    public class TeacherService : ITeacherService
    {
        TeacherDbContext _db;
        public TeacherService(TeacherDbContext db)
        {
            _db = db;
        }
        public async Task<TeacherResponseModel> CreateTeacher(TeacherRegisterModel trqm)
        {
            Teacher teacher = new Teacher()
            {
                Username = trqm.Username,
                Email = trqm.Email,
                FirstName = trqm.FirstName,
                LastName = trqm.LastName,
                Password = trqm.Password,
                PhoneNumber = trqm.PhoneNumber,
                Qualifications = trqm.Qualifications,
            };
            if (teacher != null)
            {
                await _db.TeacherTable.AddAsync(teacher);
                await _db.SaveChangesAsync();
            }

            return new TeacherResponseModel
            {
                Id= teacher.Id,
                Username = teacher.Username,
                Email = teacher.Email,
                FirstName = teacher.FirstName,
                LastName = teacher.LastName
            };

        }

        public async Task<bool> DeleteTeacher(int id)
        {
            Teacher teacher = await _db.TeacherTable.FindAsync(id);
            if (teacher != null)
            {
                _db.TeacherTable.Remove(teacher);
                _db.SaveChanges();
                return true;
            }
            return false;
        }

       

        public async Task<List<TeacherResponseModel>> GetAllTeacher()
        {
            List<Teacher> list = await _db.TeacherTable.ToListAsync();

           List<TeacherResponseModel> listModel = new List<TeacherResponseModel>();
            foreach(Teacher teacher in list)
            {
                listModel.Add(new TeacherResponseModel
                {
                    Id = teacher.Id,
                    Username = teacher.Username,
                    Email = teacher.Email,
                    FirstName = teacher.FirstName,
                    LastName = teacher.LastName

                });
            }
            if (list.Count > 0)
            {
                return listModel;
            }
            return null;
        }

        public async Task<TeacherResponseModel> GetTeacherById(int id)
        {
            Teacher teacher = await _db.TeacherTable.FindAsync(id);
            if (teacher != null)
            {
                return new TeacherResponseModel
                {

                    Username = teacher.Username,
                    Email = teacher.Email,
                    FirstName = teacher.FirstName,
                    LastName = teacher.LastName,
                    Id = teacher.Id
                };
            }
            return null;
        }

        public async Task<Teacher> LoginTeacher(TeacherLoginModel tlm)
        {
            var user = _db.TeacherTable.FirstOrDefault(u => u.Email == tlm.Email && u.Password == tlm.Password);
            if (user != null)
            {
                return user;
            }
            return null;
        }

        public async Task<Teacher> UpdateTeacher(Teacher trqm)
        {
            Teacher teacher = await _db.TeacherTable.FindAsync(trqm.Id);
            if(teacher == null)
            {
                return null;
            }
            if(trqm != null)
            {
                if(trqm.Username != teacher.Username)
                {
                    teacher.Username = trqm.Username;
                }
                if(trqm.Email != teacher.Email)
                {
                    teacher.Email = trqm.Email;
                }
                if(trqm.Password != teacher.Password)
                {
                    teacher.Password = trqm.Password;
                }
            }
            await _db.SaveChangesAsync();
            return teacher;

        }

        public Task<string> ForgetPassword(ForgetPasswordRequest forgetPassword)
        {
            
        }
    }
}
