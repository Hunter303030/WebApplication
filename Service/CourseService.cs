using WebApplication.Dto.Course;
using WebApplication.Models;
using WebApplication.Repositories.Interfaces;
using WebApplication.Service.Interfase;

namespace WebApplication.Service
{
    public class CourseService : ICourseService
    {
        private readonly ICourseRepository _courseRepository;
        private readonly IWebHostEnvironment _appEnvironment;

        public CourseService(ICourseRepository courseRepository, IWebHostEnvironment appEnvironment)
        {
            _courseRepository = courseRepository;
            _appEnvironment = appEnvironment;
        }

        public async Task<bool> Add(CourseAddDto courseAddDto, Guid profileId)
        {
            if (courseAddDto != null && profileId != Guid.Empty)
            {
                if (courseAddDto.Preview != null)
                {
                    Guid courseId = Guid.NewGuid();
                    string folderPath = Path.Combine(_appEnvironment.WebRootPath, "Course", courseId.ToString());

                    if (!Directory.Exists(folderPath))
                    {
                        Directory.CreateDirectory(folderPath);
                    }

                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(courseAddDto.Preview.FileName);
                    string filePath = Path.Combine(folderPath, fileName);                    

                    Course newCourse = new Course
                    {
                        Id = courseId,
                        Title = courseAddDto.Title,
                        Description = courseAddDto.Description,
                        Price = courseAddDto.Price,
                        DateCreate = DateTime.Now,
                        DateUpdata = DateTime.Now,
                        PreviewUrl = $"/Course/{courseId}/" + fileName,
                        Status_Id = 1,
                        Profile_Id = profileId
                    };
                    
                    bool cheak = await _courseRepository.Add(newCourse);

                    if(cheak)
                    {
                        using (var fileStream = new FileStream(filePath, FileMode.Create))
                        {
                            await courseAddDto.Preview.CopyToAsync(fileStream);
                        }
                    }

                    return true;
                }
            }
            return false;
        }
    }
}
