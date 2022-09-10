using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KodlamaDevs.Application.Features.Technologies.DTOs
{
    public class GetTechnologyByIdDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string ProgrammingLanguageName { get; set; } = string.Empty;
    }
}
