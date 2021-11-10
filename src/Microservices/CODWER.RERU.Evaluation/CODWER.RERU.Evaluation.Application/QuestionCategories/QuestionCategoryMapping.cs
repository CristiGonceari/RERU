using AutoMapper;
using CODWER.RERU.Evaluation.Data.Entities;
using CODWER.RERU.Evaluation.DataTransferObjects.QuestionCategory;
using CVU.ERP.Common.DataTransferObjects.SelectValues;

namespace CODWER.RERU.Evaluation.Application.QuestionCategories
{
    public class QuestionCategoryMapping : Profile
    {
        public QuestionCategoryMapping()
        {
            CreateMap<AddEditQuestionCategoryDto, QuestionCategory>()
                .ForMember(x => x.Id, opts => opts.Ignore());

            CreateMap<QuestionCategory, QuestionCategoryDto>()
                .ForMember(x => x.QuestionCount, opts => opts.MapFrom(qc => qc.QuestionUnits.Count));

            CreateMap<QuestionCategory, SelectItem>()
                .ForMember(x => x.Value, opts => opts.MapFrom(qc => qc.Id))
                .ForMember(x => x.Label, opts => opts.MapFrom(qc => qc.Name));
        }
    }
}
