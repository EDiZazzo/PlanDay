using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Planday.Schedule;

public record EmployeeId(long Value)
{
    public static EmployeeId New(string value)
    {
        if (long.TryParse(value, out long Value))
            return new EmployeeId(Value);
        throw new ValidationException("EmployeeId is invalid. Numerical values should be inserted");
    }
}
