using System.ComponentModel.DataAnnotations;

namespace ProjectHK3.Domain.Common
{
    public interface IEntity
    {
        [Key]
        int Id { get; set; }
    }
}
