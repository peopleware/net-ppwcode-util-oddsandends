using System.Runtime.Serialization;

namespace PPWCode.Util.OddsAndEnds.I.Tests
{
    public interface IPerson
    {
        [DataMember]
        string FirstName { get; set; }

        [DataMember]
        string LastName { get; set; }
    }
}