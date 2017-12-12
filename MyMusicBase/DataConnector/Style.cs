﻿using System;
using System.Text.RegularExpressions;

namespace DataConnector
{
    public class Style
    {
        private int _styleId;
        private string _name;

        public int StyleId => _styleId;
        public string Name => _name;

        public Style()
        { }

        public Style(string style)
        {
            string[] temp = Regex.Split(style, @"@@@");
            if (temp.Length != 0 && temp.Length >= 2)
            {
                _styleId = MyMusicBase.DataConnector.GetInt(temp[0]);
                _name = temp[1];
            }
            else throw new Exception("Can't create instance Style");
        }
    }
}
