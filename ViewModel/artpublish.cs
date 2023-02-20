using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Artistesta.ViewModel
{
    public class artpublish
    {
        public long ARTID { get; set; }
        public Nullable<int> USERID { get; set; }
        public string INSPIRATION { get; set; }
        public IFormFile ARTWORK { get; set; }
        public string TITLE { get; set; }
        public string TIME { get; set; } = DateTime.Now.ToString();
        public string CATEGORY { get; set; }
        public Nullable<long> ENCOURAGES { get; set; }
        public Nullable<long> DISCOURAGES { get; set; }
        public Nullable<long> FAVORITES { get; set; }
        public Nullable<int> ISPINNED { get; set; }
        public bool FORSALE { get; set; }
        public string MINIMUMBID { get; set; }
        public string AUCTIONID { get; set; }
        public string CURHIGHESTBID { get; set; }
        public string CURBIDDERID { get; set; }
    }

    public enum cats
    {
        ABSTRACT,
        ANIMALS,
        ANIME,
        ARCHITECTURE,
        AUTOMOBILE,
        BLACKandWHITE,
        CITYSCAPE,
        GAMES,
        ILLUSTRATION,
        MISC,
        NATURE,
        OBJECTS,
        PORTRAIT,
        SKETCH,
        SUPERHERO,
        CALLIGRAPHY
    }
}