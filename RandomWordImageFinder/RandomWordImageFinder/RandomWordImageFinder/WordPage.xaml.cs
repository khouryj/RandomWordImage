using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RandomWordImageFinder
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class WordPage : ContentPage
	{
		public WordPage ()
		{
            InitializeComponent();

            lbl.Text = new MainPage().getWord();
            BackgroundImage = new MainPage().getUrl();
        }

        /*
         To do:
         -Make it a separate image instead of background or fix blurriness
         -Add more words
         -More app detail
        */
	}
}