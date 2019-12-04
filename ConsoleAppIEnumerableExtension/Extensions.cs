using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleAppIEnumerableExtension
{
    public static class Extensions
    {
        /// <summary>
        /// used to calculate sum of colection elemens
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="numeralCollection"></param>
        /// <returns></returns>
        public static T Sum<T>(this IEnumerable<T> numeralCollection) where T : struct
        {
            T result = numeralCollection.FirstOrDefault();
            if(!result.Equals(default(T)))
            {
                foreach(var item in numeralCollection.Skip(1))
                {
                    result += (dynamic)item;
                }
            }
            return result;
        }
        /// <summary>
        /// used to calculate the product of every element in colection
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="numeralCollection"></param>
        /// <returns></returns>
        public static T Product<T>(this IEnumerable<T> numeralCollection) where T : struct
        {
            T result = numeralCollection.FirstOrDefault();
            if (!result.Equals(default(T)))
            {
                foreach (var item in numeralCollection.Skip(1))
                {
                    result *= (dynamic)item;
                }
            }
            return result;
        }
        /// <summary>
        /// used to calculate the minimum value from all elements
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="numeralCollection"></param>
        /// <returns></returns>
        public static T Min<T>(this IEnumerable<T> numeralCollection) where T : struct
        {
            T result = numeralCollection.FirstOrDefault();
            if (!result.Equals(default(T)))
            {
                foreach (var item in numeralCollection.Skip(1))
                {
                    if(result > (dynamic)item )
                    {
                        result = item;
                    }
                }
            }
            return result;
        }
        /// <summary>
        /// used to calculate max value from all elements in colection
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="numeralCollection"></param>
        /// <returns></returns>
        public static T Max<T>(this IEnumerable<T> numeralCollection) where T : struct
        {
            T result = numeralCollection.FirstOrDefault();
            if (!result.Equals(default(T)))
            {
                foreach (var item in numeralCollection.Skip(1))
                {
                    if(result < (dynamic)item)
                    {
                        result = item;
                    }
                }
            }
            return result;
        }
        /// <summary>
        /// used to calculate the average value for elements of colection
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="numeralCollection"></param>
        /// <returns></returns>
        public static T Average<T>(this IEnumerable<T> numeralCollection) where T : struct
        {
            T result = numeralCollection.FirstOrDefault();
            if (!result.Equals(default(T)))
            {
                int numberOfElements = 1;
                T sumOfElements = result;
                foreach (var item in numeralCollection.Skip(1))
                {
                    sumOfElements += (dynamic)item;
                    numberOfElements++;
                }
                result = (dynamic)sumOfElements / numberOfElements;
            }
            return result;
        }
    }
}
