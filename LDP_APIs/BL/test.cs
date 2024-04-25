namespace LDP_APIs.BL
{
    public class Student
    {
        public Student()
        {
        }
        public string FullName { get; set; } = "Not Set";
        public int StreamId { get; set; }
        public int Score { get; set; } = 0;
        public string Subjects { get; set; } = "All";
    }

    public class StudentInfo
    {
        public void test()
        {
            var studentsStream = new List<Student> {
    new Student { FullName = "Aruna", StreamId=1,  Score = 10 },
    new Student { FullName = "Janet", StreamId=2,  Score = 9  },
    new Student { FullName = "Ajay", StreamId=1,  Score = 11 },
    new Student { FullName = "Kunal", StreamId=2,  Score = 13  },
    new Student { FullName = "Chandra", StreamId=2,  Score = 8  },
    };

            var groupScores =
from techStream in studentsStream
group techStream by techStream.StreamId into studentGroup
select new
{
    Stream = studentGroup.Key,
    GroupScore = studentGroup.Sum(x => x.Score),
};
            foreach (var scr in groupScores)
            {
                Console.WriteLine(string.Format("{0}-{1}", scr.Stream, scr.GroupScore));
            }

        }
        
    }
}
