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
            List<Teacher> list = _db.TeacherTable.ToList();
            

            foreach (Teacher t in list)
            {
                if(t.Name == trqm.Name)
                {
                    return null;
                }
            }


            Teacher teacher = new Teacher()
            {
                
                Email = trqm.Email,
                Name = trqm.Name,
                Password = trqm.Password,
                PhoneNumber = trqm.PhoneNumber,
                Standard = trqm.Standard,
                
            };
            if (teacher != null)
            {
                await _db.TeacherTable.AddAsync(teacher);
                await _db.SaveChangesAsync();
            }

            return new TeacherResponseModel
            {
                Id= teacher.Id,
                Name = teacher.Name,
                Email = teacher.Email,
                Standard = teacher.Standard,
                Role = teacher.Role,
                PhoneNumber= teacher.PhoneNumber,
            };

        }

        public async Task<bool> DeleteTeacher(int id)
        {
            Teacher teacher = await _db.TeacherTable.FindAsync(id);
            if (teacher != null)
            {
                _db.TeacherTable.Remove(teacher);
                await _db.SaveChangesAsync();
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
                  
                    Email = teacher.Email,
                    Name = teacher.Name,

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

                   
                    Email = teacher.Email,
                    Name = teacher.Name,
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
                if(trqm.Name != teacher.Name)
                {
                    teacher.Name = trqm.Name;
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

       
    }
}
