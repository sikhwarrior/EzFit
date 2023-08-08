using EzFit.Models;
using Microcharts;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;
using Entry = Microcharts.Entry;

namespace EzFit.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GraphPage : ContentPage
    {

        public Chart Chart { get; private set; }

        public GraphPage()
        {
            InitializeComponent();
        }


        async private void me_OnSelectedIndexChanged(object sender, EventArgs e)
        {




        }

        protected override async void OnAppearing()
        {
            
            pickerMe.ItemsSource = await App.Database.GetNameAsync();
            base.OnAppearing();

            var entries = new[]
            {
                new Entry(25)
                {
                     Label = "trop maigre",
                     ValueLabel = "25",
                     Color = SKColor.Parse("#2c3e50")
                },
                 new Entry(30)
                {
                     Label = "normal",
                     ValueLabel = "30",
                     Color = SKColor.Parse("#77d065")
                },
                  new Entry(15)
                {
                     Label = "gros",
                     ValueLabel = "15",
                     Color = SKColor.Parse("#b455b6")
                },
                   new Entry(20)
                {
                     Label = "test",
                     ValueLabel = "20",
                     Color = SKColor.Parse("#3498db")
                },
                
          };


            //MyBarChart.Chart = new BarChart() { Entries = entries };

            MyLineChart.Chart = new LineChart() { Entries = entries };

            MyPointChart.Chart = new PointChart() { Entries = entries };

            MyRadialGaugeChart.Chart = new RadialGaugeChart() { Entries = entries };

            MyDonutChart.Chart = new DonutChart() { Entries = entries };

            //this.MyLineChart.Chart = chart;






        }




    }
}
      



