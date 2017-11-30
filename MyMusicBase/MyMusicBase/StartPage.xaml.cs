using System.Collections.Generic;
using System.Text;
using System.Windows;


namespace MyMusicBase
{
    /// <summary>
    /// Логика взаимодействия для StartPage.xaml
    /// </summary>
    public partial class StartPage
    {
        public StartPage( )
        {
            InitializeComponent( );
        }

        private void Search_OnClick(object sender, RoutedEventArgs e)
        {
            string searchString = "SELECT * FROM Artist WHERE Name = '" + SearchBox.Text + "'";
            StringBuilder str = new StringBuilder("Исполнитель: ");
            int temp = DataConnector.GetId("ArtistId", searchString);
            str.AppendFormat(DataConnector.GetString(searchString));
            ///////////////
            str.AppendFormat("Альбомы: ");
            searchString = "SELECT * FROM Albums WHERE ArtistId = '" + temp + "'";
            str.AppendFormat(DataConnector.GetString(searchString));
            ////////////////////////
            str.AppendFormat("Жанры: ");
            searchString = "SELECT StyleId FROM ArtistStyle WHERE ArtistId = '" + temp + "'";
            List<int> styleId = DataConnector.GetList(searchString);
            /////////////////////////////////////////
            foreach (var i in styleId)
            {
                searchString = "SELECT Style FROM Style WHERE StyleId = '" + i + "'";
                str.AppendFormat(DataConnector.GetScalar(searchString).ToString());
            }
            Result.Text = str.ToString();
        }
    }
}
