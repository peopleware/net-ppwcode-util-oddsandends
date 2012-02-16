using System.Runtime.Serialization;

namespace PPWCode.Util.OddsAndEnds.Test_I
{
    public interface IPerson
    {
        [DataMember]
        string FirstName { get; set; }

        [DataMember]
        string LastName { get; set; }
    }
}