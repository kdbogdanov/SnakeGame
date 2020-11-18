using System.Text.Json.Serialization;
using Server.Entities;

namespace Server.Models
{
    /// <summary>
    /// Class ChangeDirection for POST-request.
    /// </summary>
    public class ChangeDirection
    {
        /// <summary>
        /// Enumeration with all possible directions.
        /// </summary>
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public Direction Direction
        {
            get;
            set;
        }
    }
}
