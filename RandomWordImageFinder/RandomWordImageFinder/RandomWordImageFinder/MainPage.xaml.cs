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
        

        static string[] words = new string[] {"apple", "banana", "orange", "dog", "cat", "weenies", "ricardo milos", "rainbow ranger", "magic", "spongebob", "joe", "cry", "lunch", "tank", "school", "hotel", "knot", "work", "giant", "minecraft", "zombie", "skeleton", "creeper", "gaystation", "gabe newell", "bill gates", "five guys", "obama", "trump", "hillary", "bernie", "language", "code", "car", "truck", "road", "gas", "golf", "football", "soccer", "tennis", "basketball", "baseball", "america", "russia", "germany", "united kingdom", "ireland", "italy", "forsenHobo", "xqcM"};
        static List<string> urls = new List<string>();

        static string h = "";

        public MainPage()
        {
            InitializeComponent();
        }

        public static void GetWord(string s)
        {
            if (s == null)
            {
                Random rng = new Random();
                int x = rng.Next(0, words.Length);
                word = words[x];
            }
            else
            {
                word = s;
            }
        }

        public string getWord()
        {
            return word;
        }

        public string getUrl()
        {
            return url;
        }

        private string GetCode(string s)
        {
            GetWord(s);

            string url = "https://www.google.com/search?q=" + word + "&tbm=isch";
            string data = "";

            var request = (HttpWebRequest)WebRequest.Create(url);
            request.Accept = "text/html, application/xhtml+xml, */*";
            request.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64; Trident/7.0; rv:11.0) like Gecko";

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

            int ndx = html.IndexOf("\"ou\"", StringComparison.Ordinal);

            while (ndx >= 0)
            {
                ndx = html.IndexOf("\"", ndx + 4, StringComparison.Ordinal);
                ndx++;
                int ndx2 = html.IndexOf("\"", ndx, StringComparison.Ordinal);

                string url = html.Substring(ndx, ndx2 - ndx);
                urls.Add(url);

                ndx = html.IndexOf("\"ou\"", ndx2, StringComparison.Ordinal);
            }

            return urls;
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            if (edt.Text == "" || edt.Text == " " || edt.Text == "Enter Word")
            {
                urls = GetUrls(GetCode(null));
            }
            else
            {
                h = edt.Text;
                urls = GetUrls(GetCode(h));
            }
            Random rng = new Random();
            int urlNum = 0;
            if (getWord() == "xqcM")
            {
                urlNum = rng.Next(0, 10);
            }
            else if (getWord() == "forsenHobo")
            {
                urlNum = rng.Next(0, 10);
            }
            else
            {
                urlNum = rng.Next(0, urls.Count - 1);
            }
            url = urls[urlNum];

            Navigation.PushAsync(new WordPage());
        }
    }
}
