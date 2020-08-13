using FrontEndUI.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace FrontEndUI
{
    class SWAPIService
    {
        private readonly HttpClient _httpClient = new HttpClient();

        private static string APIUrl = "https://localhost:44357/api/Artist/";
        public static async Task GetDataWithAuthentication()
        {
            var authCredential = Encoding.UTF8.GetBytes("{userTest}:{passTest}");
            using (var client = new HttpClient())
            {


                client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Basic", Convert.ToBase64String(authCredential));
                client.BaseAddress = new Uri(APIUrl);
                HttpResponseMessage response = await client.GetAsync(APIUrl + "GetOrders");

                if (response.IsSuccessStatusCode)
                {
                    var readTask = response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    var rawResponse = readTask.GetAwaiter().GetResult();
                    Console.WriteLine(rawResponse);
                }
                Console.WriteLine("Complete");
            }
        }
        //Get All Artists
        public async Task<List<Artist>> GetArtistsAsync()
        {
            GetDataWithAuthentication();
            HttpResponseMessage response = await _httpClient.GetAsync("https://localhost:44357/api/Artist");     

            if (response.IsSuccessStatusCode)
            {
                List<Artist> artists = await response.Content.ReadAsAsync<List<Artist>>();
                return artists;
            }
            //below covers if the if statement doesnt work (not really best practice in the long run)
            return new List<Artist>();
        }




        //public async Task<Vehicle> GetVehicleAsync(string url)
        //{
        //    HttpResponseMessage response = await _httpClient.GetAsync(url);
        //    if (response.IsSuccessStatusCode)
        //    {
        //        Vehicle vehicle = await response.Content.ReadAsAsync<Vehicle>();
        //        return vehicle;
        //    }
        //    //below covers if the if statement doesnt work (not really best practice in the long run)
        //    return null;

        ////}

        //public async Task<T> GetAsync<T>(string url)
        //{
        //    HttpResponseMessage response = await _httpClient.GetAsync(url);
        //    if (response.IsSuccessStatusCode)
        //    {
        //        return await response.Content.ReadAsAsync<T>();

        //    }
        //    return default;
        //}

        //public async Task<SearchResult<Artist>> GetArtistSearchAsync(string query)
        //{
        //    HttpResponseMessage response = await _httpClient.GetAsync("http://swapi.dev/api/people/?search=" + query);
        //    if (response.IsSuccessStatusCode)
        //    {
        //        return await response.Content.ReadAsAsync<SearchResult<Person>>();
        //    }
        //    return null;

        //}

        public async Task<SearchResult<T>> GetSearchAsync<T>(string category, string query)
        {
            HttpResponseMessage response = await _httpClient.GetAsync("https://localhost:44357/api/" + category + "/" + query);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsAsync<SearchResult<T>>();
            }
            return null;

        }
        public async Task<SearchResult<Artist>> GetArtistSearch(string query)
        {
            return await GetSearchAsync<Artist>("Artist", query);
        }

        public async Task<SearchResult<Album>> GetAlbumSearch(string query)
        {
            return await GetSearchAsync<Album>("Album", query);
        }

        public async Task<SearchResult<Song>> GetSongSearch(string query)
        {
            return await GetSearchAsync<Song>("Song", query);
        }

    }
}

