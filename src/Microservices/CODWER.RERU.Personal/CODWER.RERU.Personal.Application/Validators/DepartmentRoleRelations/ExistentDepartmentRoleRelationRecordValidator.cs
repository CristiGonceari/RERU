using CODWER.RERU.Personal.Application.Validation;
using CODWER.RERU.Personal.Data.Persistence.Context;
using CODWER.RERU.Personal.DataTransferObjects.DepartmentRoleRelations;
using CODWER.RERU.Personal.DataTransferObjects.DepartmentRoleRelations.Add;
using CVU.ERP.Common.Extensions;
using FluentValidation;
using FluentValidation.Validators;
using System.Linq;

namespace CODWER.RERU.Personal.Application.Validators.DepartmentRoleRelations
{
    public class ExistentDepartmentRoleRelationRecordValidator : AbstractValidator<AddDepartmentRoleRelationDto>
    {
        private readonly AppDbContext _appDbContext;

        public ExistentDepartmentRoleRelationRecordValidator(AppDbContext appDbContext, string errorMessage)
        {
            _appDbContext = appDbContext;

            RuleFor(x => x).Custom((data, c) =>
                ExistentDepartmentRoleRelationRecord(data.ParentId, data.ChildId, (DepartmentRoleRelationTypeEnum)data.RelationType, errorMessage, data.OrganizationalChartId, c));
        }

        private void ExistentDepartmentRoleRelationRecord(int parentId, int childId, DepartmentRoleRelationTypeEnum type, string errorMessage, int orgChartId, CustomContext context)
        {
            switch (type)
            {
                case DepartmentRoleRelationTypeEnum.DepartmentDepartment:
                    {
                        var existent = _appDbContext.ParentDepartmentChildDepartments
                            .Any(x => x.ParentDepartmentId == parentId && x.ChildDepartmentId == childId && x.OrganizationalChartId == orgChartId);

                        if (existent)
                        {
                            context.AddFail(ValidationCodes.EXISTENT_RECORD, errorMessage);
                        }
                        break;
                    }

                case DepartmentRoleRelationTypeEnum.DepartmentRole:
                    {
                        var existent = _appDbContext.ParentDepartmentChildOrganizationRoles.Any(x => x.ParentDepartmentId == parentId && x.ChildOrganizationRoleId == childId && x.OrganizationalChartId == orgChartId);

                        if (existent)
                        {
                            context.AddFail(ValidationCodes.EXISTENT_RECORD, errorMessage);
                        }
                        break;
                    }

                case DepartmentRoleRelationTypeEnum.RoleDepartment:
                    {
                        var existent = _appDbContext.ParentOrganizationRoleChildDepartments.Any(x => x.ParentOrganizationRoleId == parentId && x.ChildDepartmentId == childId && x.OrganizationalChartId == orgChartId);

                        if (existent)
                        {
                            context.AddFail(ValidationCodes.EXISTENT_RECORD, errorMessage);
                        }
                        break;
                    }

                case DepartmentRoleRelationTypeEnum.RoleRole:
                    {
                        var existent = _appDbContext.ParentOrganizationRoleChildOrganizationRoles.Any(x => x.ParentOrganizationRoleId == parentId && x.ChildOrganizationRoleId == childId && x.OrganizationalChartId == orgChartId);

                        if (existent)
                        {
                            context.AddFail(ValidationCodes.EXISTENT_RECORD, errorMessage);
                        }
                        break;
                    }
            }
        }
    }
}
