using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain;
public static class Extensions
{
    public static bool IsDefinedInEnum<T>(this string value)
    {
        return Enum.IsDefined(typeof(T), value);
    }
}
