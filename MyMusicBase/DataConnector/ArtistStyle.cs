namespace DataConnector
{
    class ArtistStyle
    {
        private int _artistId;
        private int _styleId;
        private static int _count;

        public int ArtistId => _artistId;
        public int StyleId => _styleId;
        public int Count => _count;

        public ArtistStyle()
        { }

        public ArtistStyle(int artistId, int styleId)
        {
            _artistId = artistId;
            _styleId = styleId;
            ++_count;
        }
    }
}
