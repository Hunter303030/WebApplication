﻿using WebApplication.Dto.Course;
using WebApplication.Models;

namespace WebApplication.Repositories.Interfaces
{
    public interface ICourseRepository
    {
        public Task<IEnumerable<Course>> ListAll();
        public Task<IEnumerable<Course>> ListControl(Guid profileId);
        public Task<bool> Add(Course course);
        public Task<bool> Edit(Course course);
        public Task<Course> GetCourse(Guid courseId);
    }
}
