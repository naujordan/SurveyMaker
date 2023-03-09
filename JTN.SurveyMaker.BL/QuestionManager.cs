using JTN.SurveyMaker.BL.Models;
using JTN.SurveyMaker.PL;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JTN.SurveyMaker.BL
{
    public static class QuestionManager
    {
        public async static Task<int> Insert(Question question, bool rollback = false)
        {
            try
            {
                IDbContextTransaction transaction = null;

                using (SurveyMakerEntities dc = new SurveyMakerEntities())
                {
                    if (rollback) transaction = dc.Database.BeginTransaction();

                    tblQuestion newrow = new tblQuestion();
                    newrow.Id = Guid.NewGuid();
                    newrow.Question = question.Text;

                    question.Id = newrow.Id;

                    dc.tblQuestions.Add(newrow);
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

        public async static Task<int> Update(Question question, bool rollback = false)
        {
            try
            {
                IDbContextTransaction transaction = null;
                using (SurveyMakerEntities dc = new SurveyMakerEntities())
                {
                    tblQuestion row = dc.tblQuestions.FirstOrDefault(c => c.Id == question.Id);
                    int results = 0;
                    if (row != null)
                    {
                        if (rollback) transaction = dc.Database.BeginTransaction();
                        row.Question = question.Text;

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

        public async static Task<int> Delete(Guid id, bool rollback = false)
        {
            try
            {
                IDbContextTransaction transaction = null;
                using (SurveyMakerEntities dc = new SurveyMakerEntities())
                {

                    tblQuestion row = dc.tblQuestions.FirstOrDefault(c => c.Id == id);
                    int results = 0;
                    if (row != null)
                    {
                        if (rollback) transaction = dc.Database.BeginTransaction();

                        dc.tblQuestions.Remove(row);
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

        public async static Task<List<Question>> Load()
        {
            try
            {
                List<Question> questions = new List<Question>();
                await Task.Run(async () =>
                    {
                        using (SurveyMakerEntities dc = new SurveyMakerEntities())
                        {
                            foreach (tblQuestion q in dc.tblQuestions.ToList())
                            {
                                Question question = new Question { Id = q.Id, Text = q.Question };

                                question.Answers = new List<Answer>();
                                foreach (tblQuestionAnswer qa in q.tblQuestionAnswers.ToList()) 
                                {
                                    Answer answer = new Answer
                                    {
                                        Id = qa.AnswerId, isCorrect = qa.isCorrect, Text = qa.Answer.Answer
                                    };
                                    question.Answers.Add(answer);
                                }
                                question.Activations = new List<Activation>();
                                foreach (tblActivation a in q.tblActivations.ToList())
                                {
                                    Activation activation = new Activation
                                    {
                                        Id = a.Id, QuestionId = a.QuestionId, ActivationCode= a.ActivationCode, EndDate= a.EndDate, StartDate = a.StartDate
                                    };
                                    question.Activations.Add(activation);
                                }
                                questions.Add(question);
                            }
                        }
                    });
                return questions;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async static Task<Question> LoadById(Guid id)
        {
            try
            {
                using (SurveyMakerEntities dc = new SurveyMakerEntities())
                {
                    tblQuestion tblQuestion = dc.tblQuestions.Where(c => c.Id == id).FirstOrDefault();
                    Question question = new Question();

                    if (tblQuestion != null)
                    {
                        // Put the table row values into the object.
                        question.Id = tblQuestion.Id;
                        question.Text = tblQuestion.Question;
                        question.Answers = new List<Answer>();
                        foreach (tblQuestionAnswer qa in tblQuestion.tblQuestionAnswers.ToList())
                        {
                            Answer answer = new Answer
                            {
                                Id = qa.AnswerId,
                                isCorrect = qa.isCorrect,
                                Text = qa.Answer.Answer
                            };
                            question.Answers.Add(answer);
                        }
                        question.Activations = new List<Activation>();
                        foreach (tblActivation a in tblQuestion.tblActivations.ToList())
                        {
                            Activation activation = new Activation
                            {
                                Id = a.Id,
                                QuestionId = question.Id,
                                ActivationCode = a.ActivationCode,
                                EndDate = a.EndDate,
                                StartDate = a.StartDate
                            };
                            question.Activations.Add(activation);
                        }
                        return question;
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
