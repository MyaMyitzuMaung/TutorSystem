namespace TutorSystem.Domain.Dtos
{
    public class StudentLevelDto
    {
        public int LevelId { get; set; }
        public int CourseId { get; set; }   // "Government" or "International"
        public string LevelName { get; set; } = null!;    // e.g., "Grade 1", "IGCSE"
        public string? CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public bool IsDeleted { get; set; }
    }

    public class StudentLevelCreateRequestDto
    {
        public int CourseId { get; set; }
        public string LevelName { get; set; } = null!;
    }

    public class StudentLevelUpdateRequestDto
    {
        public int? CourseId { get; set; }
        public string? LevelName { get; set; }
    }

    public class StudentLevelResponseDto
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; } = null!;
        public List<StudentLevelDto>? StudentLevels { get; set; }
    }
}
