using System.Diagnostics; //измерение затраченного времени на выполнение операции
using System.Net;

//Stopwatch sw = new Stopwatch();
Stopwatch sw = Stopwatch.StartNew(); //сразу запускает

Sync_Method ip = new Sync_Method("https://api.myip.com/");
Sync_Method cookie = new Sync_Method("http://cookie.jsontest.com/");
Sync_Method date = new Sync_Method("http://date.jsontest.com/");
ip.Request();
cookie.Request();
date.Request();
sw.Stop();
Console.WriteLine($"Прошло {sw}\n");

sw.Restart();
var t1 = new Async_Method("https://api.myip.com/");
var t2 = new Async_Method("http://cookie.jsontest.com/");
var t3 = new Async_Method("http://date.jsontest.com/");
var task = new List<Task> { 
    t1.Request(),
    t2.Request(),
    t3.Request()
};
await Task.WhenAll(task);
sw.Stop();
Console.WriteLine($"Прошло {sw}");


public class Sync_Method
{
    string url;
    public Sync_Method(string URL) { url = URL; }
    public void Request()
    {
        using (var Httpclient = new HttpClient())
        {
            try
            {
                var result = Httpclient.GetAsync(url).Result;
                var json = result.Content.ReadAsStringAsync().Result;
                Console.WriteLine(json);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Environment.Exit(0);
            }
        }
    }
}

public class Async_Method
{
    string url;
    public Async_Method(string URL) { url = URL; }

    public async Task Request()
    {
        using (var Httpclient = new HttpClient())
        {
            try
            {
                var result = Httpclient.GetAsync(url).Result;
                var json = result.Content.ReadAsStringAsync().Result;
                Console.WriteLine(json);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Environment.Exit(0);
            }
        }
    }

}

