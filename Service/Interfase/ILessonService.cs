using WebApplication.Dto.Lesson;

namespace WebApplication.Service.Interfase
{
    public interface ILessonService
    {
        public Task<LessonListDto> List(Guid courseId);
        public Task<bool> Add(LessonAddDto lessonAddDto);
    }
}
