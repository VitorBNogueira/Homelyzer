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
        public static bool IsDifferent<T>(T original, T _new)
        {
            List<string> PropsToIgnore = new List<string>
            {
                "Pictures",
                "Owner"
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
