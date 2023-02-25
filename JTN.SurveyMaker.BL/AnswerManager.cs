using JTN.SurveyMaker.BL.Models;
using JTN.SurveyMaker.PL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JTN.SurveyMaker.BL
{
    public static class AnswerManager
    {
        public async static Task<int> Delete(Guid id, bool rollback = false)
        {
            try
            {
                IDbContextTransaction transaction = null;
                using (SurveyMakerEntities dc = new SurveyMakerEntities())
                {

                    tblAnswer row = dc.tblAnswers.FirstOrDefault(c => c.Id == id);
                    int results = 0;
                    if (row != null)
                    {
                        if (rollback) transaction = dc.Database.BeginTransaction();

                        dc.tblAnswers.Remove(row);
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

        public async static Task<int> Update(Answer answer, bool rollback = false)
        {
            try
            {
                IDbContextTransaction transaction = null;
                using (SurveyMakerEntities dc = new SurveyMakerEntities())
                {
                    tblAnswer row = dc.tblAnswers.FirstOrDefault(c => c.Id == answer.Id);
                    int results = 0;
                    if (row != null)
                    {
                        if (rollback) transaction = dc.Database.BeginTransaction();
                        row.Answer = answer.Text;


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

        public async static Task<List<Answer>> Load()
        {
            try
            {
                List<Answer> answers = new List<Answer>();
                using (SurveyMakerEntities dc = new SurveyMakerEntities())
                {
                    dc.tblAnswers
                        .ToList()
                        .ForEach(c => answers.Add(new Answer
                        {
                            Id = c.Id,
                            Text = c.Answer
                        }));
                }
                
                return answers;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async static Task<bool> Insert(Answer answer, bool rollback = false)
        {
            try
            {
                IDbContextTransaction transaction = null;

                using (SurveyMakerEntities dc = new SurveyMakerEntities())
                {
                    if (rollback) transaction = dc.Database.BeginTransaction();

                    tblAnswer newrow = new tblAnswer();
                    newrow.Id = Guid.NewGuid();
                    newrow.Answer = answer.Text;

                    answer.Id = newrow.Id;

                    dc.tblAnswers.Add(newrow);
                    int results = dc.SaveChanges();

                    if (rollback) transaction.Rollback();

                    return true;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async static Task<List<Answer>> Load(Guid questionId)
        {
            try
            {
                List<Answer> answers = new List<Answer>();
                await Task.Run(() =>
                {
                    using (SurveyMakerEntities dc = new SurveyMakerEntities())
                    {
                        var rows = (from a in dc.tblAnswers
                                   join qa in dc.tblQuestionAnswers
                                   on a.Id equals qa.AnswerId
                                   where qa.QuestionId == questionId
                                   select new
                                   {
                                       a.Id,
                                       a.Answer,
                                       qa.isCorrect
                                   }).Distinct().ToList();
                        rows.ForEach(row => answers.Add(new Answer
                        {
                            Id = row.Id,
                            Text = row.Answer,
                            isCorrect = row.isCorrect
                        }));
                    }
                    
                });
                return answers;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async static Task<Answer> LoadById(Guid id)
        {
            try
            {
                using (SurveyMakerEntities dc = new SurveyMakerEntities())
                {
                    tblAnswer tblAnswer = dc.tblAnswers.Where(c => c.Id == id).FirstOrDefault();
                    Answer answer = new Answer();

                    if (tblAnswer != null)
                    {
                        answer.Id = tblAnswer.Id;
                        answer.Text = tblAnswer.Answer;
                        return answer;
                    }
                    else
                    {
                        throw new Exception("Could not find the row");
                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
