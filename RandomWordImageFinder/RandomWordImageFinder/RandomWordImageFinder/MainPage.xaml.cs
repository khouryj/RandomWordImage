using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace RandomWordImageFinder
{
    public partial class MainPage : ContentPage
    {
        public static string word = "";
        public static string url = "";
        

        static string[] words = new string[] {"apple", "banana", "orange", "dog", "cat"};
        static List<string> urls = new List<string>();

        public MainPage()
        {
            InitializeComponent();
        }

        public static void GetWord()
        {
            Random rng = new Random();
            int x = rng.Next(0, words.Length - 1);
            word = words[x];
        }

        public string getWord()
        {
            return word;
        }

        public string getUrl()
        {
            return url;
        }

        private string GetCode()
        {
            GetWord();

            string url = "https://www.google.com/search?q=" + word + "&tbm=isch";
            string data = "";

            var request = (HttpWebRequest)WebRequest.Create(url);
            var response = (HttpWebResponse)request.GetResponse();

            using(Stream dataStream = response.GetResponseStream())
            {
                if (dataStream == null)
                {
                    return "";
                }
                using (var reader = new StreamReader(dataStream))
                {
                    data = reader.ReadToEnd();
                }
            }

            return data;
        }

        private List<string> GetUrls(string html)
        {
            var urls = new List<string>();

            int ndx = html.IndexOf("class=\"images_table\"", StringComparison.Ordinal);
            ndx = html.IndexOf("<img", ndx, StringComparison.Ordinal);

            while (ndx >= 0)
            {
                ndx = html.IndexOf("src=\"", ndx, StringComparison.Ordinal);
                ndx += 5;
                int ndx2 = html.IndexOf("\"", ndx, StringComparison.Ordinal);

                string url = html.Substring(ndx, ndx2 - ndx);
                urls.Add(url);

                ndx = html.IndexOf("<img", ndx, StringComparison.Ordinal);
            }

            return urls;
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            urls = GetUrls(GetCode());
            Random rng = new Random();
            int urlNum = rng.Next(0, urls.Count - 1);
            url = urls[urlNum];


        }
    }
}
