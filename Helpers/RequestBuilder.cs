using Challonge.Objects;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;

namespace Challonge.Helpers
{
    internal static class RequestBuilder
    {
        internal static HttpRequestMessage BuildRequest(string url, HttpMethod method,
            IEnumerable<KeyValuePair<string, object>> parameters)
        {
            parameters ??= new Dictionary<string, object>();

            return method.Method switch
            {
                "GET" => BuildGetRequest(url, parameters),
                "POST" => BuildPostRequest(url, parameters),
                "PUT" => BuildPutRequest(url, parameters),
                "DELETE" => BuildDeleteRequest(url, parameters),
                _ => throw new NotImplementedException("This HTTP method is not supported.")
            };
        }

        private static HttpRequestMessage BuildGetRequest(string url,
            IEnumerable<KeyValuePair<string, object>> parameters)
        {
            return new(HttpMethod.Get, url + BuildQueryString(parameters));
        }

        private static HttpRequestMessage BuildPostRequest(string url,
            IEnumerable<KeyValuePair<string, object>> parameters)
        {
            return new(HttpMethod.Post, url)
            {
                Content = BuildHttpContent(parameters)
            };
        }

        private static HttpRequestMessage BuildPutRequest(string url,
            IEnumerable<KeyValuePair<string, object>> parameters)
        {
            return new(HttpMethod.Put, url)
            {
                Content = BuildHttpContent(parameters)
            };
        }

        private static HttpRequestMessage BuildDeleteRequest(string url,
            IEnumerable<KeyValuePair<string, object>> parameters)
        {
            return new(HttpMethod.Delete, url + BuildQueryString(parameters));
        }

        private static string BuildQueryString(IEnumerable<KeyValuePair<string, object>> parameters)
        {
            string qString = "";

            if (parameters.Any())
            {
                qString = "?" + string.Join("&", parameters.Where(kv => kv.Value != null)
                    .Select(kv => $"{Uri.EscapeDataString(kv.Key)}=" +
                        $"{Uri.EscapeDataString(kv.Value.ToString())}"));
            }

            return qString;
        }

        private static HttpContent BuildHttpContent(IEnumerable<KeyValuePair<string, object>> parameters)
        {
            MultipartFormDataContent content = new();

            foreach (KeyValuePair<string, object> kv in parameters)
            {
                string key = kv.Key;
                object value = kv.Value;

                if (value is MatchAttachmentAsset a)
                {
                    content.Add(new StreamContent(
                        new MemoryStream(a.Content)), key, a.FileName);
                }
                else if(value is DateTime d)
                {
                    content.Add(new StringContent(d.ToString("O")), key);
                }
                else
                {
                    content.Add(new StringContent(value?.ToString()), key);
                }
            }
            return content;
        }
    }
}