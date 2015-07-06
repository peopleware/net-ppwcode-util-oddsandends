using System.Runtime.Serialization;

namespace PPWCode.Util.OddsAndEnds.Test_I
{
    public interface IAddress
    {
        [DataMember]
        string StreetAndNr { get; set; }

        [DataMember]
        string PostalCode { get; set; }

        [DataMember]
        string City { get; set; }
    }
}