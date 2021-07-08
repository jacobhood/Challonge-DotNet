using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Challonge.Exceptions
{
    internal class ErrorResponse
    {
        [JsonProperty("errors")]
        internal IEnumerable<string> Errors { get; set; }

        internal string Message
        {
            get
            {
                int i = 1;
                int errorCount = Errors.Count();
                StringBuilder builder = new($"Challonge responded with the following errors:{Environment.NewLine}");
                foreach (string error in Errors)
                {
                    builder.Append($"{i}. {error}{(i == errorCount ? "" : Environment.NewLine)}");
                    i++;
                }
                return builder.ToString();
            }
        }
    }
}
