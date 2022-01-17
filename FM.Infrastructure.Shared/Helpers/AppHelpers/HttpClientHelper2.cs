using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FM.Infrastructure.Shared.Helpers.AppHelpers
{
    public class HttpClientHelper2
    {
        public static async Task<string> SendRequestAsync(string url, string cookieStr)
        {
            var htmltask = await GetWebContent(url, cookieStr);

            return htmltask;
        }

        private static async Task<string> GetWebContent(string url, string cookieStr)
        {
            CookieContainer cookieContainer = new CookieContainer();  // Sử dụng CookieContainer riêng, để lưu lại Cookie - hoặc thêm cookie
            var arrCookie = cookieStr.Split(';');
            foreach (var cookie in arrCookie)
            {
                var arr = cookie.Split('=');
                if (arr.Length > 1)
                {
                    cookieContainer.Add(new Uri(url), new Cookie(arr[0].Trim(), arr[1].Trim()));
                }
            }

            using (var myHttpClientHandler = new MyHttpClientHandler(cookieContainer))
            using (var httpClient = new HttpClient(myHttpClientHandler))
            {
                Console.WriteLine($"Starting connect {url}");
                httpClient.DefaultRequestHeaders.Add("Accept", "text/html,application/xhtml+xml+json");
                httpClient.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/80.0.3987.163 Safari/537.36");

                HttpResponseMessage response = await httpClient.GetAsync(url);
                response.EnsureSuccessStatusCode();
                byte[] bytes = await response.Content.ReadAsByteArrayAsync();

                //Encoding encoding = Encoding.GetEncoding("windows-1251");
                Encoding encoding = Encoding.GetEncoding("utf-8");
                string html = encoding.GetString(bytes, 0, bytes.Length);

                return html;
            }
        }

        private static void ShowHeaders(string lable, HttpHeaders headers)
        {
            Console.WriteLine(lable);
            foreach (var header in headers)
            {
                string value = string.Join(" ", header.Value);
                Console.WriteLine($"{header.Key,20} : {value}");
            }
            Console.WriteLine();
        }

        private class MyHttpClientHandler : HttpClientHandler
        {
            public MyHttpClientHandler(CookieContainer cookie_container)
            {
                CookieContainer = cookie_container;     // Thay thế CookieContainer mặc định
                AllowAutoRedirect = true;                // không cho tự động Redirect
                AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip;
                UseCookies = true;
            }

            protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
            {
                ShowHeaders("Request header trước khi qua Handler ", request.Headers);

                var task = base.SendAsync(request, cancellationToken); // bắt buộc gọi
                await task;

                ShowHeaders("Request header sau khi qua Handler ", request.Headers);

                return task.Result;
            }
        }
    }
}