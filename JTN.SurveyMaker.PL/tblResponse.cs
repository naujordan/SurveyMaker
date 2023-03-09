﻿using System;
using System.Collections.Generic;

namespace JTN.SurveyMaker.PL;

public partial class tblResponse
{
    public Guid Id { get; set; }

    public Guid QuestionId { get; set; }

    public Guid AnswerId { get; set; }

    public DateTime ResponseDate { get; set; }

    public virtual tblAnswer Answer { get; set; } = null!;

    public virtual tblQuestion Question { get; set; } = null!;
}
