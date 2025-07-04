
using System.ComponentModel.DataAnnotations;

namespace Core.Entities
{
    public class BaseEntity
    {
        public int Id { get; set; }
        
        [Timestamp]
        public byte[] RowVersion { get; set; }
    }
}
