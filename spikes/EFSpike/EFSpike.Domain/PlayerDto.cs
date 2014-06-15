using System.Runtime.Serialization;

namespace EFSpike.Domain
{
    [DataContract]
    public class PlayerDto : BaseDataTransferObject
    {
        [DataMember]
        public string PlayerName { get; set; }

        [DataMember]
        public int Placing { get; set; }

        [DataMember]
        public int Winnings { get; set; }
    }
}
