using TutorSystem.Domain.Dtos;
using TutorSytstem.Database.AppDbContextModels;

namespace TutorSystem.Domain.Features
{
    public class TeacherService
    {
        private readonly AppDbContext _db;

        public TeacherService(AppDbContext db)
        {
            _db = db;
        }

        public TeacherResponseDto GetTeachers(int? courseId = null, string? expertise = null)
        {
            var lst = _db.TblTeachers
                .Where(t => !t.IsDeleted &&
                            (courseId == null || t.CourseId == courseId) &&
                            (string.IsNullOrEmpty(expertise) || t.Expertise == expertise))
                .Select(t => new TeacherDto
                {
                    TeacherId = t.TeacherId,
                    TeacherName = t.TeacherName,
                    Expertise = t.Expertise,
                    Phone = t.Phone,
                    CourseId = t.CourseId, 
                    CreatedBy = t.CreatedBy,
                    CreatedDate = DateTime.Now,
                    ModifiedBy = t.ModifiedBy,
                    ModifiedDate = t.ModifiedDate,
                    IsDeleted = t.IsDeleted
                })
                .ToList();

            return new TeacherResponseDto
            {
                IsSuccess = true,
                Message = lst.Any() ? "Teachers retrieved successfully." : "No teachers found.",
                Teachers = lst
            };
        }

        public TeacherResponseDto CreateTeacher(TeacherCreateRequestDto dto)
        {
            if (string.IsNullOrEmpty(dto.TeacherName) || dto.CourseId == null)
                return new TeacherResponseDto
                {
                    IsSuccess = false,
                    Message = "TeacherName and CourseId are required."
                };

            bool exists = _db.TblTeachers
                .Any(t => t.TeacherName == dto.TeacherName && t.CourseId == dto.CourseId && !t.IsDeleted);

            if (exists)
                return new TeacherResponseDto
                {
                    IsSuccess = false,
                    Message = "Teacher already exists in this course."
                };

            var teacher = new TblTeacher
            {
                TeacherName = dto.TeacherName,
                Expertise = dto.Expertise,
                Phone = dto.Phone,
                CourseId = dto.CourseId, 
                CreatedBy = "System",
                CreatedDate = DateTime.Now,
                IsDeleted = false
            };

            _db.TblTeachers.Add(teacher);
            int result = _db.SaveChanges();

            return new TeacherResponseDto
            {
                IsSuccess = result > 0,
                Message = result > 0 ? "Teacher created successfully." : "Failed to create teacher."
            };
        }

        public TeacherResponseDto UpdateTeacher(int id, TeacherUpdateRequestDto dto)
        {
            var teacher = _db.TblTeachers.FirstOrDefault(t => t.TeacherId == id && !t.IsDeleted);
            if (teacher == null)
                return new TeacherResponseDto { IsSuccess = false, Message = "Teacher not found." };

            teacher.TeacherName = dto.TeacherName ?? teacher.TeacherName;
            teacher.Expertise = dto.Expertise ?? teacher.Expertise;
            teacher.Phone = dto.Phone ?? teacher.Phone;

            if (dto.CourseId.HasValue)
                teacher.CourseId = dto.CourseId.Value; // update CourseId

            teacher.ModifiedBy = "System";
            teacher.ModifiedDate = DateTime.Now;

            int result = _db.SaveChanges();

            return new TeacherResponseDto
            {
                IsSuccess = result > 0,
                Message = result > 0 ? "Teacher updated successfully." : "Failed to update teacher."
            };
        }

        public TeacherResponseDto DeleteTeacher(int id)
        {
            var teacher = _db.TblTeachers.FirstOrDefault(t => t.TeacherId == id && !t.IsDeleted);
            if (teacher == null)
                return new TeacherResponseDto { IsSuccess = false, Message = "Teacher not found." };

            teacher.IsDeleted = true;
            teacher.ModifiedDate = DateTime.Now;

            int result = _db.SaveChanges();

            return new TeacherResponseDto
            {
                IsSuccess = result > 0,
                Message = result > 0 ? "Teacher deleted successfully." : "Failed to delete teacher."
            };
        }
    }
}
