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

        public static List<User> GetUsers(IConfiguration config, string token)
        {
            UserRootObject data = new UserRootObject();
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
            data = JsonConvert.DeserializeObject<UserRootObject>(response.Content);

            Console.WriteLine("retrieved Users: ");
           
            return data.data.users.values.ToList();
        }
        public static List<Tag> GetTags(IConfiguration config, string token)
        {
            TagRootObject data = new TagRootObject();
            string graphQLQuery = "{\"query\":\"query { tags(options: { limit: 50} value: { }) { values { id image} } }\"}";

            var baseUrl = config.GetSection("Astra").GetSection("BaseUrl").Value;
            var keyspace = config.GetSection("Astra").GetSection("Keyspace").Value;
            var url = string.Format("{0}{1}",
                baseUrl, keyspace);

            var client = new RestClient(url);
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("X-Cassandra-Token", token);
            request.AddParameter("application/json", graphQLQuery, ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            data = JsonConvert.DeserializeObject<TagRootObject>(response.Content);

            Console.WriteLine("retrieved Users: ");

            return data.data.tags.values.ToList() ;
        }
        public static List<ApacheLog> GetApacheLogs(IConfiguration config, string token)
        {
            ApacheLogRootobject data = new ApacheLogRootobject();
            string graphQLQuery = "{\"query\":\"query { apacheLogs(options: { limit: 50} value: { }) { values { logId ip accessUrl} } }\"}";

            var baseUrl = config.GetSection("Astra").GetSection("BaseUrl").Value;
            var keyspace = config.GetSection("Astra").GetSection("Keyspace").Value;
            var url = string.Format("{0}{1}",
                baseUrl, keyspace);

            var client = new RestClient(url);
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("X-Cassandra-Token", token);
            request.AddParameter("application/json", graphQLQuery, ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            data = JsonConvert.DeserializeObject<ApacheLogRootobject>(response.Content);

            Console.WriteLine("retrieved Users: ");

            return data.data.apacheLogs.values.ToList();
        }
        public static List<Upload> GetUploads(IConfiguration config, string token)
        {
            UploadRootobject data = new UploadRootobject();
            string graphQLQuery = "{\"query\":\"query { uploads(options: { limit: 50} value: { }) { values { id filename ip userid} } }\"}";

            var baseUrl = config.GetSection("Astra").GetSection("BaseUrl").Value;
            var keyspace = config.GetSection("Astra").GetSection("Keyspace").Value;
            var url = string.Format("{0}{1}",
                baseUrl, keyspace);

            var client = new RestClient(url);
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("X-Cassandra-Token", token);
            request.AddParameter("application/json", graphQLQuery, ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            data = JsonConvert.DeserializeObject<UploadRootobject>(response.Content);

            Console.WriteLine("retrieved Users: ");

            return data.data.uploads.values.ToList();
        }
        public static List<Tag> GetTagsByUploadId(IConfiguration config, string token, string uploadId)
        {
            TagsByUploadIdRootObject data = new TagsByUploadIdRootObject();
            string graphQLQuery = "{\"query\":\"query {    uploads(value: { id: " + uploadId + " }) {        values {            id             filename             ip             userid        }    }    tags(value: { uploadid: " + uploadId + "}) {        values {            title            album            artist            image        }    }}\" }";

            var baseUrl = config.GetSection("Astra").GetSection("BaseUrl").Value;
            var keyspace = config.GetSection("Astra").GetSection("Keyspace").Value;
            var url = string.Format("{0}{1}",
                baseUrl, keyspace);

            var client = new RestClient(url);
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("X-Cassandra-Token", token);
            request.AddParameter("application/json", graphQLQuery, ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            data = JsonConvert.DeserializeObject<TagsByUploadIdRootObject>(response.Content);

            Console.WriteLine("retrieved Tags: ");

            // send empty Upload list if value is null
            if (data.data.tags == null)
            {
                data.data.tags = new Tags();
                data.data.tags.values = new Tag[0];
            }

            return data.data.tags.values.ToList();
        }
        public static List<Upload> GetUploadsByUserId(IConfiguration config, string token, string userId)
        {
            UploadsByUserIdRootObject data = new UploadsByUserIdRootObject();
            string graphQLQuery = "{\"query\":\"query {    users(value: { id: " + userId + "}) {        values {            ip            email            lastActive        }    }    uploads(value: { userid: " + userId + " }) {        values {            id             filename             ip             userid        }    }}\"}";

            var baseUrl = config.GetSection("Astra").GetSection("BaseUrl").Value;
            var keyspace = config.GetSection("Astra").GetSection("Keyspace").Value;
            var url = string.Format("{0}{1}",
                baseUrl, keyspace);

            var client = new RestClient(url);
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("X-Cassandra-Token", token);
            request.AddParameter("application/json", graphQLQuery, ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            data = JsonConvert.DeserializeObject<UploadsByUserIdRootObject>(response.Content);

            Console.WriteLine("retrieved Uploads: ");

            // send empty Upload list if value is null
            if (data.data.uploads == null)
            {
                data.data.uploads = new Uploads();
                data.data.uploads.values = new Upload[0];
            }

            return data.data.uploads.values.ToList();
        }

    }
}
