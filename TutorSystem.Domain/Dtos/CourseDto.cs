namespace TutorSystem.Domain.Dtos
{
    public class CourseDto
    {
        public int CourseId { get; set; }
        public string CourseType { get; set; } = null!;  // "Government" or "International"
        public string? Description { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public bool IsDeleted { get; set; }
    }

    public class CourseCreateRequestDto
    {
        public string CourseType { get; set; } = null!;
        public string? Description { get; set; }
    }

    public class CourseUpdateRequestDto
    {
        public string? CourseType { get; set; }
        public string? Description { get; set; }
    }

    public class CourseResponseDto
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; } = null!;
        public List<CourseDto>? Courses { get; set; }
    }
}
