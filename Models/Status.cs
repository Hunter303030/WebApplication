﻿namespace WebApplication.Models
{
    public class Status
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public IEnumerable<Course> Courses { get; set; }
    }
}
