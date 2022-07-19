using System;
using AutoMapper;
using CODWER.RERU.Personal.DataTransferObjects.NomenclatureTypes.NomenclatureRecords;
using CODWER.RERU.Personal.DataTransferObjects.NomenclatureTypes.NomenclatureRecords.RecordValues;
using CVU.ERP.Common.DataTransferObjects.SelectValues;
using RERU.Data.Entities.PersonalEntities.NomenclatureType.NomenclatureRecords;
using RERU.Data.Entities.PersonalEntities.NomenclatureType.NomenclatureRecords.RecordValues;

namespace CODWER.RERU.Personal.Application.NomenclatureTypes.NomenclatureRecords
{
    public class NomenclatureRecordValueMappingProfile : Profile
    {
        public NomenclatureRecordValueMappingProfile()
        {
            //nomenclatureRecords selectItems
            CreateMap<NomenclatureRecord, SelectItem>()
                .ForMember(x => x.Label, opts => opts.MapFrom(x => x.Name))
                .ForMember(x => x.Value, opts => opts.MapFrom(x => x.Id));

            //record
            CreateMap<NomenclatureRecordDto, NomenclatureRecord>()
                .ForMember(x => x.Id, opts => opts.Ignore())
                .ForMember(x => x.IsActive, opts => opts.Ignore())
                .ForMember(x => x.NomenclatureType, opts => opts.Ignore())
                .ForMember(x => x.RecordValues, opts => opts.Ignore());

            CreateMap<NomenclatureRecord, NomenclatureRecordDto>()
                .ForMember(x => x.RecordValues, opts => opts.Ignore());

            //convert to DbEntity
            CreateMap<RecordValueDto, RecordValueBoolean>()
                .ForMember(x => x.Id, opts => opts.Ignore())
                .ForMember(x => x.NomenclatureRecord, opts => opts.Ignore())
                .ForMember(x => x.NomenclatureColumn, opts => opts.Ignore())
                .ForMember(x => x.Value, opts => opts.MapFrom(op=> ConvertToBool(op.StringValue)));

            CreateMap<RecordValueDto, RecordValueInteger>()
                .ForMember(x => x.Id, opts => opts.Ignore())
                .ForMember(x => x.NomenclatureRecordId, opts => opts.Ignore())
                .ForMember(x => x.NomenclatureColumn, opts => opts.Ignore())
                .ForMember(x => x.Value, opts => opts.MapFrom(op => ConvertToInt(op.StringValue)));

            CreateMap<RecordValueDto, RecordValueChar>()
                .ForMember(x => x.Id, opts => opts.Ignore())
                .ForMember(x => x.NomenclatureRecord, opts => opts.Ignore())
                .ForMember(x => x.NomenclatureColumn, opts => opts.Ignore())
                .ForMember(x => x.Value, opts => opts.MapFrom(op => ConvertToChar(op.StringValue)));

            CreateMap<RecordValueDto, RecordValueDate>()
                .ForMember(x => x.Id, opts => opts.Ignore())
                .ForMember(x => x.NomenclatureRecord, opts => opts.Ignore())
                .ForMember(x => x.NomenclatureColumn, opts => opts.Ignore())
                .ForMember(x => x.Value, opts => opts.MapFrom(op=>ConvertToDate(op.StringValue)));

            CreateMap<RecordValueDto, RecordValueDateTime>()
                .ForMember(x => x.Id, opts => opts.Ignore())
                .ForMember(x => x.NomenclatureRecord, opts => opts.Ignore())
                .ForMember(x => x.NomenclatureColumn, opts => opts.Ignore())
                .ForMember(x => x.Value, opts => opts.MapFrom(op => ConvertToDateTime(op.StringValue)));

            CreateMap<RecordValueDto, RecordValueDouble>()
                .ForMember(x => x.Id, opts => opts.Ignore())
                .ForMember(x => x.NomenclatureRecord, opts => opts.Ignore())
                .ForMember(x => x.NomenclatureColumn, opts => opts.Ignore())
                .ForMember(x => x.Value, opts => opts.MapFrom(op => ConvertToDouble(op.StringValue)));

            CreateMap<RecordValueDto, RecordValueEmail>()
                .ForMember(x => x.Id, opts => opts.Ignore())
                .ForMember(x => x.NomenclatureRecord, opts => opts.Ignore())
                .ForMember(x => x.NomenclatureColumn, opts => opts.Ignore())
                .ForMember(x => x.Value, opts => opts.MapFrom(op =>op.StringValue));

            CreateMap<RecordValueDto, RecordValueText>()
                .ForMember(x => x.Id, opts => opts.Ignore())
                .ForMember(x => x.NomenclatureRecord, opts => opts.Ignore())
                .ForMember(x => x.NomenclatureColumn, opts => opts.Ignore())
                .ForMember(x => x.Value, opts => opts.MapFrom(op => op.StringValue));


            //convert to dto
            CreateMap<RecordValueBoolean, RecordValueDto>()
                .ForMember(x => x.Type, opts => opts.Ignore())
                .ForMember(x => x.StringValue, opts => opts.MapFrom(x => x.Value.ToString()));

            CreateMap<RecordValueInteger, RecordValueDto>()
                .ForMember(x => x.Type, opts => opts.Ignore())
                .ForMember(x => x.StringValue, opts => opts.MapFrom(x => x.Value.ToString()));

            CreateMap<RecordValueChar, RecordValueDto>()
                .ForMember(x => x.Type, opts => opts.Ignore())
                .ForMember(x => x.StringValue, opts => opts.MapFrom(x => x.Value.ToString()));

            CreateMap<RecordValueDate, RecordValueDto>()
                .ForMember(x => x.Type, opts => opts.Ignore())
                .ForMember(x => x.StringValue, opts => opts.MapFrom(x => ConvertToDateDto(x.Value)));

            CreateMap<RecordValueDateTime, RecordValueDto>()
                .ForMember(x => x.Type, opts => opts.Ignore())
                .ForMember(x => x.StringValue, opts => opts.MapFrom(x => ConvertToDateTimeDto(x.Value)));

            CreateMap<RecordValueDouble, RecordValueDto>()
                .ForMember(x => x.Type, opts => opts.Ignore())
                .ForMember(x => x.StringValue, opts => opts.MapFrom(x => x.Value.ToString()));

            CreateMap<RecordValueEmail, RecordValueDto>()
                .ForMember(x => x.Type, opts => opts.Ignore())
                .ForMember(x => x.StringValue, opts => opts.MapFrom(x => x.Value.ToString()));

            CreateMap<RecordValueText, RecordValueDto>()
                .ForMember(x => x.Type, opts => opts.Ignore())
                .ForMember(x => x.StringValue, opts => opts.MapFrom(x => x.Value.ToString()));

        }

        private bool? ConvertToBool(string value)
        {
            return string.IsNullOrEmpty(value) ? null : bool.Parse(value);
        }

        private int? ConvertToInt(string value)
        {
            return string.IsNullOrEmpty(value) ? null : int.Parse(value);
        }

        private char? ConvertToChar(string value)
        {
            return string.IsNullOrEmpty(value) ? null : char.Parse(value);
        }

        private DateTime? ConvertToDate(string value)
        {
            return string.IsNullOrEmpty(value) ? null : DateTime.Parse(value).Date;
        }

        private DateTime? ConvertToDateTime(string value)
        {
            return string.IsNullOrEmpty(value) ? null : DateTime.Parse(value);
        }

        private double? ConvertToDouble(string value)
        {
            return string.IsNullOrEmpty(value) ? null : double.Parse(value);
        }


        private string ConvertToDateDto(DateTime? date)
        {
            return date != null ? ((DateTime)date).ToString("yyyy-MM-ddTHH\\:mm\\:ss.fffffffzzz") : string.Empty;
        }
        private string ConvertToDateTimeDto(DateTime? date)
        {
            return date != null ? ((DateTime)date).ToString("yyyy-MM-ddTHH\\:mm\\:ss.fffffffzzz") : string.Empty;
        }
    }
}
