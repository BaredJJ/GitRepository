using System;
using System.Collections.Generic;

namespace DataConnector.Patterns
{
    class AlbumsBuilder:Builder
    {
        private string[] _result;

        private static AlbumsBuilder BuildAlbumses( )
        {
            throw new NotImplementedException( );
        }

        private static Artist BuildArtist( )
        {
            throw new NotImplementedException( );
        }

        private static List<ArtistStyle> BuildStyleList( )
        {
            throw new NotImplementedException( );
        }

        private static List<Style> BuildStyles( )
        {
            throw new NotImplementedException( );
        }

        public override string[] Create( )
        {
            _result = new string[3];
            BuildAlbumses( );
            BuildArtist( );
            BuildStyleList( );
            BuildStyles( );
            return _result;
        }
    }
}
