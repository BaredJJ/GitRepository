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
        private readonly IMessageService _messageService;

        #region Конструкторы
        public Presenter(MainWindow window, IMessageService message)
        {
            _mainWindow = window;
            _mainWindow.MainWindowEvent += MainWindow_mainWindowEvent;
            _messageService = message;
        }

        public Presenter(StartPage startPage, IMessageService message)
        {
            _startPageWindow = startPage;
            _startPageWindow.StartPageEvent += StartPageWindow_startPageEvent;
            _startPageWindow.AddEvent += _startPageWindow_AddEvent;
            _messageService = message;
        }

        public Presenter(AddArtist addArtist, IMessageService message)
        {
            _addArtist = addArtist;
            _addArtist.NewGroup += _addArtist_NewGroup;
            _messageService = message;
        }

        public Presenter(AddAlbums addAlbums, IMessageService message)
        {
            _addAlbums = addAlbums;
            _addAlbums.AddNewAlbum += _addAlbums_AddNewAlbum;
            _messageService = message;
        }

        public Presenter(AddStyle addStyle, IMessageService message)
        {
            _addStyle = addStyle;
            _addStyle.AddNewStyle += _addStyle_AddNewStyle;
            _messageService = message;
        }
        #endregion

        #region Добавление в БД
        private void _addStyle_AddNewStyle(object sender, EventArgs e)
        {
            _addStyle.Close();
            _startPageWindow.Show();
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
            albums.Show();
            _addArtist.Close();
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
