using WebApplication.Dto.Lesson;
using WebApplication.Models;
using WebApplication.Repositories.Interfaces;
using WebApplication.Service.Interfase;

namespace WebApplication.Service
{
    public class LessonService: ILessonService
    {
        private readonly ILessonRepository _lessonRepository;
        private readonly IWebHostEnvironment _appEnvironment;
        private readonly AutoMapper.IMapper _mapper;

        public LessonService(ILessonRepository lessonRepository,
                             IWebHostEnvironment appEnvironment,
                             AutoMapper.IMapper mapper)
        {
            _lessonRepository = lessonRepository;
            _appEnvironment = appEnvironment;
            _mapper = mapper;
        }
        
        public async Task<LessonListDto> List(Guid courseId)
        {
            if (courseId == Guid.Empty) return null;

            var list = await _lessonRepository.List(courseId);

            LessonListDto lessonListDto = new LessonListDto()
            {
                CourseId = courseId,
                Lessons = list
            };

            return lessonListDto;
        }

        public async Task<bool> Add(LessonAddDto lessonAddDto)
        {
            if (lessonAddDto == null) return false;

            Guid lessonId = Guid.NewGuid();

            string folderPath = Path.Combine(_appEnvironment.WebRootPath, "Course", lessonAddDto.CourseId.ToString());

            string fileName = lessonId + Path.GetExtension(lessonAddDto.Content.FileName);

            string filePath = Path.Combine(folderPath, fileName);

            Lesson newLesson = new Lesson()
            {
                Id = lessonId,
                ContentUrl = $"/Course/{lessonAddDto.CourseId}/" + fileName,                
            };

            _mapper.Map(lessonAddDto, newLesson);

            bool isAdd = await _lessonRepository.Add(newLesson);

            if (isAdd)
            {
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await lessonAddDto.Content.CopyToAsync(fileStream);
                }
            }

            return isAdd;
        }
    }
}
