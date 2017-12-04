using System;
using System.Collections.Generic;

namespace DataConnector.Patterns
{
    class StyleBuilder:Builder
    {
        private string[] _result;

        private static Style BuildStyles( )
        {
            throw new NotImplementedException( );
        }

        private static List<ArtistStyle> BuildStyleList( )
        {
            throw new NotImplementedException( );
        }

        private static List<Artist> BuildArtist( )
        {
            throw new NotImplementedException( );
        }

        public override string[] Create( )
        {
            _result = new string[3];
            BuildStyles( );
            BuildStyleList( );
            BuildArtist( );
            return _result;
        }
    }
}
