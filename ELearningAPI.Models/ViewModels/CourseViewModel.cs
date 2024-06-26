﻿

namespace ELearningAPI.Models.ViewModels
{
    public class CourseViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        public List<int>? CorrelatedCourseIds { get; set; }
        public List<CorrelatedCourseViewModel>? CorrelatedCourses { get; set; }  
        public List<LessonViewModel>? Lessons { get; set; }

    }
}
