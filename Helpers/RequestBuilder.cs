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
            IEnumerable<KeyValuePair<string, object>> cleaned = CleanParameters(parameters);

            return method.Method switch
            {
                "GET" => BuildGetRequest(url, cleaned),
                "POST" => BuildPostRequest(url, cleaned),
                "PUT" => BuildPutRequest(url, cleaned),
                "DELETE" => BuildDeleteRequest(url, cleaned),
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
                qString = "?" + string.Join("&", parameters
                    .Where(kv => kv.Value != null)
                    .Select(kv => $"{Uri.EscapeDataString(kv.Key)}=" +
                        $"{Uri.EscapeDataString(kv.Value.ToString())}"));
            }

            return qString;
        }

        private static HttpContent BuildHttpContent(IEnumerable<KeyValuePair<string, object>> parameters)
        {
            HttpContent content = null;

            if (parameters.Any(kv => kv.Value?.GetType() == typeof(MatchAttachmentAsset)))
            {
                MultipartFormDataContent fileContent = new();

                foreach (KeyValuePair<string, object> kv in parameters)
                {
                    string key = kv.Key;
                    object value = kv.Value;

                    if (value is MatchAttachmentAsset a)
                    {
                        fileContent.Add(new StreamContent(
                            new MemoryStream(a.Content)), key, a.FileName);
                    }
                    else
                    {
                        fileContent.Add(new StringContent(value?.ToString()), key);
                    }
                }

                content = fileContent;
            }
            else
            {
                content = new FormUrlEncodedContent(parameters.Select(kv =>
                    new KeyValuePair<string, string>(kv.Key, kv.Value?.ToString())));
            }

            return content;
        }

        private static IEnumerable<KeyValuePair<string, object>> CleanParameters(
        IEnumerable<KeyValuePair<string, object>> parameters)
        {
            Dictionary<string, object> result = new();

            parameters ??= new Dictionary<string, object>();

            foreach(KeyValuePair<string, object> kv in parameters)
            {
                string key = kv.Key;
                object value = kv.Value;;

                result.Add(key, value switch
                {
                    bool b => b.ToString().ToLowerInvariant(),
                    DateTime d => d.ToString("O"),
                    _ => value
                });
            }

            return result;
        }
    }
}