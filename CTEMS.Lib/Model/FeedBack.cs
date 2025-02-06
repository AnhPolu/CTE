using System;

namespace CTEMS.Lib.Model;

public class FeedBack
{
    public FeedBack(){}
    public int Id { get; set; }
    public string Text { get; set; }
    public int Rate { get; set; }
    public long? StudentId { get; set; }
    public string StudentName { get; set; }
    public DateTime DateCreated { get; set; }
}
