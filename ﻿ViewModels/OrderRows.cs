using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TilausDB2.ViewModels
{
    using System;
    using System.Drawing;
    public class OrderRows
    {
        public int TilausID { get; set; }
        public int TuoteID { get; set; }
        public float Ahinta { get; set; }
        public int Maara { get; set; }
        public string Nimi { get; set; }
        public string Kuva { get; set; }
        public string Lisätieto_1 { get; set; }
        public string Lisätieto_2 { get; set; }

    }

}