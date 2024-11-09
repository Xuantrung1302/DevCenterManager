using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BusinessLogic
{
    public class CallAPI
    {
        private static readonly HttpClient client = new HttpClient();
        //client.DefaultRequestHeaders.ConnectionClose = true;

        public async Task<DataTable> GetAPI(string url)
        {
            DataTable result = new DataTable();
            try
            {
                string response = await client.GetStringAsync(url);
                if (response != null)
                {
                    result = JsonConvert.DeserializeObject<DataTable>(response);
                }
            }
            catch
            {
                MessageBox.Show("Có lỗi get xảy ra");
            }

            return result;
        }
        public async Task<bool> PostAPI(string url, string json = null)
        {
            bool result = false;
            try
            {
                var content = json is null ? null : new StringContent(json, Encoding.UTF8, "application/json");

                var response = await client.PostAsync(url, content);

                if (response.IsSuccessStatusCode)
                {
                    result = bool.Parse(await response.Content.ReadAsStringAsync());
                }
                else
                {
                    // Xử lý khi phản hồi không thành công
                    string errorMessage = $"Lỗi khi gọi API: {response.ReasonPhrase}";
                    MessageBox.Show(errorMessage, "Thông báo");
                }
            }
            catch
            {
                MessageBox.Show("Có lỗi post xảy ra");
            }

            return result;
        }
        public async Task<int> PostIntAPI(string url, string json = null)
        {

            int result = 0;
            try
            {
                var content = json is null ? null : new StringContent(json, Encoding.UTF8, "application/json");

                var response = await client.PostAsync(url, content);

                if (response.IsSuccessStatusCode)
                {
                    result = int.Parse(await response.Content.ReadAsStringAsync());
                }
                else
                {
                    // Xử lý khi phản hồi không thành công
                    string errorMessage = $"Lỗi khi gọi API: {response.ReasonPhrase}";
                    MessageBox.Show(errorMessage, "Thông báo");
                }
            }
            catch
            {
                MessageBox.Show("Có lỗi post xảy ra");
            }

            return result;
        }
        public async Task<string> CallApiAsync(string apiUrl)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    // Gửi yêu cầu HTTP GET đến API
                    HttpResponseMessage response = await client.GetAsync(apiUrl);

                    // Kiểm tra nếu yêu cầu thành công (status code 200)
                    response.EnsureSuccessStatusCode();

                    // Đọc nội dung trả về từ API và chuyển đổi thành chuỗi
                    string result = await response.Content.ReadAsStringAsync();
                    return result;
                }
                catch (HttpRequestException e)
                {
                    // Xử lý lỗi khi có vấn đề với yêu cầu HTTP
                    Console.WriteLine($"Request error: {e.Message}");
                    return null; // Hoặc xử lý lỗi theo cách phù hợp
                }
            }
        }
    }
}
