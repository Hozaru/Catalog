using System;

namespace Hozaru.Catalogs.Catalog.Exceptions
{
    [Serializable]
    public class InvalidImpactTypeException : Exception
    {
        public InvalidImpactTypeException()
            : base("Invalid impact type")
        {
        }
    }
}
