﻿using System;
using System.Collections.Generic;

namespace JTN.SurveyMaker.PL;

public partial class tblQuestion
{
    public Guid Id { get; set; }

    public string Question { get; set; } = null!;

    public virtual ICollection<tblActivation> tblActivations { get; } = new List<tblActivation>();

    public virtual ICollection<tblQuestionAnswer> tblQuestionAnswers { get; } = new List<tblQuestionAnswer>();

    public virtual ICollection<tblResponse> tblResponses { get; } = new List<tblResponse>();
}
