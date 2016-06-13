﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WeiKe.Models
{
    public class Weike
    {
        private int _id = 0;
        public int id { get { return _id;} set {  _id = value; }}
        public string title { get; set; }
        public string subject { get; set; }
        public string author { get; set; }
        public string src { get; set; }
        public string size { get; set; }
        public string description { get; set; }
        public int star { get; set; }

        public Weike()
        {

        }
        public Weike(string _title, string _subject, string _author, string _src, string _size, string _description, int _star)
        {
            title = _title;
            subject = _subject;
            author = _author;
            src = _src;
            size = _size;
            description = _description;
            star = _star;
        }

        public Weike(int _id, string _title, string _subject, string _author, string _src, string _size, string _description, int _star)
        {
            id = _id;
            title = _title;
            subject = _subject;
            author = _author;
            src = _src;
            size = _size;
            description = _description;
            star = _star;
        }
    }


}