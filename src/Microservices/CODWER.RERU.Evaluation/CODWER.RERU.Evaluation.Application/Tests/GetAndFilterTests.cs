﻿using System.Linq;
using CODWER.RERU.Evaluation.Data.Entities;
using CODWER.RERU.Evaluation.Data.Persistence.Context;
using CODWER.RERU.Evaluation.DataTransferObjects.Tests;
using Microsoft.EntityFrameworkCore;

namespace CODWER.RERU.Evaluation.Application.Tests
{
    public static class GetAndFilterTests
    {
        public static IQueryable<Test> Filter(AppDbContext appDbContext, TestFiltersDto request)
        {
            var tests = appDbContext.Tests
                .Include(t => t.TestTemplate)
                .Include(t => t.TestQuestions)
                .Include(t => t.UserProfile)
                .Include(t => t.Location)
                .Include(t => t.Event)
                .OrderByDescending(x => x.CreateDate)
                .Select(t => new Test
                {
                    Id = t.Id,
                    UserProfile = t.UserProfile,
                    TestTemplate = t.TestTemplate,
                    TestQuestions = t.TestQuestions,
                    Location = t.Location,
                    Event = t.Event,
                    AccumulatedPercentage = t.AccumulatedPercentage,
                    EvaluatorId = t.EvaluatorId,
                    EventId = t.EventId,
                    ResultStatus = t.ResultStatus,
                    TestStatus = t.TestStatus,
                    ProgrammedTime = t.ProgrammedTime,
                    EndTime = t.EndTime,
                    TestTemplateId = t.TestTemplateId,
                    TestPassStatus = t.TestPassStatus
                })
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(request.TestTemplateName))
            {
                tests = tests.Where(x => x.TestTemplate.Name.ToLower().Contains(request.TestTemplateName.ToLower()));
            }

            if (!string.IsNullOrWhiteSpace(request.UserName))
            {
                tests = tests.Where(x => x.UserProfile.FirstName.ToLower().Contains(request.UserName.ToLower()) || x.UserProfile.LastName.ToLower().Contains(request.UserName.ToLower()) || x.UserProfile.Patronymic.ToLower().Contains(request.UserName.ToLower()));
            }

            if (!string.IsNullOrWhiteSpace(request.Idnp))
            {
                tests = tests.Where(x => x.UserProfile.Idnp.Contains(request.Idnp));
            }

            if (request.TestStatus.HasValue)
            {
                tests = tests.Where(x => x.TestStatus == request.TestStatus);
            }

            if (!string.IsNullOrWhiteSpace(request.LocationKeyword))
            {
                tests = tests.Where(x => x.Location.Name.ToLower().Contains(request.LocationKeyword.ToLower()) || x.Location.Address.ToLower().Contains(request.LocationKeyword.ToLower()));
            }

            if (!string.IsNullOrWhiteSpace(request.EventName))
            {
                tests = tests.Where(x => x.Event.Name.ToLower().Contains(request.EventName.ToLower()));
            }

            if (request.ProgrammedTimeFrom.HasValue)
            {
                tests = tests.Where(x => x.ProgrammedTime >= request.ProgrammedTimeFrom);
            }

            if (request.ProgrammedTimeTo.HasValue)
            {
                tests = tests.Where(x => x.ProgrammedTime <= request.ProgrammedTimeTo);
            }

            return tests;
        }
    }
}
