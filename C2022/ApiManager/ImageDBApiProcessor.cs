using DBAccess.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Nodes;

namespace C2022.ApiManager
{
    public class ImageDBApiProcessor
    {
        private readonly HttpConfig httpWorker;

        public ImageDBApiProcessor()
        {
            httpWorker = new HttpConfig();
        }

        public Image getImageByCategory(string type, string category)
        {

            try
            {
                string url = SearchRandomImage(type, category);
                string response = httpWorker.GetJsonResponse(url).Result;

                List<Image> result = ParseSearchingModel(response);

                return result.FirstOrDefault();

            }
            catch
            {
                throw new Exception($"Search is not valid: {category}");
            }
        }
        public List<Image> getImageByCategoryMany(string type, string category)
        {

            string url = SearchInBulk(type, category);
            string response = httpWorker.GetJsonResponse(url).Result;
            return ParseSearchingModels(response);
        }
       
        private string SearchRandomImage(string type, string category) => @$"https://api.waifu.pics/{type}/{category}";
        private string SearchInBulk(string type, string category) => @$"https://api.waifu.pics/many/{type}/{category}";

        //get single image
        private List<Image>? ParseSearchingModel(string json)
        {
            List<Image> searchingModels = new List<Image>();
            JObject images = JObject.Parse(json);

            var imagesArray =
                from c in images["url"]
                select c;

            foreach (var item in imagesArray)
            {
                Image s = new Image { Link = (string)item["url"] };
                searchingModels?.Add(s);
            }

            if (searchingModels == null)
                return null;

            return searchingModels;
        }
        //get 30 image links
        private List<Image>? ParseSearchingModels(string json)
        {
            JsonObject obj = JsonNode.Parse(json).AsObject();
            JsonArray jsonArray = (JsonArray)obj["files"];
            string jsonString = jsonArray.ToString();
            List<string> listS = JsonConvert.DeserializeObject<List<string>>(jsonString);
            List<Image> searching = new List<Image>();

            foreach (string link in listS)
            {
                searching.Add(new Image { Link = link });
            }

            if (searching == null)
                return null;

            return searching;
        }
    }
}
