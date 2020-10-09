using Items;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ConsumeRest
{
    class ConsumeWorker
    {
        private const string URI = "http://item-service.azurewebsites.net/api/localItems/";
        public async void start()
        {
            IList<Item> l = await GetAllItemsAsync();
            foreach (var item in l)
            {
                Console.WriteLine(item);
            }

            Item item1 = await GetOneItemsAsync(3);
            Console.WriteLine(item1);
            Item item2 = new Item(8, "string", 10);
            Item item3 = new Item(8, "string", 15);
            PostItemAsync(item2);
            PutItemAsync(item3, 8);

            DeleteItemsAsync(8);


        }
        public async Task<IList<Item>> GetAllItemsAsync()
        {
            using (HttpClient client = new HttpClient())
            {
                string content = await client.GetStringAsync(URI);
                IList<Item> cList = JsonConvert.DeserializeObject<IList<Item>>(content);
                return cList;
            }
        }

        public async Task<Item> GetOneItemsAsync(int id)
        {
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(URI + id);

                string content = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode)
                {
                    Item i = JsonConvert.DeserializeObject<Item>(content);
                    return i;
                }
                //else
                throw new KeyNotFoundException($"Status code={response.StatusCode} Message={content}");
            }
        }
        public async void PostItemAsync(Item i)
        {
            using (HttpClient client = new HttpClient())
            {
                String jsonStr = JsonConvert.SerializeObject(i);
                StringContent content = new StringContent(jsonStr, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PostAsync(URI, content);

                if (response.IsSuccessStatusCode)
                {
                    return;
                }
                throw new ArgumentException("opret fejlede");
            }
        }
        public async void PutItemAsync(Item i ,int id)
        {
            using (HttpClient client = new HttpClient())
            {
                String jsonStr = JsonConvert.SerializeObject(i);
                StringContent content = new StringContent(jsonStr, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PutAsync(URI + id, content);
                if (response.IsSuccessStatusCode)
                {
                    return;
                }
                throw new ArgumentException("opdater fejlede");
            }
        }

        public async void DeleteItemsAsync(int id)
        {
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.DeleteAsync(URI + id);

                string content = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode)
                {
                    return;
                }
                //else
                throw new ArgumentException("slet fejlede");
            }
        }
    }
}
