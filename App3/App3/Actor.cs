using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace App3
{
    public class Actor
    {
        public Actor()
        {

        }
        public string groupname { get; set; }
        public string content { get; set; }
        public string data { get; set; }
        public string rennum { get; set; }
        public override string ToString()
        {
            return groupname;
        }
    }
}
