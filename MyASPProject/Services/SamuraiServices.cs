using MyASPProject.Models;
using Newtonsoft.Json;
using System.Text;

namespace MyASPProject.Services
{
    public class SamuraiServices : ISamurai
    {
        public async Task Delete(int id)
        {
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.DeleteAsync($"http://localhost:5001/api/Samurais/{id}"))
                {
                    if (response.StatusCode != System.Net.HttpStatusCode.OK)
                        throw new Exception($"Gagal untuk delete data");
                }
            }
        }

        public async Task<IEnumerable<Samurai>> GetAll()
        {
            List<Samurai> samurais = new List<Samurai>();
            using(var httpClient = new HttpClient())
            {
                using (var respone = await httpClient.GetAsync("http://localhost:5001/api/Samurais"))
                {
                    string apiResponse = await respone.Content.ReadAsStringAsync();
                    samurais = JsonConvert.DeserializeObject<List<Samurai>>(apiResponse);

                }
            }
            return samurais;
        }

        public async Task<IEnumerable<Weather>> GetAllWeather(string token)
        {
            List<Weather> lstweathers = new List<Weather>();
            using (var httpClient = new HttpClient())
            {
                //Memasukan Token
                httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", $"{token}");

                    
                using (var respone = await httpClient.GetAsync("https://localhost:6001/WeatherForecast"))
                {

                    if (respone.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        string apiResponse = await respone.Content.ReadAsStringAsync();
                        lstweathers = JsonConvert.DeserializeObject<List<Weather>>(apiResponse);
                    }
                    else
                    {
                        throw new Exception("Gagal Retrieve Data");
                    }
                    

                }
            }
            return lstweathers;
        }

        public async Task<Samurai> GetById(int id)
        {
            Samurai samurai = new Samurai();
            using (var httpClient = new HttpClient())
            {
                using (var respone = await httpClient.GetAsync($"http://localhost:5001/api/Samurais/{id}"))
                {
                    if(respone.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        string apiResponse = await respone.Content.ReadAsStringAsync();
                        samurai = JsonConvert.DeserializeObject<Samurai>(apiResponse);
                    }
        
                }
            }
            return samurai;
        }

        public async Task<IEnumerable<Samurai>> GetByName(string name)
        {
            List<Samurai> samurais = new List<Samurai>();
            using (var httpClient = new HttpClient())
            {
                using (var respone = await httpClient.GetAsync($"http://localhost:5001/api/Samurais/ByName?name={name}"))
                {
                    string apiResponse = await respone.Content.ReadAsStringAsync();
                    samurais = JsonConvert.DeserializeObject<List<Samurai>>(apiResponse);

                }
            }
            return samurais;
        }

        public async Task<IEnumerable<SamuraiWithQuote>> GetWithQuote()
        {
            List<SamuraiWithQuote> samurais = new List<SamuraiWithQuote>();
            using (var httpClient = new HttpClient())
            {
                using (var respone = await httpClient.GetAsync("http://localhost:5001/api/Samurais/WithQuotes"))
                {
                    string apiResponse = await respone.Content.ReadAsStringAsync();
                    samurais = JsonConvert.DeserializeObject<List<SamuraiWithQuote>>(apiResponse);

                }
            }
            return samurais;
        }

        public async Task<Samurai> Insert(Samurai obj)
        {
            Samurai samurai = new Samurai();
            using (var httpClient = new HttpClient())
            {
                StringContent content = new StringContent(
                    JsonConvert.SerializeObject(obj), Encoding.UTF8,"application/json");
                using (var response = await httpClient.PostAsync("http://localhost:5001/api/Samurais", content))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.Created)
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        samurai = JsonConvert.DeserializeObject<Samurai>(apiResponse);
                    }
                }
            }
            return samurai;
        }

        public Task<IEnumerable<SamuraiWithSwordTypeSwordElement>> SamuraiWithSwordTypeSwordElement()
        {
            throw new NotImplementedException();
        }

        public async Task<Samurai> Update(Samurai obj)
        {
            Samurai samurai = await GetById(obj.Id);
            if (samurai == null)
                throw new Exception($"Data Samurai dengan id {obj.Id} tidak ditemukan");
            StringContent content = new StringContent(JsonConvert.SerializeObject(obj),
                  Encoding.UTF8, "application/json");
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.PutAsync("http://localhost:5001/api/Samurais", content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    samurai = JsonConvert.DeserializeObject<Samurai>(apiResponse);
                }
            }
            return samurai;

        }
    }
}
