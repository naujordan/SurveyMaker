﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JTN.SurveyMaker.BL.Models
{
    public class Question
    {
        public Guid Id { get; set; }
        public List<Answer> Answers { get; set; }
        public string Text { get; set; }
    }
}
