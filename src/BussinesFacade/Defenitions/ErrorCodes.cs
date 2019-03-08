using System.Collections.Generic;

namespace BussinesFacade.Defenitions
{
    public class ErrorCodes
    {
        public Dictionary<long, string> ErrorsDictionary = new Dictionary<long, string>
        {
            {400, "Bad request"},
            {401, "Your API Key was not supplied or is invalid"},
            {403, "The plan associated with this API Key doesn't support this request"},
            {404, "Unknown error"},
            {429, "You have exceeded your API rate limit"},
            {500, "An internal server error occurred"}
        };
    }
}