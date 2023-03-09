using System;
using System.Collections.Generic;

namespace JTN.SurveyMaker.PL;

public partial class tblAnswer
{
    public Guid Id { get; set; }

    public string Answer { get; set; } = null!;

    public virtual ICollection<tblQuestionAnswer> tblQuestionAnswers { get; } = new List<tblQuestionAnswer>();

    public virtual ICollection<tblResponse> tblResponses { get; } = new List<tblResponse>();
}
