using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TilausDB2._ViewModels
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Drawing;

    public class OrderSummaryData
    {
        public int TilausID { get; set; }
        public int AsiakasID { get; set; }
        public Nullable<System.DateTime> Tilauspvm { get; set; }
        public Nullable<System.DateTime> Tomituspvm { get; set; }
        public string Tomitusosoite { get; set; }
        public string Postinumero { get; set; }
        public int TuoteID{ get; set; }
        public float Ahinta { get; set; }
        public int Maara { get; set; }
        public string Nimi { get; set; }
        public string Lisätieto_1 { get; set; }
        public string Lisätieto_2 { get; set; }
        public string Kuva { get; set; }    

    }
}