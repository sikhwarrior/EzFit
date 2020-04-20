using EzFit.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EzFit.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NutriPage : ContentPage
    {

        public NutriPage()
        {
            InitializeComponent();

            listNutri.ItemSelected += (sender, e) =>
            {
                if (listNutri.SelectedItem != null)
                {
                    Aliments item = listNutri.SelectedItem as Aliments;
                    DisplayAlert(item.Protein, " (g/100g) Protein", "OK");
                    listNutri.SelectedItem = null;
                    //Protein, Carbohydrate, Fibres
                }
            };




            listNutri.RefreshCommand = new Command((obj) =>
            {
                DownloadData((aliments) =>
                {
                    listNutri.ItemsSource = aliments;
                    listNutri.IsRefreshing = false;

                });


            });

            listNutri.IsVisible = false;
            waitLayout.IsVisible = true;

            DownloadData((aliments) =>
            {
                listNutri.ItemsSource = aliments;


                listNutri.IsVisible = true;
                waitLayout.IsVisible = false;

            });

        }



         void DownloadData(Action<List<Aliments>> action)
        {
            const string URL = "https://aitek.tech/repas.json";

            using (var webclient = new WebClient())
            {
       

                    webclient.DownloadStringCompleted += (object sender, DownloadStringCompletedEventArgs e) =>
                    {
                        try
                        {

                            string alimentsJson = e.Result;

                        List<Aliments> aliments = JsonConvert.DeserializeObject<List<Aliments>>(alimentsJson);

                        Device.BeginInvokeOnMainThread(() =>
                        {

                            action.Invoke(aliments);
                        });
                        }
                        catch (Exception ex)
                        {
                            Device.BeginInvokeOnMainThread(() =>
                            {
                                DisplayAlert("Erreur", ex.Message, "ok");
                                action.Invoke(null);

                            });
                            return;
                        }
                    };

                    webclient.DownloadStringAsync(new Uri(URL));

        



            }


        }





    }

}