using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JTN.SurveyMaker.BL.Models
{
    public class Answer
    {
        public Guid Id { get; set; }
        public bool isCorrect { get; set; }
        public string Text { get; set; }
    }
}
