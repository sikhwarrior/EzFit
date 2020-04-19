using System;
using System.Collections.Generic;
using System.Text;

namespace EzFit.Models
{
    class Aliments
    {

    public string alim_grp_nom_eng { get; set; }

        public string alim_nom_eng { get; set; }

        //(g/100g)
        public string Protein { get; set; }
        public string Fat { get; set; }
        public string Carbohydrate { get; set; }
        public string Fibres { get; set; }
        public string Sugars { get; set; }
        public string Water { get; set; }



    }
}
