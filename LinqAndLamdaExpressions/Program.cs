namespace LinqAndLamdaExpressions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using Models;

    internal class Program
    {
        private static void Main(string[] args)
        {
            List<User> allUsers = ReadUsers("users.json");
            List<Post> allPosts = ReadPosts("posts.json");

            #region Demo

            // 1 - find all users having email ending with ".net".
            var users1 = from user in allUsers
                         where user.Email.EndsWith(".net")
                         select user;

            var users11 = allUsers.Where(user => user.Email.EndsWith(".net"));

            IEnumerable<string> userNames = from user in allUsers
                                            select user.Name;

            var userNames2 = allUsers.Select(user => user.Name);

            foreach (var value in userNames2)
            {
                Console.WriteLine(value);
            }

            IEnumerable<Company> allCompanies = from user in allUsers
                                                select user.Company;

            var users2 = from user in allUsers
                         orderby user.Email
                         select user;

            var netUser = allUsers.First(user => user.Email.Contains(".net"));
            Console.WriteLine(netUser.Username);

            #endregion

            // 2 - find all posts for users having email ending with ".net".
            IEnumerable<int> usersIdsWithDotNetMails = from user in allUsers
                                                       where user.Email.EndsWith(".net")
                                                       select user.Id;

            IEnumerable<Post> posts = from post in allPosts
                                      where usersIdsWithDotNetMails.Contains(post.UserId)
                                      select post;

            foreach (var post in posts)
            {
                Console.WriteLine(post.Id + " " + "user: " + post.UserId);
            }

            // 3 - print number of posts for each user.
            foreach (var x in (from post in allPosts
                               group post by post.UserId)
                               .OrderByDescending(p => p.Count())
                               .Select(g => (UserId: g.Key, NumberOfPosts: g.Count())))
            {
                Console.WriteLine("User {0} has {1} posts", x.UserId, x.NumberOfPosts);
            }

            //3 var 2
            var result = allPosts.GroupBy(p => p.UserId)
                .OrderByDescending(p => p.Count())
                .Select(
                    g => (
                        UserId: g.Key,
                        NumberOfPosts: g.Count()
                    )
                );

            foreach (var x in result)
            {
                Console.WriteLine("User {0} has {1} posts",x.UserId,x.NumberOfPosts);
            }


            // 4 - find all users that have lat and long negative. attention to null values of lat and long
            var negativeLatitudeUsers = allUsers.Find(u => u.Address.Geo.Lat < 0 && u.Address.Geo.Lng < 0);

            // 5 - find the post with longest body.
            int maxLengthPost = allPosts.Max(p => p.Body.Length);

            Post longPost = allPosts.First(p => p.Body.Length == maxLengthPost);

            // 6 - print the name of the employee that have post with longest body.
            string employeeName = allUsers.Find(u => u.Id.Equals(longPost.UserId)).Name;
            Console.WriteLine("User {0} has the longest post with {1} charachers", employeeName, maxLengthPost);

            //using joins
            var employeeNameV2 = from post in allPosts
                                 join users in allUsers on post.UserId equals users.Id
                                 where post.Body.Length.Equals(maxLengthPost)
                                 select users.Name;
            foreach (var x in employeeNameV2)
            {
                Console.WriteLine("V2 User {0} has the logest post with {1} charachers", x, maxLengthPost);
            }

            // 7 - select all addresses in a new List<Address>. print the list.
            List<Address> allAddresses = new List<Address>();
            allAddresses = allUsers.Select(p => p.Address).ToList();

            Console.WriteLine("Address List");
            foreach (Address a in allAddresses)
            {
                PropertyInfo[] propInfo;
                propInfo = a.GetType().GetProperties();

                foreach (PropertyInfo p in propInfo)
                {
                    Console.WriteLine(" Address {0} = {1}", p.Name, p.GetValue(a));
                }
            }

            // 8 - print the user with min lat
            double minLat = allUsers.Min(p => p.Address.Geo.Lat);
            User userMinLat = allUsers.First(u => u.Address.Geo.Lat == minLat);
            Console.WriteLine(" User with min lat is {0} ", userMinLat.Name);

            // 9 - print the user with max long
            double maxLong = allUsers.Max(p => p.Address.Geo.Lng);
            User userMaxLong = allUsers.First(u => u.Address.Geo.Lng == maxLong);
            Console.WriteLine(" User with max long is {0} ", userMaxLong.Name);

            // 10 - create a new class: public class UserPosts { public User User {get; set}; public List<Post> Posts {get; set} }
            //    - create a new list: List<UserPosts>
            //    - insert in this list each user with his posts only

            List<UserPosts> userPostsList = new List<UserPosts>();
            for (int i = 0; i < allUsers.Count; i++)
            {
                User u = allUsers[i];
                UserPosts up = new UserPosts();
                up.User = u;
                List<Post> currentPostList = allPosts.Where(p => p.UserId == u.Id).ToList();
                up.Posts = currentPostList;
                userPostsList.Add(up);
            }
            

            // 11 - order users by zip code
            allUsers.OrderBy(p => p.Address.Zipcode);

            // 12 - order users by number of posts
            IOrderedEnumerable<User> orderedEnumerable = allUsers.OrderBy(p => allPosts.Where(q => q.UserId == p.Id).Count());

        }

        private static List<Post> ReadPosts(string file)
        {
            return ReadData.ReadFrom<Post>(file);
        }

        private static List<User> ReadUsers(string file)
        {
            return ReadData.ReadFrom<User>(file);
        }
    }
}
