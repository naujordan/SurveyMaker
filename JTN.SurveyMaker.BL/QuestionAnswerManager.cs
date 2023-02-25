using JTN.SurveyMaker.PL;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JTN.SurveyMaker.BL
{
    public class QuestionAnswerManager
    {
        public async static Task<int> Insert(Guid questionId,
                                             Guid answerId,
                                             bool isCorrect,
                                             bool rollback = false)
        {
            try
            {
                IDbContextTransaction transaction = null;

                using (SurveyMakerEntities dc = new SurveyMakerEntities())
                {
                    if (rollback) transaction = dc.Database.BeginTransaction();

                    tblQuestionAnswer newrow = new tblQuestionAnswer();
                    newrow.Id = Guid.NewGuid();
                    newrow.QuestionId = questionId;
                    newrow.AnswerId = answerId;
                    newrow.isCorrect = isCorrect;

                    dc.tblQuestionAnswers.Add(newrow);
                    int results = dc.SaveChanges();

                    if (rollback) transaction.Rollback();

                    return results;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async static Task<int> Delete(Guid questionId, Guid answerId, bool rollback = false)
        {
            try
            {
                IDbContextTransaction transaction = null;
                using (SurveyMakerEntities dc = new SurveyMakerEntities())
                {

                    tblQuestionAnswer row = dc.tblQuestionAnswers.FirstOrDefault(c => c.QuestionId == questionId && c.AnswerId == answerId);
                    int results = 0;
                    if (row != null)
                    {
                        if (rollback) transaction = dc.Database.BeginTransaction();

                        dc.tblQuestionAnswers.Remove(row);
                        results = dc.SaveChanges();
                        if (rollback) transaction.Rollback();

                    }
                    else
                    {
                        throw new Exception("Row was not found.");
                    }
                    return results;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
