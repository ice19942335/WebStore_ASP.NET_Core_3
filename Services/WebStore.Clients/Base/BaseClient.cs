using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Primitives;

namespace WebStore.Clients.Base
{
    public abstract class BaseClient: IDisposable
    {
        protected readonly HttpClient Client;

        protected readonly string _ServiceAddress;

        protected BaseClient(IConfiguration configuration, string serviceAddress)
        {
            _ServiceAddress = serviceAddress;

            Client = new HttpClient { BaseAddress = new Uri(configuration["ClientAddress"])};
            Client.DefaultRequestHeaders.Accept.Clear();
            Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(configuration["ProduceDataType"]));
        }

        protected async Task<T> GetAsync<T>(string url, CancellationToken cancellationToken = default) where T : new()
        {
            var response = await Client.GetAsync(url, cancellationToken);
            if (response.IsSuccessStatusCode)
                return await response.Content.ReadAsAsync<T>(cancellationToken);
            return new T();
        }

        protected T Get<T>(string url) where T : new() => GetAsync<T>(url).Result;

        protected async Task<HttpResponseMessage> PostAsync<T>(string url, T item, CancellationToken cancellationToken = default)
        {
            return (await Client.PostAsJsonAsync(url, item, cancellationToken)).EnsureSuccessStatusCode();
        }

        protected HttpResponseMessage Post<T>(string url, T item) => PostAsync(url, item).Result;


        protected async Task<HttpResponseMessage> PutAsync<T>(string url, T item, CancellationToken cancellationToken = default)
        {
            return (await Client.PutAsJsonAsync(url, item, cancellationToken)).EnsureSuccessStatusCode();
        }

        protected HttpResponseMessage Put<T>(string url, T item) => PutAsync(url, item).Result;

        protected async Task<HttpResponseMessage> DeleteAsync(string url, CancellationToken cancellationToken = default)
        {
            return await Client.DeleteAsync(url, cancellationToken);
        }

        protected HttpResponseMessage Delete(string url) => DeleteAsync(url).Result;

        public void Dispose()
        {
            Client?.Dispose();
        }
    }
}