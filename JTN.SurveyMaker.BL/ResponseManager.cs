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
    public static class ResponseManager
    {
        public async static Task<int> Insert(Response response, bool rollback = false)
        {
            try
            {
                IDbContextTransaction transaction = null;

                using (SurveyMakerEntities dc = new SurveyMakerEntities())
                {
                    if (rollback) transaction = dc.Database.BeginTransaction();

                    tblResponse newrow = new tblResponse();
                    newrow.Id = Guid.NewGuid();
                    newrow.QuestionId = response.QuestionId;
                    newrow.AnswerId = response.AnswerId;
                    newrow.ResponseDate = DateTime.Now;

                    response.Id = newrow.Id;

                    dc.tblResponses.Add(newrow);
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

        public async static Task<List<Response>> Load(Guid questionId)
        {
            try
            {
                List<Response> responses = new List<Response>();
                await Task.Run(() =>
                {
                    using (SurveyMakerEntities dc = new SurveyMakerEntities())
                    {
                        var rows = (from r in dc.tblResponses
                                    join q in dc.tblQuestions
                                    on r.QuestionId equals q.Id
                                    where r.QuestionId == questionId
                                    select new
                                    {
                                        r.Id,
                                        QuestionId = q.Id,
                                        r.AnswerId,
                                        r.ResponseDate
                                    }).Distinct().ToList();
                        rows.ForEach(row => responses.Add(new Response
                        {
                            Id = row.Id,
                            QuestionId = row.QuestionId,
                            AnswerId = row.AnswerId,
                            ResponseDate = row.ResponseDate
                        }));
                    }

                });
                return responses;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
