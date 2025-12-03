using ProjectHK3.Domain.Emtitys;
using ProjectHK3.Domain.ValueObjects;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectHK3.Application.DTOs.Insurer
{
    public class CreateInsurerRequest
    {
        public string CompanyName { get; set; } = string.Empty;

        public string Address { get; set; } = string.Empty;

        public string ContactEmail { get; set; } = string.Empty;

        public string ContactPhone { get; set; } = string.Empty;

        public string Website { get; set; } = string.Empty;

        public int? CreateBy { get; set; }
    }
}
