using System.Runtime.Serialization;

namespace RDService
{
    [DataContract]
    public class CommonOutput
    {
        [DataMember]
        public bool success
        {
            get;
            set;
        }
        [DataMember]
        public string message
        {
            get;
            set;
        }
    }
}
