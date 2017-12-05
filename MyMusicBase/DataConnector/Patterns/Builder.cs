using System.Collections.Generic;

namespace DataConnector.Patterns
{
    public abstract class Builder
    {
        public abstract List<List<string>> Create(string option);
    }
}
