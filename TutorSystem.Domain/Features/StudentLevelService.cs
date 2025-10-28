using TutorSystem.Domain.Dtos;
using TutorSytstem.Database.AppDbContextModels;

namespace TutorSystem.Domain.Features
{
    public class StudentLevelService
    {
        private readonly AppDbContext _db;

        public StudentLevelService(AppDbContext db)
        {
            _db = db;
        }

        public StudentLevelResponseDto GetLevels(int? courseId = null)
        {
            var lst = _db.TblStudentLevels
                .Where(l => !l.IsDeleted &&
                            (courseId == null || l.CourseId == courseId))
                .Select(l => new StudentLevelDto
                {
                    LevelId = l.LevelId,
                    CourseId = l.CourseId,
                    LevelName = l.LevelName,
                    CreatedBy = l.CreatedBy,
                    CreatedDate = l.CreatedDate ?? DateTime.Now,
                    ModifiedBy = l.ModifiedBy,
                    ModifiedDate = l.ModifiedDate,
                    IsDeleted = l.IsDeleted
                })
                .ToList();

            return new StudentLevelResponseDto
            {
                IsSuccess = true,
                Message = lst.Any() ? "Student levels retrieved successfully." : "No student levels found.",
                StudentLevels = lst
            };
        }

        public StudentLevelResponseDto CreateLevel(StudentLevelCreateRequestDto dto)
        {
            if (dto.CourseId <= 0 || string.IsNullOrEmpty(dto.LevelName))
                return new StudentLevelResponseDto
                {
                    IsSuccess = false,
                    Message = "CourseId and LevelName are required."
                };

            bool exists = _db.TblStudentLevels.Any(l =>
                l.CourseId == dto.CourseId &&
                l.LevelName == dto.LevelName &&
                !l.IsDeleted);

            if (exists)
                return new StudentLevelResponseDto
                {
                    IsSuccess = false,
                    Message = "Student level already exists for this course."
                };

            var level = new TblStudentLevel
            {
                CourseId = dto.CourseId,
                LevelName = dto.LevelName,
                CreatedBy = "System",
                CreatedDate = DateTime.Now,
                IsDeleted = false
            };

            _db.TblStudentLevels.Add(level);
            _db.SaveChanges();

            return new StudentLevelResponseDto
            {
                IsSuccess = true,
                Message = "Student level created successfully."
            };
        }

        public StudentLevelResponseDto UpdateLevel(int id, StudentLevelUpdateRequestDto dto)
        {
            var level = _db.TblStudentLevels.FirstOrDefault(l => l.LevelId == id && !l.IsDeleted);
            if (level == null)
                return new StudentLevelResponseDto { IsSuccess = false, Message = "Student level not found." };

            if (dto.CourseId.HasValue)
                level.CourseId = dto.CourseId.Value;

            level.LevelName = dto.LevelName ?? level.LevelName;
            level.ModifiedBy = "System";
            level.ModifiedDate = DateTime.Now;

            _db.SaveChanges();

            return new StudentLevelResponseDto
            {
                IsSuccess = true,
                Message = "Student level updated successfully."
            };
        }

        public StudentLevelResponseDto DeleteLevel(int id)
        {
            var level = _db.TblStudentLevels.FirstOrDefault(l => l.LevelId == id && !l.IsDeleted);
            if (level == null)
                return new StudentLevelResponseDto { IsSuccess = false, Message = "Student level not found." };

            level.IsDeleted = true;
            level.ModifiedDate = DateTime.Now;

            _db.SaveChanges();

            return new StudentLevelResponseDto
            {
                IsSuccess = true,
                Message = "Student level deleted successfully."
            };
        }
    }
}
