using System.Collections.Generic;

namespace DataConnector.Patterns
{
    public class Foreman
    {
        private readonly Builder _buider;

        public Foreman(Builder builder)
        {
            _buider = builder;
        }

        public List<List<string>> Construct(string option)
        {
            return _buider.Create(option);
        }
    }
}
