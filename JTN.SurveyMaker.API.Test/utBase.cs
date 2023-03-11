using Azure;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.OutputCaching;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.Extensions.Hosting;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Reflection;

namespace JTN.SurveyMaker.API.Test
{
    class APIProject : WebApplicationFactory<Program>
    {
        protected override IHost CreateHost(IHostBuilder builder)
        {
            //Sharing extra set up
            return base.CreateHost(builder);
        }
    }

    [TestClass]
    public abstract class utBase
    {
        public HttpClient client { get; }
        public Type type;

        public utBase()
        {
            var application = new APIProject();
            client = application.CreateClient();
            client.BaseAddress = new Uri(client.BaseAddress + "api/");
        }

        [TestMethod]
        public async Task LoadTestAsync<T>()
        {
            dynamic items;
            var response = await client.GetStringAsync(typeof(T).Name);
            items = (JArray)JsonConvert.DeserializeObject(response);
            List<T> values = items.ToObject<List<T>>();

            Assert.IsTrue(values.Count > 0);
        }

        [TestMethod]
        public async Task InsertTestAsync<T>(T item)
        {
            bool rollback = true;
            string serializedObject = JsonConvert.SerializeObject(item);
            var content = new StringContent(serializedObject);
            content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
            HttpResponseMessage response = client.PostAsync(typeof(T).Name + "/" + rollback, content).Result;
            string result = response.Content.ReadAsStringAsync().Result;

            //Remove \s from the result
            result = result.Replace("\"", "");
            Guid guid = Guid.Parse(result);

            //Checks that returned Guid is not an empty guid
            Assert.IsFalse(guid.Equals(Guid.Empty));
        }

        [TestMethod]
        public async Task DeleteTestAsync<T>(KeyValuePair<string, string> filter)
        {
            Guid id = await GetId<T>(filter);
            bool rollback = true;
            HttpResponseMessage response = client.DeleteAsync(typeof(T).Name + "/" + id + "/" + rollback).Result;
            string result = response.Content.ReadAsStringAsync().Result;
            Assert.IsTrue(Convert.ToInt32(result) > 0);

        }

        private async Task<Guid> GetId<T>(KeyValuePair<string, string> filter)
        {
            //Return id of filter combination (ie Vin Id)
            string result;
            dynamic items;

            var response1 = await client.GetStringAsync(typeof(T).Name);
            items = (JArray)JsonConvert.DeserializeObject(response1);
            List<T> values = items.ToObject<List<T>>();
            string key = filter.Key;
            string value = filter.Value;

            var field = values[0].GetType().GetProperty(key);
            Guid id = Guid.Empty;

            foreach (T v in values)
            {
                //is data types value = value. ie Vin value = value
                if (v.GetType().GetProperty(key).GetValue(v, null).ToString() == value)
                {
                    id = (Guid)v.GetType().GetProperty("Id").GetValue(v, null);
                }
            }

            return id;
        }

        [TestMethod]
        public async Task LoadByIdTestAsync<T>(KeyValuePair<string, string> filter)
        {
            Guid id = await GetId<T>(filter);
            dynamic items;
            var response = client.GetStringAsync(typeof(T).Name + "/" + id).Result;
            items = JsonConvert.DeserializeObject(response);
            T item = items.ToObject<T>();

            Assert.AreEqual(id, item.GetType().GetProperty("Id").GetValue(item, null));
        }

        [TestMethod]
        public async Task UpdateTestAsync<T>(KeyValuePair<string, string> filter, T item)
        {
            var response1 = await client.GetStringAsync(typeof(T).Name);
            dynamic items = (JArray)JsonConvert.DeserializeObject(response1);
            List<T> values = items.ToObject<List<T>>();
            string key = filter.Key;
            string value = filter.Value;
            var field = values[0].GetType().GetProperty(key);
            Guid id = Guid.Empty;
            foreach (T v in values)
            {
                if (v.GetType().GetProperty(key).GetValue(v, null).ToString() == value)
                {
                    id = (Guid)v.GetType().GetProperty("Id").GetValue(v, null);
                    PropertyInfo prop = v.GetType().GetProperty("Id");
                    prop.SetValue(item, id, null);
                    break;
                }
            }
            bool rollback = true;
            string serializedItem = JsonConvert.SerializeObject(item);
            var content = new StringContent(serializedItem);
            content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
            var response = client.PutAsync(typeof(T).Name + "/" + id + "/" + rollback, content).Result;
            var result = response.Content.ReadAsStringAsync().Result;
            Assert.IsTrue(Convert.ToInt32(result) > 0);
        }
    }


}
