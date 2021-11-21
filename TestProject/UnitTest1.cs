using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using WebApi;
using Xunit;

namespace TestProject
{
    public class UnitTest1
    {
        HttpClient client;
        public UnitTest1()
        {
            client = new TestServer(new WebHostBuilder().UseStartup<Startup>()).CreateClient();
        }

        private async Task<string> getAllKeys()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "/testapi/GetAllKeys");

            var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            return content;
        }


        [Fact]
        public async Task GetAllKeys() => await getAllKeys();

        [Theory]
        [InlineData("Key1","Value1")]
        public async Task AddItem(string key, string value)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, $"testapi/AddItem?key={key}&value={value}");

            var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal(value, content);

            Assert.Contains(key, await getAllKeys());
        }

        [Theory]
        [InlineData("Key2", "Value2")]
        public async Task DeleteItem(string key, string value)
        {
            AddItem(key, value).Wait();

            var request = new HttpRequestMessage(HttpMethod.Delete, $"testapi/DeleteValue?key={key}");
            var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal(value, content);
            Assert.DoesNotContain(key, await getAllKeys());
        }

        [Theory]
        [InlineData("Key3", "Value3")]
        public async Task GetItem(string key, string value)
        {
            AddItem(key, value).Wait();

            var request = new HttpRequestMessage(HttpMethod.Get, $"testapi/GetValue?key={key}");

            var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal(value, content);
            if(value != null)
                Assert.Contains(key, await getAllKeys());
        }
    }
}
