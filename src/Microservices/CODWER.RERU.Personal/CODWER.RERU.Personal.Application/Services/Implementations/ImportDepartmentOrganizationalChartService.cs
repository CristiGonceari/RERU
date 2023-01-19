using AutoMapper;
using CODWER.RERU.Personal.Application.DepartmentRoleRelations.AddDepartmentRoleRelation;
using CODWER.RERU.Personal.Application.DepartmentRoleRelations.AddOrganizationalChartHead;
using CODWER.RERU.Personal.DataTransferObjects.DepartmentRoleRelations;
using CODWER.RERU.Personal.DataTransferObjects.DepartmentRoleRelations.Add;
using CVU.ERP.Common.DataTransferObjects.Files;
using MediatR;
using Microsoft.AspNetCore.Http;
using OfficeOpenXml;
using RERU.Data.Persistence.Context;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CODWER.RERU.Personal.Application.Services.Implementations
{
    public  class ImportDepartmentOrganizationalChartService : IImportDepartmentOrganizationalChartService
    {
        private readonly AppDbContext _appDbContext;
        private ExcelWorksheet _workSheet;
        private ExcelPackage _package;
        private int _totalRows;
        private readonly IMediator _mediator;


        public ImportDepartmentOrganizationalChartService(AppDbContext appDbContext, IMediator mediator)
        {
            _appDbContext = appDbContext;
            _mediator = mediator;
        }

        public async Task<FileDataDto> ImportDepartmentToDepartmentRelation(IFormFile data, int organizationalChartId)
        {
            await ReadExcelFile(data);

            for (var i = 1; i <= _totalRows; i++)
            {
                var firstCellCode = _appDbContext.Departments.FirstOrDefault(x => x.ColaboratorId == Convert.ToInt32(_workSheet.Cells[i, 1].Value));

                var secondCellCode = _appDbContext.Departments.FirstOrDefault(x => x.ColaboratorId == Convert.ToInt32(_workSheet.Cells[i, 2].Value));

                if (i <= 1)
                {
                    if (!string.IsNullOrEmpty(_workSheet.Cells[i, 1]?.Value?.ToString() ?? string.Empty) &&
                        string.IsNullOrEmpty(_workSheet.Cells[i, 2]?.Value?.ToString() ?? string.Empty)
                        )
                    {
                        if (firstCellCode != null)
                        {
                            await AddOrganizationalChartHead(firstCellCode.Id, organizationalChartId);
                        }
                        else 
                        {
                            _workSheet.Cells[i, 3].Value = $"Error: Nu există departament cu un astfel de Cod colaborator";
                        }
                       
                    }
                    else
                    {
                        _workSheet.Cells[i, 3].Value = $"Error: Randul trebuie să conțină în mod necesar numai Child Colaborator Code";
                    }
                }
                else 
                {
                    if (!string.IsNullOrEmpty(_workSheet.Cells[i, 1]?.Value?.ToString() ?? string.Empty) &&
                        !string.IsNullOrEmpty(_workSheet.Cells[i, 2]?.Value?.ToString() ?? string.Empty)
                        )
                    {
                        if (firstCellCode != null && secondCellCode != null)
                        {
                            await AddDepartmentRoleRelation(secondCellCode.Id, firstCellCode.Id, organizationalChartId);
                        }
                        else if (firstCellCode == null)
                        {
                            _workSheet.Cells[i, 3].Value = $"Error: Nu a fost găsit departament dupa Child Colaborator Code";
                        }
                        else if (secondCellCode == null)
                        {
                            _workSheet.Cells[i, 3].Value = $"Error: Nu a fost găsit departament dupa Parent Colaborator Code";
                        }
                        
                    }
                    else
                    {
                        _workSheet.Cells[i, 3].Value = $"Error: Randul trebuie să conțină în mod necesar Child Colaborator Code și Parent Colaborator Code";
                    }
                }
            }

            var streamBytesArray = _package.GetAsByteArray();

            return FileDataDto.GetExcel("Import-Department-Role-Relation", streamBytesArray);
        }

        private async Task ReadExcelFile(IFormFile data)
        {
            var fileStream = new MemoryStream();
            await data.CopyToAsync(fileStream);
            _package = new ExcelPackage(fileStream);
            _workSheet = _package.Workbook.Worksheets[0];
            _totalRows = _workSheet.Dimension.Rows;
        }

        private async Task<int> AddOrganizationalChartHead(int parentDepartmentId, int organizationalChartId)
        {
            var command = new AddOrganizationalChartHeadCommand 
            { 
                HeadId = parentDepartmentId, 
                OrganizationalChartId =organizationalChartId, 
                Type = OrganizationalChartItemType.Department
            };

            var result = await _mediator.Send(command);

            return result;
        }

        private async Task<int> AddDepartmentRoleRelation(int parentDepartmentId, int childDepartmentId, int organizationalChartId) 
        { 
            
            var data = new AddDepartmentRoleRelationDto()
            {
                ParentId = parentDepartmentId,
                ChildId = childDepartmentId,
                RelationType = DepartmentRoleRelationTypeEnum.DepartmentDepartment,
                OrganizationalChartId = organizationalChartId
            };

            var command = new AddDepartmentRoleRelationCommand { Data = data };

            var result = await _mediator.Send(command);

            return result;
        }
    }
}
