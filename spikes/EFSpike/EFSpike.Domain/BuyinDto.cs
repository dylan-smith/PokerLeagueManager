using System.Runtime.Serialization;

namespace EFSpike.Domain
{
    [DataContract]
    public class BuyinDto : BaseDataTransferObject
    {
        [DataMember]
        public int BuyinAmount { get; set; }
    }
}
