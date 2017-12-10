using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Windows;
using DataConnector.Patterns;

namespace MyMusicBase
{
    public class Presenter
    {
        private readonly MainWindow _mainWindow;
        private readonly StartPage _startPageWindow;
        private readonly AddArtist _addArtist;
        private readonly AddAlbums _addAlbums;
        private readonly AddStyle _addStyle;
        private readonly IMessageService _messageService = new MessageService();

        #region Конструкторы
        public Presenter(MainWindow window)
        {
            _mainWindow = window;
            _mainWindow.MainWindowEvent += MainWindow_mainWindowEvent;
        }

        public Presenter(StartPage startPage)
        {
            _startPageWindow = startPage;
            _startPageWindow.StartPageEvent += StartPageWindow_startPageEvent;
            _startPageWindow.AddEvent += _startPageWindow_AddEvent;
        }

        public Presenter(AddArtist addArtist)
        {
            _addArtist = addArtist;
            _addArtist.NewGroup += _addArtist_NewGroup;
        }

        public Presenter(AddAlbums addAlbums)
        {
            _addAlbums = addAlbums;
            _addAlbums.AddNewAlbum += _addAlbums_AddNewAlbum;
        }

        public Presenter(AddStyle addStyle)
        {
            _addStyle = addStyle;
            _addStyle.AddNewStyle += _addStyle_AddNewStyle;
        }
        #endregion

        #region Добавление в БД
        private void _addStyle_AddNewStyle(object sender, EventArgs e)
        {
            _addStyle.Close();
        }

        private void _addAlbums_AddNewAlbum(object sender, EventArgs e)
        {
            while (true)
            {
                MessageBoxResult result = _messageService.ShowExclametion("Вы ввели все альбомы?");
                if (result == MessageBoxResult.Yes)
                {
                    Window style = new AddStyle();
                    style.Show();
                    _addAlbums.Close();
                    break;
                }
            }
        }

        private void _addArtist_NewGroup(object sender, EventArgs e)
        {
            Window albums = new AddAlbums();
            if (_addArtist.ArtistBox.Text != "" && _addArtist.AppeareanceBox.Text != "")
            {
                try
                {
                    DataConnector.GetData(_addArtist.AppeareanceBox.Text);
                    string result = _addArtist.ArtistBox.Text + "@@@" + _addArtist.AppeareanceBox.Text;
                    if (_addArtist.BreackUpBox.Text != "")
                    {
                        try
                        {
                            DataConnector.GetData(_addArtist.BreackUpBox.Text);
                            result += "@@@" + _addArtist.BreackUpBox.Text;
                        }
                        catch (Exception exception)
                        {
                            throw;
                        }
                    }
                    albums.Show( );
                    _addArtist.Close( );
                }
                catch (Exception exception)
                {
                    _messageService.ShowError(exception.Message);
                }
            }
            else _messageService.ShowMessage("Вы не ввели обязательные данные");
        }
        #endregion

        #region MainWindow
        private void MainWindow_mainWindowEvent(object sender, EventArgs e)
        {
            
            SqlConnection instance = new SqlConnection(DataConnector.SqlStringBuilder(_mainWindow.UserName.Text, _mainWindow.Pasword.Password).ConnectionString);
            try
            {
                instance.Open( );
                Window startPage = new StartPage( );
                instance.Close( );
                startPage.Show( );
                _mainWindow.LoginWindow.Close( );
            }
            catch (Exception)
            {
                _messageService.ShowError("Проверьте имя пользлвателя и пароль. Или возможно отсудствует доступ к базе данных.");
            }
            finally
            {
                instance.Close( );
            }
        }
        #endregion

        #region SatrtPage
        private void StartPageWindow_startPageEvent(object sender, EventArgs e)
        {
            try
            {
                if (_startPageWindow.SearchBox.Text != "")
                {
                    _startPageWindow.ArtistText.Text = "";
                    _startPageWindow.AlbumText.Text = "";
                    _startPageWindow.StyleText.Text = "";
                    List<List<string>> list = new List<List<string>>( );
                    if (_startPageWindow.ChoiseOfSearch.SelectedIndex == 00)
                    {
                        list = new Foreman(new ArtistBuilder( )).Construct(_startPageWindow.SearchBox.Text);
                    }
                    else if (_startPageWindow.ChoiseOfSearch.SelectedIndex == 1)
                    {
                        list = new Foreman(new StyleBuilder( )).Construct(_startPageWindow.SearchBox.Text);
                    }
                    else if (_startPageWindow.ChoiseOfSearch.SelectedIndex == 2)
                    {
                        list = new Foreman(new AlbumsBuilder( )).Construct(_startPageWindow.SearchBox.Text);
                    }
                    if (list.Count != 0)
                        for (int i = 0; i < list.Count; i++)
                        {
                            for (int j = 0; j < list[i].Count; j++)
                            {
                                if (i == 0)
                                    _startPageWindow.ArtistText.Text += list[i][j] + Environment.NewLine;
                                else if (i == 1)
                                    _startPageWindow.StyleText.Text += list[i][j] + Environment.NewLine;
                                else if (i == 2)
                                    _startPageWindow.AlbumText.Text += list[i][j] + Environment.NewLine;
                            }
                        }
                    else
                        _messageService.ShowMessage("Мы не нашли, то что вы ищите в базе");
                }

            }
            catch (Exception ex)
            {
                _messageService.ShowError(ex.Message);
            }
        }

        private void _startPageWindow_AddEvent(object sender, EventArgs e)
        {
            Window artist = new AddArtist( );
            artist.Show( );
        }
#endregion
    }
}
