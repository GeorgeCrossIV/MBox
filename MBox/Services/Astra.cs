using MBox.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Server.Features;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace MBox.Services
{
    public class Astra
    {
        public static string GetToken(string authUrl, string username, string password)
        {            
            var client = new RestClient(authUrl);
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("Content-Type", "application/json");
            request.AddParameter("application/json", "{\n    \"username\": \"" + username + "\",\n    \"password\": \"" + password + "\"\n}", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);

            return JObject.Parse(response.Content)["authToken"].ToString();
        }

        public static List<MBox.Models.User> GetUsers(IConfiguration config, string token)
        {
            UserRootobject data = new UserRootobject();
            List<User> users = new List<User>();
            string graphQLQuery = "{\"query\":\"query { users(options: { limit: 50} value: { }) { values { id email ip} } }\"}";

            var baseUrl = config.GetSection("Astra").GetSection("BaseUrl").Value;
            var keyspace = config.GetSection("Astra").GetSection("Keyspace").Value;
            var url = string.Format("{0}{1}", 
                baseUrl, keyspace);

            var client = new RestClient(url);
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("X-Cassandra-Token", token);
            request.AddParameter("application/json",graphQLQuery, ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            data = JsonConvert.DeserializeObject<UserRootobject>(response.Content);

            Console.WriteLine("retrieved Users: ");
           
            return data.data.users.values.ToList();
        }
    }
}
