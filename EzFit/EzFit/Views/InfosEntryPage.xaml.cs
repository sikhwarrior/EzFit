using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EzFit.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;


namespace EzFit.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class InfosEntryPage : ContentPage
    {


        public InfosEntryPage()
        {
            InitializeComponent();

            listActiviter.ItemsSource = new List<Infos>
            {
                 new Infos { ValLifeStyle = "basal", ValVisuel = "Métabolisme de base."},
                 new Infos { ValLifeStyle = "sedentaire", ValVisuel = "Vous avez un travail de bureau et une faible dépense sportive."},
                 new Infos { ValLifeStyle = "legere", ValVisuel = "Vous vous entraînez 1 à 3 fois par semaine."},
                 new Infos { ValLifeStyle = "moderer", ValVisuel = "Vous vous entraînez 4 à 6 fois par semaine."},
                 new Infos { ValLifeStyle = "intense", ValVisuel = "Vous vous entraînez plus de 6 fois par semaine."}
             };

            pickerSex.ItemsSource = new List<Infos>
            {
                 new Infos { ValSex = "femme", ValVisuel = "Femme"},
                 new Infos { ValSex = "homme", ValVisuel = "Homme"},

             };

            listObjectif.ItemsSource = new List<Infos>
            {
                 new Infos { ValObjectif = "perdre", ValVisuel = "Perdre du Poids."},
                 new Infos { ValObjectif = "perdreP", ValVisuel = "Perdre 5 kg et maintenir les muscles."},
                 new Infos { ValObjectif = "maintenir", ValVisuel = "Maintenir votre poids."},
                 new Infos { ValObjectif = "prendreM", ValVisuel = "Prendre du muscle."},
             };
        }



        private void pickerSex_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            var stylesex = (Infos)pickerSex.SelectedItem;
            //await DisplayAlert("test", $"{stylesex.ValSex}", "OK");

        }

        async private void listObjectif_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            var styleobj = (Infos)listObjectif.SelectedItem;
            //await DisplayAlert("test", $"{styleobj.ValObjectif}", "OK");

        }



        async private void listActiviter_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            var stylevie = (Infos)listActiviter.SelectedItem;
            //await DisplayAlert("test", $"{stylevie.ValLifeStyle}", "OK");

        }

        async void EnregistrerButtonClicked(object sender, EventArgs e)
        {
    

            var stylevie = (Infos)listActiviter.SelectedItem;
            var stylesex = (Infos)pickerSex.SelectedItem;
            var styleobj = (Infos)listObjectif.SelectedItem;

            if (!string.IsNullOrWhiteSpace(nameEntry.Text) && !string.IsNullOrWhiteSpace(ageEntry.Text) && !string.IsNullOrWhiteSpace(poidsEntry.Text) && !string.IsNullOrWhiteSpace(tailleEntry.Text))
            {
                await App.Database.SaveInfosAsync(new Infos
                {
                    Name = nameEntry.Text,
                    Age = int.Parse(ageEntry.Text),
                    Poids = int.Parse(poidsEntry.Text),
                    Taille = int.Parse(tailleEntry.Text),
                    ValLifeStyle = stylevie.ValLifeStyle,
                    ValSex = stylesex.ValSex,
                    ValObjectif = styleobj.ValObjectif,
                });

            }

        }

       
    }

}