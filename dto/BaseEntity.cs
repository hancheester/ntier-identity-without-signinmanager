using System.Runtime.Serialization;

namespace dto
{
    [DataContract]
    public abstract class BaseEntity
    {
        [DataMember]
        public int Id { get; set; }
    }
}
