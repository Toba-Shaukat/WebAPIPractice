using Entities.DataTransferObjects;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Net.Http.Headers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AWebAPIPractice
{
    public class CsvOutputFormatter : TextOutputFormatter
    {
        public CsvOutputFormatter()
        {
            //text/csv
            SupportedMediaTypes.Add(MediaTypeHeaderValue.Parse("text/csv"));
            //Encoding
            SupportedEncodings.Add(Encoding.UTF8);
            SupportedEncodings.Add(Encoding.Unicode);
        }

        protected override bool CanWriteType(Type type)
        {

            if(typeof(CompanyDto).IsAssignableFrom(type) || typeof(IEnumerable<CompanyDto>).IsAssignableFrom(type))
            {
                return base.CanWriteType(type);
            }

            return false;
        }

        public override async Task WriteResponseBodyAsync(OutputFormatterWriteContext context, Encoding selectedEncoding)
        {
            var response = context.HttpContext.Response;
            var sb = new StringBuilder();

            if(context.Object is IEnumerable<CompanyDto>)
            {
                foreach(var company in context.Object as IEnumerable<CompanyDto>)
                {
                    FormatCsv(sb, company);
                }
            }
            else
            {
                FormatCsv(sb, context.Object as CompanyDto);
            }

            await response.WriteAsync(sb.ToString());
        }

        private static void FormatCsv(StringBuilder sb, CompanyDto company)
        {
            //sb.AppendLine($"{company.Id},\"{company.Name},\"{company.FullAddress}\"");
            sb.AppendLine($"{company.Id}, {company.Name}, {company.FullAddress}");
        }
    }
}
