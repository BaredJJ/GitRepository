namespace DataConnector
{
    class Style
    {
        private int _styleId;
        private string _name;
        private static int _count;

        public int StyleId => _styleId;
        public string Name => _name;
        public int Count => _count;

        public Style()
        { }

        public Style(int styleId, string name)
        {
            _styleId = styleId;
            _name = name;
            ++_count;
        }
    }
}
