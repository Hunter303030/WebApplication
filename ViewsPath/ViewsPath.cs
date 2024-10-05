﻿namespace WebApplication.ViewsPath
{
    public static class ViewPaths
    {
        public static class Course
        {
            public const string ListControl = "~/Views/Course/ListControl.cshtml";
            public const string ListAll = "~/Views/Course/ListAll.cshtml";
            public const string Add = "~/Views/Course/Add.cshtml";
            public const string Edit = "~/Views/Course/Edit.cshtml";
        }

        public static class Profile
        {
            public const string Auth = "~/Views/Profile/Auth.cshtml";
            public const string Edit = "~/Views/Profile/Edit.cshtml";
            public const string EditPassword = "~/Views/Profile/EditPassword.cshtml";
            public const string Register = "~/Views/Profile/Register.cshtml";
        }

        public static class Lesson
        {
            public const string ListContor = "~/Views/Lesson/ListControl.cshtml"; 
        }

        public static class Error
        {
            public const string NotFound = "~/Views/Shared/_NotFound.cshtml";
        }
    }
}
