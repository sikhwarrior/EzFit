using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using SQLite;



namespace EzFit.Models
{

    [Table("Infos")]
    public class Infos : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;


        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }

        [MaxLength(200), Indexed]
        public string Name { get; set; }
        
        public int Age { get; set; }
        public int Poids { get; set; }
        public int Taille { get; set; }
        public string ValVisuel { get; set; }
        public string ValLifeStyle { get; set; }    
        public string ValSex { get; set; }
        public string ValObjectif { get; set; }

        public DateTime Date { get; set; }


        //[Column("Illustration")]
        //public string Image { get; set; }


        public double IMC { get; set; }
    
        public double MacroP { get; set; }
      
        public double MacroL { get; set; }
    
        public double MacroG { get; set; }
       
        public int IMG { get; set; }

        public double MBasal { get; set; }

        public double MBresult { get; set; }
        

        protected void OnPropertyChanged(string IMG)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(IMG));

            }
        }

    }
}
