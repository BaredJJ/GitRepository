﻿using System;
using System.Windows;

namespace MyMusicBase
{
    /// <summary>
    /// Логика взаимодействия для AddAlbums.xaml
    /// </summary>
    public partial class AddAlbums
    {
        public AddAlbums( )
        {
            InitializeComponent( );
            new Presenter(this, new MessageService());
        }

        public event EventHandler AddNewAlbum;

        private void AddAlbum_OnClick(object sender, RoutedEventArgs e)
        {
            AddNewAlbum?.Invoke(sender, e);
        }
    }
}