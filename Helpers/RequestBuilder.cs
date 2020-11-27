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
            IEnumerable<KeyValuePair<string, object>> cleanParameters = CleanParameters(parameters);

            return method.Method switch
            {
                "GET" => BuildGetRequest(url, cleanParameters),
                "POST" => BuildPostRequest(url, cleanParameters),
                "PUT" => BuildPutRequest(url, cleanParameters),
                "DELETE" => BuildDeleteRequest(url, cleanParameters),
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
                else
                {
                    content.Add(new StringContent(value?.ToString()), key);
                }
            }

            return content;
        }
        
        private static IEnumerable<KeyValuePair<string, object>> CleanParameters(
            IEnumerable<KeyValuePair<string, object>> parameters)
        {
            Dictionary<string, object> result = new();

            if (parameters == null)
            {
                return result;
            }

            foreach(KeyValuePair<string, object> kv in parameters)
            {
                string key = kv.Key;
                object value = kv.Value;

                if(value is bool b)
                {
                    result.Add(key, b.ToString().ToLowerInvariant());
                }
                else if(value is DateTime d)
                {
                    result.Add(key, d.ToString("O"));
                }
                else
                {
                    result.Add(key, value);
                }
            }

            return result;
        }
    }
}