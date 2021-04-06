using System;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json.Linq;

namespace HTTP{
    public static class Response{
        public static JObject returnResponse(string URL, string urlParameters) {
        HttpClient client =  new HttpClient();
        client.BaseAddress = new Uri(URL);
        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        HttpResponseMessage response = client.GetAsync(urlParameters).Result;
        var GdataObjects = response.Content.ReadAsStringAsync().Result;
        JObject joResponse = JObject.Parse(GdataObjects);
        client.Dispose();
        return joResponse;
    }
    }
}
