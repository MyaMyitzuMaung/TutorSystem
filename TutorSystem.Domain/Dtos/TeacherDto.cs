namespace TutorSystem.Domain.Dtos
{
    public class TeacherDto
    {
        public int TeacherId { get; set; }
        public string TeacherName { get; set; } = null!;
        public string? Expertise { get; set; }
        public string? Phone { get; set; }
        public int CourseId { get; set; }  // "Government" or "International"
        public string? CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public bool IsDeleted { get; set; }
    }

    public class TeacherCreateRequestDto
    {
        public string TeacherName { get; set; } = null!;
        public string? Expertise { get; set; }
        public string? Phone { get; set; }
        public int CourseId { get; set; } 
    }

    public class TeacherUpdateRequestDto
    {
        public string? TeacherName { get; set; }
        public string? Expertise { get; set; }
        public string? Phone { get; set; }
        public int? CourseId { get; set; }
    }

    public class TeacherResponseDto
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; } = null!;
        public List<TeacherDto>? Teachers { get; set; }
    }
}
