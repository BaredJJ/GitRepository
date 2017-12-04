using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataConnector.Patterns
{
    class Foreman
    {
        private Builder buider;

        public Foreman(Builder builder)
        {
            this.buider = builder;
        }

        public string[] Construct()
        {
            return buider.Create();
        }
    }
}
