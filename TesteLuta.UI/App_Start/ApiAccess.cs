using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using TesteLuta.UI.Models;

namespace TesteLuta.UI.App_Start
{
    public class ApiAccess
    {
        public List<Fighter> AllFighter { get; private set; }
        public Fighter Fighter { get; private set; }

        public ApiAccess()
        {
            AllFighter = GetAllFighters().Result;
        }
        public ApiAccess(int id):this()
        {
            Fighter = AllFighter.FirstOrDefault(a => a.Id == id);
        }


        private async Task<List<Fighter>> GetAllFighters()
        {
            var url = ConfigurationManager.AppSettings["url"];
            var apiKey = ConfigurationManager.AppSettings["x-api-key"];

            using (var client = new HttpClient())
            {
                using (var requestMessage = new HttpRequestMessage(HttpMethod.Get, url))
                {
                    requestMessage.Headers.Add("x-api-key", apiKey);
                    var response = await client.SendAsync(requestMessage)
                        .ConfigureAwait(false);

                    if (response.StatusCode != System.Net.HttpStatusCode.OK)
                        throw new Exception("Não foi possível obter os dados dos lutadores.");

                    var json = response.Content.ReadAsStringAsync().Result;                    
                    return JsonConvert.DeserializeObject<List<Fighter>>(json);
                }
            }
        }
        private Fighter GetFighter(int id)
        {
            return GetAllFighters().Result.FirstOrDefault(a => a.Id == id);
        }
    }
}