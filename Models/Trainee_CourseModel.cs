using System;
using System.Collections.Generic;

namespace Tranning.Models
{
    public class Trainee_courseModel
    {
        public List<Trainee_courseDetail> Trainee_CourseDetailLists { get; set; }
    }

    public class Trainee_courseDetail
    {
        public int trainee_id { get; set; }
        public int course_id { get; set; }
        // Other properties...
    


    public DateTime? created_at { get; set; }
        public DateTime? updated_at { get; set; }
        public DateTime? deleted_at { get; set; }
    }
}
