namespace Planday.Schedule.Response;

public class GetShiftResponse
{
    public GetShiftResponse(Shift shift, string mail)
    {
        Id = shift.Id;
        EmployeeId = shift.EmployeeId.Value;
        Email = mail;
        Start = shift.Start;
        End = shift.End;
    }

    public long Id { get; set; }  
    public long EmployeeId { get; set; }
    public string Email { get; set; }
    public DateTime Start { get; set; }
    public DateTime End { get; set; }  
}

