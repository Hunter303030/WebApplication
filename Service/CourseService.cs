using System.Collections;
using WebApplication.Dto.Course;
using WebApplication.Dto.Profile;
using WebApplication.Models;
using WebApplication.Repositories.Interfaces;
using WebApplication.Service.Interfase;

namespace WebApplication.Service
{
    public class CourseService : ICourseService
    {
        private readonly ICourseRepository _courseRepository;
        private readonly IWebHostEnvironment _appEnvironment;
        private readonly AutoMapper.IMapper _mapper;

        public CourseService(
                            ICourseRepository courseRepository,
                            IWebHostEnvironment appEnvironment,
                            AutoMapper.IMapper mapper)
        {
            _courseRepository = courseRepository;
            _appEnvironment = appEnvironment;
            _mapper = mapper;
        }

        public async Task<IEnumerable<Course>> ListAll()
        {
            var list = await _courseRepository.ListAll();

            if (list == null) return null;

            return list;
        }

        public async Task<IEnumerable<Course>> ListControl(Guid profileId)
        {
            var list = await _courseRepository.ListControl(profileId);

            if (list == null) return null;

            return list;
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

                    string fileName = courseId.ToString() + Path.GetExtension(courseAddDto.Preview.FileName);
                    string filePath = Path.Combine(folderPath, fileName);

                    Course newCourse = new Course
                    {
                        Id = courseId,
                        DateCreate = DateTime.Now,
                        DateUpdata = DateTime.Now,
                        PreviewUrl = $"/Course/{courseId}/" + fileName,
                        Status_Id = 1,
                        Profile_Id = profileId
                    };

                    _mapper.Map(courseAddDto, newCourse);

                    bool cheak = await _courseRepository.Add(newCourse);

                    if (cheak)
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

        public async Task<CourseEditDto> GetCourse(Guid courseId)
        {
            if (courseId == Guid.Empty) return null;

            var course = await _courseRepository.GetCourse(courseId);

            if (course == null) return null;

            var editCourse = _mapper.Map<CourseEditDto>(course);

            return editCourse;
        }

        public async Task<bool> Edit(CourseEditDto courseEditDto)
        {
            if (courseEditDto == null) return false;

            var getCourse = await _courseRepository.GetCourse(courseEditDto.Id);
            if (getCourse == null) return false;

            string newFilePath = "";
            string oldFilePath = Path.Combine(_appEnvironment.WebRootPath, getCourse.PreviewUrl.TrimStart('/'));

            if (courseEditDto.Preview != null)
            {
                string folderPath = Path.Combine(_appEnvironment.WebRootPath, "Course", courseEditDto.Id.ToString());

                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }
                
                string fileName = courseEditDto.Id.ToString() + Path.GetExtension(courseEditDto.Preview.FileName);
                newFilePath = Path.Combine(folderPath, fileName);
                
                courseEditDto.PreviewUrl = $"/Course/{courseEditDto.Id}/" + fileName;
                
                if (System.IO.File.Exists(oldFilePath))
                {
                    System.IO.File.Delete(oldFilePath);
                }
            }

            _mapper.Map(courseEditDto, getCourse);

            bool isSaved = await _courseRepository.Edit(getCourse);
            if (!isSaved) return false;

            if (!string.IsNullOrEmpty(newFilePath))
            {
                using (var fileStream = new FileStream(newFilePath, FileMode.Create))
                {
                    await courseEditDto.Preview.CopyToAsync(fileStream);
                }
            }

            return true;
        }

    }
}
