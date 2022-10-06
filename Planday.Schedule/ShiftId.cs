using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Planday.Schedule;

public record ShiftId(long Value)
{
    public static ShiftId New(string value)
    {
        if(long.TryParse(value, out long Value))
            return new ShiftId(Value);
        throw new ValidationException("ShiftId is invalid. Numerical values should be inserted");
    }
}
