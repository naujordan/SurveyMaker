using JTN.SurveyMaker.BL.Models;
using JTN.SurveyMaker.PL;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JTN.SurveyMaker.BL
{
    public static class ActivationManager
    {
        public async static Task<int> Insert(Activation activation, bool rollback = false)
        {
            try
            {
                IDbContextTransaction transaction = null;

                using (SurveyMakerEntities dc = new SurveyMakerEntities())
                {
                    if (rollback) transaction = dc.Database.BeginTransaction();

                    tblActivation newrow = new tblActivation();
                    newrow.Id = Guid.NewGuid();
                    newrow.QuestionId = activation.QuestionId;
                    newrow.StartDate = activation.StartDate;
                    newrow.EndDate = activation.EndDate;
                    newrow.ActivationCode = activation.ActivationCode;

                    activation.Id = newrow.Id;

                    dc.tblActivations.Add(newrow);
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

        public async static Task<int> Update(Activation activation, bool rollback = false)
        {
            try
            {
                IDbContextTransaction transaction = null;
                using (SurveyMakerEntities dc = new SurveyMakerEntities())
                {
                    tblActivation row = dc.tblActivations.FirstOrDefault(c => c.Id == activation.Id);
                    int results = 0;
                    if (row != null)
                    {
                        if (rollback) transaction = dc.Database.BeginTransaction();
                        row.QuestionId = activation.QuestionId;
                        row.StartDate = activation.StartDate;
                        row.EndDate = activation.EndDate;
                        row.ActivationCode = activation.ActivationCode;

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

                    tblActivation row = dc.tblActivations.FirstOrDefault(c => c.Id == id);
                    int results = 0;
                    if (row != null)
                    {
                        if (rollback) transaction = dc.Database.BeginTransaction();

                        dc.tblActivations.Remove(row);
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
