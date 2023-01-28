using System;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

namespace NoticeMe.Services.Particle
{
    public class RestApiTests
    {
        private static HttpClient _httpClient;
        private const string _token = "b77f40c266c21a67ba7bad5ca37422730ae83dc5";

        public void CreateHttpClient()
        {
#if __WASM__
            var innerHandler = new Uno.UI.Wasm.WasmHttpHandler();
#else
            var innerHandler = new HttpClientHandler();
#endif
            // please ensure that a single instance of _httpClient is used
            // between all requests.
            // https://aspnetmonsters.com/2016/08/2016-08-27-httpclientwrong/
            _httpClient = new HttpClient(innerHandler);
        }

        public async Task ProcessRepositories()
        {
            if (_httpClient == null)
                CreateHttpClient();

            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/vnd.github.v3+json"));
            _httpClient.DefaultRequestHeaders.Add("User-Agent", ".NET Foundation Repository Reporter");

            var stringTask = _httpClient.GetStringAsync("https://api.github.com/orgs/dotnet/repos");

            var msg = await stringTask;
            Debug.WriteLine(msg);
        }

        public void RequestStatus()
        {
            Debug.WriteLine("RequestStatus called, starting request");
            //// Start the child process.
            //Process p = new Process();
            //// Redirect the output stream of the child process.
            //p.StartInfo.UseShellExecute = false;
            //p.StartInfo.RedirectStandardOutput = true;
            //p.StartInfo.FileName = "YOURBATCHFILE.bat";
            //p.Start();
            //// Do not wait for the child process to exit before
            //// reading to the end of its redirected stream.
            //// p.WaitForExit();
            //// Read the output stream first and then wait.
            //string output = p.StandardOutput.ReadToEnd();
            //p.WaitForExit();

            Thread t = new Thread(delegate () 
            {
                    Process p = new Process();
                    System.Diagnostics.Process process = new System.Diagnostics.Process();
                    process.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                    process.StartInfo.FileName = "cmd.exe";
                    process.StartInfo.Arguments = @"/C curl https://api.particle.io/v1/devices/events?access_token=491b4b1c5d626497780cd786fdd49121ab6c4ff0";
                    process.StartInfo.UseShellExecute = false;
                    process.StartInfo.CreateNoWindow = true;
                    process.StartInfo.RedirectStandardOutput = true;
                    process.StartInfo.RedirectStandardInput = true;
                    process.Start();
                    string q = "";
                    while (!process.HasExited)
                    {
                        q += process.StandardOutput.ReadToEnd();
                    }
                    Debug.WriteLine(q);
                    //    MessageBox.Show(q);  
            }) 
            { IsBackground = true };

            t.Start();
        }

      
        private async Task<string> StartCMD(Process p)
        {
            // Start the child process. if p == null

            // Redirect the output stream of the child process.
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.FileName = ".bat";
            p.Start();
            // Do not wait for the child process to exit before
            // reading to the end of its redirected stream.
            // p.WaitForExit();
            // Read the output stream first and then wait.
            string output = await p.StandardOutput.ReadToEndAsync();
            p.WaitForExitAsync();

            return output;
        }


        public async Task<string> GetAsync(string uri)
        {
            if(_httpClient == null)
                CreateHttpClient();

            //// Create the HttpContent for the form to be posted.
            //var requestContent = new FormUrlEncodedContent(new[] {
            //    new KeyValuePair<string, string>("access_token", "491b4b1c5d626497780cd786fdd49121ab6c4ff0"),
            //    new KeyValuePair<string, string>("eventPrefix", "c"),
            //});
            // Get the response.
            HttpResponseMessage response = await _httpClient.GetAsync(
                "https://api.particle.io/v1/devices/events?access_token=491b4b1c5d626497780cd786fdd49121ab6c4ff0");

            // Get the response content.
            HttpContent responseContent = response.Content;

            // Get the stream of the content.
            using (var reader = new StreamReader(await responseContent.ReadAsStreamAsync()))
            {
                // Write the output.
                string output = await reader.ReadToEndAsync();
                Debug.WriteLine(output);
            }

            return "sadge";
        }
    }
}