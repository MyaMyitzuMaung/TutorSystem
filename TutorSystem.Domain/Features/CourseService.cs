using TutorSystem.Domain.Dtos;
using TutorSytstem.Database.AppDbContextModels;

namespace TutorSystem.Domain.Features
{
    public class CourseService
    {
        private readonly AppDbContext _db;

        public CourseService(AppDbContext db)
        {
            _db = db;
        }

        public CourseResponseDto GetCourses(string? courseType = null)
        {
            var list = _db.TblCourses
                .Where(c => !c.IsDeleted &&
                            (string.IsNullOrEmpty(courseType) || c.CourseType == courseType))
                .Select(c => new CourseDto
                {
                    CourseId = c.CourseId,
                    CourseType = c.CourseType,
                    Description = c.Description,
                    CreatedBy = c.CreatedBy,
                    CreatedDate = DateTime.Now,
                    ModifiedBy = c.ModifiedBy,
                    ModifiedDate = c.ModifiedDate,
                    IsDeleted = c.IsDeleted
                })
                .ToList();

            return new CourseResponseDto
            {
                IsSuccess = true,
                Message = list.Any() ? "Courses retrieved successfully." : "No courses found.",
                Courses = list
            };
        }

        public CourseResponseDto CreateCourse(CourseCreateRequestDto dto)
        {
            if (string.IsNullOrEmpty(dto.CourseType))
                return new CourseResponseDto { IsSuccess = false, Message = "CourseType is required." };

            bool exists = _db.TblCourses.Any(c => c.CourseType == dto.CourseType && !c.IsDeleted);
            if (exists)
                return new CourseResponseDto { IsSuccess = false, Message = "Course already exists." };

            var course = new TblCourse
            {
                CourseType = dto.CourseType,
                Description = dto.Description,
                CreatedBy = "System",
                CreatedDate = DateTime.Now,
                IsDeleted = false
            };

            _db.TblCourses.Add(course);
            _db.SaveChanges();

            return new CourseResponseDto
            {
                IsSuccess = true,
                Message = "Course created successfully."
            };
        }

        public CourseResponseDto UpdateCourse(int id, CourseUpdateRequestDto dto)
        {
            var course = _db.TblCourses.FirstOrDefault(c => c.CourseId == id && !c.IsDeleted);
            if (course == null)
                return new CourseResponseDto { IsSuccess = false, Message = "Course not found." };

            course.CourseType = dto.CourseType ?? course.CourseType;
            course.Description = dto.Description ?? course.Description;
            course.ModifiedBy = "System";
            course.ModifiedDate = DateTime.Now;

            _db.SaveChanges();

            return new CourseResponseDto
            {
                IsSuccess = true,
                Message = "Course updated successfully."
            };
        }

        public CourseResponseDto DeleteCourse(int id)
        {
            var course = _db.TblCourses.FirstOrDefault(c => c.CourseId == id && !c.IsDeleted);
            if (course == null)
                return new CourseResponseDto { IsSuccess = false, Message = "Course not found." };

            course.IsDeleted = true;
            course.ModifiedDate = DateTime.Now;

            _db.SaveChanges();

            return new CourseResponseDto
            {
                IsSuccess = true,
                Message = "Course deleted successfully."
            };
        }
    }
}
