using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DependencyInversionDatabaseStaticAfter
{
    class DataNonStatic : IData
    {
        public IEnumerable<int> CourseIds()
        {
            return Data.CourseIds();
        }

        public IEnumerable<string> CourseNames()
        {
            return Data.CourseNames();
        }

        public string GetCourseById(int id)
        {
            return Data.GetCourseById(id);
        }

        public IEnumerable<string> Search(string substring)
        {
            return Data.Search(substring);
        }
    }
}
