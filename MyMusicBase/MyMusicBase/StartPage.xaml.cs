﻿using System;
using System.Collections.Generic;
using System.Windows;
using DataConnector.Patterns;


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
            ArtistText.Text = "";
            AlbumText.Text = "";
            StyleText.Text = "";
            List<List<string>> list = new List<List<string>>();
            if (ChoiseOfSearch.SelectedIndex == 00)
            {
                list = new Foreman(new ArtistBuilder()).Construct(SearchBox.Text);
            }
            else if (ChoiseOfSearch.SelectedIndex == 1)
            {
                list = new Foreman(new StyleBuilder()).Construct(SearchBox.Text);
            }
            else if (ChoiseOfSearch.SelectedIndex == 2)
            {
                list = new Foreman(new AlbumsBuilder( )).Construct(SearchBox.Text);
            }
            for (int i = 0; i < list.Count; i++)
            {
                for (int j = 0; j < list[i].Count; j++)
                {
                    if (i == 0)
                        ArtistText.Text += list[i][j] + Environment.NewLine;
                    else if (i == 1)
                        StyleText.Text += list[i][j] + Environment.NewLine;
                    else if (i == 2)
                        AlbumText.Text += list[i][j] + Environment.NewLine;
                }
            }
        }

        private void AddButton_OnClick(object sender, RoutedEventArgs e)
        {
            //TO DO Реализация добавления в БД
            throw new System.NotImplementedException();
        }
    }
}
