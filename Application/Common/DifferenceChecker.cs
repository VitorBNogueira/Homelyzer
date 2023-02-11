using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common
{
    public static class DifferenceChecker
    {
        // some generics and reflection
        /// <summary>
        /// Checks two objects of the same type and compares their properties one by one until it detects a difference
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="original"></param>
        /// <param name="_new"></param>
        /// <returns>True if a difference was detected; False if no difference was detected</returns>
        public static bool IsDifferent<T>(T original, T _new)
        {
            List<string> PropsToIgnore = new List<string>
            {
                "Pictures",
                "Owner",
            };

            var props = typeof(T).GetProperties();
            foreach (var prop in props)
            {
                if (PropsToIgnore.Contains(prop.Name))
                    continue;

                if (!object.Equals(prop.GetValue(original), prop.GetValue(_new)))
                    return true;
            }

            return false;
        }
    }
}
