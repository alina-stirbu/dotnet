namespace DependencyInversionDatabaseAfter
{
    public class Courses
    {
        IData database;

        public Courses(IData data)
        {
            this.database = data;
        }
        public void PrintAll()
        {
            var courses = database.CourseNames();

            // print courses
        }

        public void PrintIds()
        {
            var courses = database.CourseIds();

            // print courses
        }

        public void PrintById(int id)
        {
            var courses = database.GetCourseById(id);

            // print courses
        }

        public void Search(string substring)
        {
            var courses = database.Search(substring);

            // print courses
        }
    }
}
