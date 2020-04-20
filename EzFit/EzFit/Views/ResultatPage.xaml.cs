using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EzFit.Models;
using Xam.Forms.Markdown;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;



namespace EzFit.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ResultatPage : ContentPage
    {

       

        public double femmeR;
        public double hommeR;

        public double macroP;
        public double macroG;
        public double macroL;


        public string IMCR;
        public string IMGR;


        public double IMC;
        public double IMG;
        public double fibre;
        public double hommeO;
        public double femmeO;

        //PERDRE = deficite 20 % / deficte 10 % faire des muscle en meme temps / maintenir / gagner + 20 %

        public double perdre = -(20 / (double)100);
        public double perdreP = -(10 / (double)100);
        public double maintenir = 0;
        public double prendreM = (10 / (double)100);

        //Formule améliorée de Harris et Benedict par Roza et Shizgal(1984) :
        //MB(Homme) = 13,707∗Poids(kg) + 492,3∗Taille(m)−6,673∗Age(an) + 77,607
        //MB(Femme) = 9,740∗Poids(kg) + 172,9∗Taille(m) − 4,737∗Age(an) + 667,051

        public float femme1 = 9.740f;
        public float femme2 = 172.9f;
        public float femme3 = 4.737f;
        public float femme4 = 667.051f;

        public float homme1 = 13.707f;
        public float homme2 = 492.3f;
        public float homme3 = 6.673f;
        public float homme4 = 77.607f;

        public float sedentaire = 1.375f;
        public float legere = 1.56f;
        public float moderer = 1.64f;
        public float intense = 1.82f;
        public float basal = 1;

        public float activiter;
        public double objectifP;

        public int age;
        public float poids;
        public float taille;
        public double poids1;
        public double tailleC;

        public double objectifR;
        public double hommeMBR;
        public double femmeMBR;
        public ResultatPage()
        {
            InitializeComponent();



        }


        protected override async void OnAppearing()
        {
            base.OnAppearing();

            listSel.ItemsSource = await App.Database.GetPersoAsync();
            pickerMe.ItemsSource = await App.Database.GetNameAsync();

            pickerMe.SelectedIndexChanged += pickerMe_OnSelectedIndexChanged;



        }

 
    


        async private void pickerMe_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            if (pickerMe.SelectedIndex == -1)
            {
                await DisplayAlert("Salut", "tu es qui ?", "OK");
                return;
            }

            var perso = await App.Database.GetPersoAsync();
            var nameselect = (Infos)pickerMe.SelectedItem;


            foreach (var y in perso)
            {

                if (Convert.ToString(y.Name) == Convert.ToString(nameselect.Name))
                {

                    age = Convert.ToInt32(y.Age);

                    double taille;
                    taille = Convert.ToInt32(y.Taille);
                    taille *= 0.01;

                    poids1 = Convert.ToInt32(y.Poids);
                    float poids = Convert.ToSingle(poids1);


                    switch (Convert.ToString(y.ValLifeStyle))
                    {
                        case "basal":
                            activiter = basal;
                            break;
                        case "sedentaire":
                            activiter = sedentaire;
                            break;
                        case "legere":
                            activiter = legere;
                            break;
                        case "moderer":
                            activiter = moderer;
                            break;
                        case "intense":
                            activiter = intense;
                            break;
                    }

                    switch (Convert.ToString(y.ValObjectif))
                    {
                        case "perdre":
                            objectifP = perdre;
                            break;
                        case "perdreP":
                            objectifP = perdreP;
                            break;
                        case "maintenir":
                            objectifP = maintenir;
                            break;
                        case "prendreM":
                            objectifP = prendreM;
                            break;
                    }




                    if (Convert.ToString(y.ValSex) == "femme")
                    {
                       
                        float femme = femme1 * poids + femme2 * Convert.ToSingle(taille) - femme3 * age + femme4;
                        float RF = femme * activiter;
                        double femmeR = Math.Round(RF, 2);
                        double objectifR = femmeR * objectifP;
                        textMB.Text = Convert.ToString(Math.Round(femmeR, 0));
                        textR.Text = " Résultat du métabolisme de base pour toi: " + Convert.ToString(Math.Round(objectifR + femmeR, 0)) + " en Calories";
                        // 20 % proteine - 40 %lipide - 40 %glucide - 30 g de fibre /J
                        double macroP = objectifR + femmeR * (25 / (double)100);
                        double macroL = objectifR + femmeR * (25 / (double)100);
                        double macroG = objectifR + femmeR * (50 / (double)100);
                        //double fibre = macroG * (30 / (double)100);

                        textMacro.Text = Convert.ToString(Math.Round((macroP / 4), 0)) + " g de Porteines | "
                                            + Convert.ToString(Math.Round((macroL / 9), 0)) + " g de Lipides | "
                                            + Convert.ToString(Math.Round((macroG / 4), 0)) /*- Math.Round((fibre / 1.9), 0))*/ + " g de Glucides | "
                                            /*+ Convert.ToString(Math.Round((fibre/1.9), 0)) + " g de Fibres"*/;
                        textICI.Text = Convert.ToString(y.ValSex) + " " + Convert.ToString(y.ValObjectif) + " " + Convert.ToString(y.ValLifeStyle) + " " + Convert.ToString(y.Taille) + " " + Convert.ToString(y.Poids) + " " + Convert.ToString(y.Age);
                        double femmeMBR = objectifR + femmeR;





                        ////IMG % = (1.20*IMC)+(0.23*AGE)-(10.8*Sexe)-5.4 | La première formule de Deurenberg : homme 1 et femme 0 pour sexe
                        ////femme Inférieur à 25% 	Trop maigre - Entre 25% et 30% 	Pourcentage normal - Supérieur à 30% 	Trop de graisse
                        double tailleC = Math.Pow(taille, 2);
                        double IMC = poids / tailleC;
                        double IMG = (1.20 * IMC) + (0.23 * age) - (10.8 * 0) - 5.4;

                        ResultatIMC();

                        if (IMG < 25) 
                        {
                            IMGR = " Tu es trop maigre.";
                        }
                        else if ((IMG >= 25) && (IMG <= 30))
                        {
                            IMGR = " Tu es normal.";
                        }
                        else if ((IMG > 30) && (IMG == 0))
                        {
                            IMGR = " Tu es a trop de graisse.";
                        };

                        textIMG.Text = " Votre IMG est de " + Math.Round(IMG, 0) + Convert.ToString(IMGR);

                        await App.Database.SaveInfosAsync(new Infos
                        {
                            ID = y.ID,
                            Name = y.Name,
                            IMC = Math.Round(IMC, 1),
                            MacroP = Math.Round((macroP / 4), 0),
                            MacroG = Math.Round((macroG / 4), 0),
                            MacroL = Math.Round((macroL / 9), 0),
                            IMG = Math.Round(IMG, 0),
                            MBasal = Math.Round(femmeR, 0),
                            Age = y.Age,
                            Poids = y.Poids,
                            Taille = y.Taille,
                            ValLifeStyle = y.ValLifeStyle,
                            ValSex = y.ValSex,
                            ValObjectif = y.ValObjectif,
                            MBresult = Convert.ToDouble(Math.Round(femmeMBR, 0)),

                        }); ;





                    }
                    else if (Convert.ToString(y.ValSex) == "homme")
                    {
                        
                        float homme = homme1 * poids + homme2 * Convert.ToSingle(taille) - homme3 * age + homme4;
                        float RH = homme * activiter;
                        double hommeR = Math.Round(RH, 2);
                        double objectifR = hommeR * objectifP;
                        textMB.Text = Convert.ToString(Math.Round(hommeR, 0));
                        textR.Text = "Résultat du métabolisme de base pour toi : " + Convert.ToString(Math.Round(objectifR + hommeR, 0)) + " en Calories";
                        // 20 % proteine - 40 %lipide - 40 %glucide - 30 g de fibre /J
                        double macroP = objectifR + hommeR * (25 / (double)100);
                        double macroL = objectifR + hommeR * (25 / (double)100);
                        double macroG = objectifR + hommeR * (50 / (double)100);
                        //double fibre =  macroG * (30 / (double)100);

                        textMacro.Text = Convert.ToString(Math.Round((macroP / 4), 0)) + " g de Porteines | "
                                            + Convert.ToString(Math.Round((macroL / 9), 0)) + " g de Lipides | "
                                            + Convert.ToString(Math.Round((macroG / 4), 0)) /*- Math.Round((fibre / 1.9), 0))*/ + " g de Glucides | "
                                            /*+ Convert.ToString(Math.Round((fibre/1.9), 0)) + " g de Fibres"*/;
                        textICI.Text = Convert.ToString(y.ValSex) + " " + Convert.ToString(y.ValObjectif) + " " + Convert.ToString(y.ValLifeStyle) + " " + Convert.ToString(y.Taille) + " " + Convert.ToString(y.Poids) + " " + Convert.ToString(y.Age);
                        double hommeMBR = objectifR + hommeR;



                        ////IMG % = (1.20*IMC)+(0.23*AGE)-(10.8*Sexe)-5.4 | La première formule de Deurenberg : homme 1 et femme 0 pour sexe            
                        ////homme Inférieur à 15% 	Trop maigre - Entre 15% et 20% 	Pourcentage normal - Supérieur à 20% 	Trop de graisse


                        double tailleC = Math.Pow(taille, 2);
                        double IMC = poids / tailleC;
                        double IMG = (1.20 * IMC) + (0.23 * age) - (10.8 * 1) - 5.4;

                        ResultatIMC();


                        if (IMG < 15)
                        {
                            IMGR = " Tu es trop maigre.";
                        }
                        else if ((IMG >= 15) && (IMG <= 20))
                        {
                            IMGR = " Tu es normal.";
                        }
                        else if (IMG > 20) 
                        {
                            IMGR = " Tu es trop de graisse.";
                        };
                        textIMG.Text = " Ton IMG est de " + Math.Round(IMG, 0) + Convert.ToString(IMGR);




                        await App.Database.SaveInfosAsync(new Infos
                        {
                            ID = y.ID,
                            Name = y.Name,
                            IMC = Math.Round(IMC, 1),
                            MacroP = Math.Round((macroP / 4), 0),
                            MacroG = Math.Round((macroG / 4), 0),
                            MacroL = Math.Round((macroL / 9), 0),
                            IMG = Math.Round(IMG, 0),
                            MBasal = Math.Round(hommeR),
                            Age = y.Age,
                            Poids = y.Poids,
                            Taille = y.Taille,
                            ValLifeStyle = y.ValLifeStyle,
                            ValSex = y.ValSex,
                            ValObjectif = y.ValObjectif,
                            MBresult = Convert.ToDouble(Math.Round(hommeMBR, 0)),
                        }) ;


                    }


                    void ResultatIMC()
                    {
                        //IMC ------------------------------------------------
                        //Indice de masse corporelle – formule = kg / m2
                        //- de 16,5 denutrition / 16.5 a 18.5 maigreur / 18.5 a 25 corpulence normale / 25 a 30 surpoids / 30 a 35 obesite moderer / 35 a 40 obesite severe / plus de 40 obesité morbide ou massive/ 

                        double tailleC = Math.Pow(taille, 2);
                        double IMC = poids / tailleC;

                        if (IMC < 16.5)
                        {
                            IMCR = "Dénutrition";
                        }
                        else if ((IMC >= 16.5) && (IMC <= 18.5))
                        {
                            IMCR = "Maigreur";
                        }
                        else if ((IMC >= 18.5) && (IMC <= 25))
                        {
                            IMCR = "Corpulence normale";
                        }
                        else if ((IMC >= 25) && (IMC <= 30))
                        {
                            IMCR = "Surpoids";
                        }
                        else if ((IMC >= 30) && (IMC <= 35))
                        {
                            IMCR = "Obésité modéré";
                        }
                        else if ((IMC >= 35) && (IMC <= 40))
                        {
                            IMCR = "Obésité sévère";
                        }
                        else if (IMC > 40)
                        {
                            IMCR = "Obésidé morbide";
                        };

                       
                        textIMC.Text = "Votre IMC est de " + Convert.ToString(Math.Round(IMC, 0)) + " " + Convert.ToString(IMCR);
                    }




                    break;



                }

                

            }
            




        }







    }
}

           

        

