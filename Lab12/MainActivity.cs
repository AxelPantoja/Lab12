using Android.App;
using Android.Widget;
using Android.OS;
using CustomAdapters;
using System;
using System.Threading.Tasks;

namespace Lab12
{
    [Activity(Label = "Lab12", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        private TextView tvResultados;

        protected override async void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            //Variables de UI:
            var lvColors = FindViewById<ListView>(Resource.Id.lvColors);
            tvResultados = FindViewById<TextView>(Resource.Id.tvResultados);

            //Establece el adaptador al ListView:
            lvColors.Adapter = new ColorAdapter(this, Resource.Layout.LVItem,
                Resource.Id.tvName, Resource.Id.tvCode, Resource.Id.ivColor);

            //Validacion de la actividad:
            await Validar();
        }

        private async Task Validar()
        {
            var device = Android.Provider.Settings.Secure.GetString(ContentResolver,
                Android.Provider.Settings.Secure.AndroidId);
            var email = "";
            var password = "";

            //Llamar al servicio para registrar la actividad:
            var client = new SALLab12.ServiceClient();
            var response = await client.ValidateAsync(email, password, device);

            var result = $"{response.Status}\n{response.FullName}\n{response.Token}";

            tvResultados.Text = result;
        }
    }
}
