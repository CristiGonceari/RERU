using AutoMapper;
using CODWER.RERU.Evaluation.DataTransferObjects.QuestionCategory;
using CVU.ERP.Common.DataTransferObjects.SelectValues;

namespace CODWER.RERU.Evaluation.Application.QuestionCategories
{
    public class QuestionCategoryMapping : Profile
    {
        public QuestionCategoryMapping()
        {
            CreateMap<AddEditQuestionCategoryDto, Evaluation.Data.Entities.QuestionCategory>();

            CreateMap<Evaluation.Data.Entities.QuestionCategory, QuestionCategoryDto>()
                .ForMember(x => x.QuestionCount, opts => opts.MapFrom(qc => qc.QuestionUnits.Count));

            CreateMap<Evaluation.Data.Entities.QuestionCategory, SelectItem>()
                .ForMember(x => x.Value, opts => opts.MapFrom(qc => qc.Id))
                .ForMember(x => x.Label, opts => opts.MapFrom(qc => qc.Name));
        }
    }
}
