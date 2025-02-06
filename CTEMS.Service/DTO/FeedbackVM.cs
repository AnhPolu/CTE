using System;

namespace CTEMS.Service.DTO;

public class FeedbackVM
{
    public string Text { get; set; }
    public int Rate { get; set; }
    public long? StudentId { get; set; }
    public string StudentName { get; set; }

}
