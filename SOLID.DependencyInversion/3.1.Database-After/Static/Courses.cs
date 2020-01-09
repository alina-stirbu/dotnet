namespace DependencyInversionDatabaseStaticAfter
{
    public class Courses
    {
        private readonly IData data;
        public Courses(IData d)
        {
            data = d;
        }

        public void PrintAll()
        {
            var courses = data.CourseNames();

            // print courses
        }

        public void PrintIds()
        {
            var courses = data.CourseIds();

            // print courses
        }

        public void PrintById(int id)
        {
            var courses = data.GetCourseById(id);

            // print courses
        }

        public void Search(string substring)
        {
            var courses = data.Search(substring);

            // print courses
        }
    }
}
