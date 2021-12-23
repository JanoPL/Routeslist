using System;
using System.Collections.Generic;
using System.Text;

namespace RoutesList.Build.Models
{
    public class RoutesListOptions
    {
        public string Tittle { get; set; }
        public string StylePath { get; set; }
        public string CharSet { get; set; } = "UTF-8";
        public string FooterLink { get; set; } = "https://github.com/JanoPL/Routeslist";
        public string FooterText { get; set; } = "RoutesList";
    }
}
