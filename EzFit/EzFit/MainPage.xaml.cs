using EzFit.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.IO;
using EzFit.Utils;


namespace EzFit
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {

        public MainPage()
        {
            InitializeComponent();
         
        }

        
        private void InfosButtonClicked(object sender, EventArgs e)
        {
            this.Navigation.PushAsync(new TabsPage());
        }

       
        private void ChartsButtonClicked(object sender, EventArgs e)
        {
            this.Navigation.PushAsync(new GraphPage());
        }
        private void NutriButtonClicked(object sender, EventArgs e)
        {
            this.Navigation.PushAsync(new NutriPage());
        }


    }
}
