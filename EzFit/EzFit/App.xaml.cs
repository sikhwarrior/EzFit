using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.IO;
using SQLite;
using Plugin.Iconize;
using EzFit.Views;

namespace EzFit
{
    public partial class App : Application
    {
        static InfosDatabase database;
        public static InfosDatabase Database
        {
            get
            {
                if (database == null)
                {
                    database = new InfosDatabase();
                }
                return database;
            }
        }

        public App()
        {
            InitializeComponent();

            Plugin.Iconize.Iconize.With(new Plugin.Iconize.Fonts.EntypoPlusModule())
                      .With(new Plugin.Iconize.Fonts.FontAwesomeRegularModule())
                      .With(new Plugin.Iconize.Fonts.FontAwesomeBrandsModule())
                      .With(new Plugin.Iconize.Fonts.FontAwesomeSolidModule())
                      .With(new Plugin.Iconize.Fonts.IoniconsModule())
                      .With(new Plugin.Iconize.Fonts.MaterialDesignIconsModule());
               // .With(new Plugin.Iconize.Fonts.MaterialModule())
            //.With(new Plugin.Iconize.Fonts.MeteoconsModule())
            //.With(new Plugin.Iconize.Fonts.SimpleLineIconsModule())
            //.With(new Plugin.Iconize.Fonts.TypiconsModule())
            //.With(new Plugin.Iconize.Fonts.WeatherIconsModule());


            MainPage = new NavigationPage(new MainPage());

        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
