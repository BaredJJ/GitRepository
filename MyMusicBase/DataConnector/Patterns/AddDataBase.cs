using System;

namespace DataConnector.Patterns
{
    public static class AddDataBase
    {
        private static string _result;

        public static void AddStyle(int artistId, string[] name)
        {
            ArtistStyle[] array = new ArtistStyle[name.Length];
            for (int i = 0; i < array.Length; i++)
            {
                array[i].ArtistId = artistId;
                if (IsExistence("SELECT * FROM Style WHERE NAME = '" + name[i] + "'") == 1)
                {
                    array[i].StyleId = GetStyle().StyleId;
                }
                else
                {
                    //TODO добавление в БД нового стиля и запись в массив ARTISTStyle значения SyleID
                }
            }
            //TODO добавление в таблицу ArtistStyle
        }

        public static void AddAlbum(string name, string dateRelease, int artistId)
        {
            DateTime temp = MyMusicBase.DataConnector.GetData(dateRelease);
            Albums instance = new Albums(artistId, name, temp);
            //TODO добавить запись в БД
        }

        public static int AddArtist(string name, string appereance, string breackUp)
        {
            int n;
            if (IsExistence("SELECT * FROM Artist WHERE NAME = '" + name + "'") != 1)
            {
                Artist instance;
                DateTime appereanceTime = MyMusicBase.DataConnector.GetData(appereance);
                if (breackUp != "")
                {
                    DateTime breakUpTime = MyMusicBase.DataConnector.GetData(breackUp);
                    if (appereanceTime < breakUpTime)
                        instance = new Artist(name, appereanceTime, breakUpTime);
                    else throw new Exception("Дата начала не может быть больше даты окончания");
                }
                else instance = new Artist(name, appereanceTime);
                //TODO добавить запись в БД
                IsExistence("SELECT * FROM Artist WHERE NAME = '" + name + "'");
                n = GetArtist().Id;
            }
            else
            {
                n = -1; //GetArtist().Id;
            }        
            return n;
        }

        private static int IsExistence( string search)
        {
            _result = MyMusicBase.DataConnector.GetString(search);
            if (_result == "")
                return -1;
            else
                return 1;
        }

        private static Artist GetArtist() => new Artist(_result);

        private static Style GetStyle() => new Style(_result);
    }
}
