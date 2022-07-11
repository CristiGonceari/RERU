using AutoMapper;
using CVU.ERP.Common.DataTransferObjects.Files;
using CVU.ERP.Module.Application.TableExportServices.Interfaces;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace CVU.ERP.Module.Application.TableExportServices.Implementations
{
    public class TableXmlExport<TSource, TDestination> : ITableXmlExport<TSource, TDestination>
    {
        private readonly IMapper _mapper;

        public TableXmlExport(IMapper mapper)
        {
            _mapper = mapper;
        }

        public FileDataDto ExportTable(TableData<TSource> data)
        {
            var result = CreateXmlString(_mapper.Map<List<TDestination>>(data.Items));

            var prettyXml = PrettyXml(result);

            return FileDataDto.GetXml($"{data.Name}", Encoding.ASCII.GetBytes(prettyXml));
        }

        public FileDataDto ExportListTable(TableListData<TDestination> data)
        {
            var result = CreateXmlString(data.Items);

            var prettyXml = PrettyXml(result);

            return FileDataDto.GetXml($"{data.Name}", Encoding.ASCII.GetBytes(prettyXml));
        }
        private string CreateXmlString(List<TDestination> items)
        {
            using var stringWriter = new StringWriter();
            using (var xmlWriter = new XmlTextWriter(stringWriter))
            {
                var serializer = new XmlSerializer(typeof(List<TDestination>));
                serializer.Serialize(xmlWriter, items);
            }
            var xmlResult = stringWriter.ToString();

            return xmlResult;
        }

        private static string PrettyXml(string xml)
        {
            var stringBuilder = new StringBuilder();

            var element = XElement.Parse(xml);

            var settings = new XmlWriterSettings
            {
                OmitXmlDeclaration = true,
                Indent = true,
                NewLineOnAttributes = true
            };

            using (var xmlWriter = XmlWriter.Create(stringBuilder, settings))
            {
                element.Save(xmlWriter);
            }

            return stringBuilder.ToString();
        }
    }
}
