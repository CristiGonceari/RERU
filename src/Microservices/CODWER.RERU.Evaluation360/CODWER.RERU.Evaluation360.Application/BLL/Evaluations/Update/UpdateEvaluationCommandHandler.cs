using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CODWER.RERU.Evaluation360.Application.BLL.Evaluations.GetEditEvaluation;
using CODWER.RERU.Evaluation360.DataTransferObjects.Evaluations;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RERU.Data.Entities.PersonalEntities.Enums;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Evaluation360.Application.BLL.Evaluations.Update
{
    public class UpdateEvaluationCommandHandler : IRequestHandler<UpdateEvaluationCommand, GetEvaluationDto>
    {
        private readonly AppDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ISender _sender;

        public UpdateEvaluationCommandHandler(AppDbContext dbContext, IMapper mapper, ISender sender)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _sender = sender;
        }

        public async Task<GetEvaluationDto> Handle(UpdateEvaluationCommand request, CancellationToken cancellationToken)
        {
            var evaluation = await _dbContext.Evaluations.FirstOrDefaultAsync(e=> e.Id == request.Id );

            List<decimal?> listForM1 = new List<decimal?> {evaluation.Question1, evaluation.Question2, evaluation.Question3, evaluation.Question4, evaluation.Question5};
            decimal? m1 = listForM1.Average();

            List<decimal?> listForM2 = new List<decimal?> {evaluation.Question6, evaluation.Question7, evaluation.Question8};
            decimal? m2 = listForM2.Average();

            List<decimal?> listForM3 = new List<decimal?> {evaluation.Score1, evaluation.Score2, evaluation.Score3, evaluation.Score4, evaluation.Score5};
            decimal? m3 = listForM3.Average();

            List<decimal?> listForPb = new List<decimal?> {evaluation.Question9, evaluation.Question10, evaluation.Question11, evaluation.Question12};
            decimal? pb = listForPb.Average();

            List<decimal?> listForM4 = new List<decimal?> {evaluation.Question13, pb};
            decimal? m4 = listForM4.Average();

            List<decimal?> listForMea = new List<decimal?> {m1, m2, m3, m4};
            decimal? mea = listForMea.Sum();
            decimal? mf;

            if (evaluation.PartialEvaluationScore != null)
            {
                List<decimal?> listForMf = new List<decimal?> {mea, evaluation.PartialEvaluationScore};
                mf = listForMf.Sum();
            }
            else
            {
                mf = mea;
            }

            if (mf >= 1 && mf <= 1.5m) evaluation.FinalEvaluationQualification = QualifierEnum.Dissatisfied;
            else if (mf >= 1.51m && mf <= 2.5m) evaluation.FinalEvaluationQualification = QualifierEnum.Satisfied;
            else if (mf >= 2.51m && mf <= 3.5m) evaluation.FinalEvaluationQualification = QualifierEnum.Good;
            else if (mf >= 3.51m && mf <= 4m) evaluation.FinalEvaluationQualification = QualifierEnum.VeryGood;
            
            evaluation.Points = mf;

            _mapper.Map(request.Evaluation, evaluation);
            await _dbContext.SaveChangesAsync();

            await _sender.Send(new GetEditEvaluationQuery(request.Id));

            return _mapper.Map<GetEvaluationDto>(evaluation);
        }
    }
}