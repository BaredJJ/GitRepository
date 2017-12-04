using System;
using System.Collections.Generic;

namespace DataConnector.Patterns
{
    class ArtistBuilder:Builder
    {
        private string[] _result;

        private static Artist BuildArtist()
        {

            throw new NotImplementedException();
        }

        private static List<AlbumsBuilder> BuildAlbumses( )
        {
            throw new NotImplementedException( );
        }

        private static List<ArtistStyle> BuildStyleList()
        {
            throw new NotImplementedException();
        }

        private static List<Style> BuildStyles()
        {
            throw new NotImplementedException();
        }

        public override string[] Create()
        {
            _result = new string[3];
            BuildArtist();
            BuildAlbumses();
            BuildStyleList();
            BuildStyles();
            return _result;
        }
    }
}
