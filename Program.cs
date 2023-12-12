using System;
using System.Net.Http;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

class Program
{
    static string[] cities = { "London", "Tokyo", "New York" };

    static void Main()
    {
        // API Testing
        RunAPITests();

        // UI Testing
        RunUITests();
    }

    static void RunAPITests()
    {
        Console.WriteLine("Running API Tests...");

        foreach (string city in cities)
        {
            string apiUrl = GenerateApiUrl(city);
            HttpResponseMessage response = CallWeatherAPI(apiUrl);

            Console.WriteLine($"City: {city}, Status Code: {response.StatusCode}");

            // Print the API response content
            Console.WriteLine($"API Response for {city}: {response.Content.ReadAsStringAsync().Result}");
        }

        // Test cases with invalid city names
        string[] invalidCities = { "tongtue", "zubaragu", "ambabadumba" };
        foreach (string invalidCity in invalidCities)
        {
            string apiUrl = GenerateApiUrl(invalidCity);
            HttpResponseMessage response = CallWeatherAPI(apiUrl);

            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                Console.WriteLine($"Invalid City: {invalidCity}, Status Code: {response.StatusCode}. City not found.");
            }
            else
            {
                Console.WriteLine($"Invalid City: {invalidCity}, Status Code: {response.StatusCode}. Check the response for more details.");
                // Print the API response content
                Console.WriteLine($"API Response for {invalidCity}: {response.Content.ReadAsStringAsync().Result}");
            }
        }

        Console.WriteLine("API Tests completed.\n");
    }

    static void RunUITests()
    {
        Console.WriteLine("Running UI Tests...");

        using (var driver = new ChromeDriver())
        {
            // Navigate to OpenWeatherMap website
            driver.Navigate().GoToUrl("https://openweathermap.org/");
            driver.Navigate().GoToUrl("https://openweathermap.org/");
            driver.Navigate().GoToUrl("https://home.openweathermap.org/users/sign_in");
            driver.FindElement(By.Id("user_email")).Click();
            driver.FindElement(By.Id("user_email")).Clear();
            driver.FindElement(By.Id("user_email")).SendKeys("thang.jay@gmail.com");
            driver.FindElement(By.Id("user_password")).Clear();
            driver.FindElement(By.Id("user_password")).SendKeys("P4ssw0rd");
            driver.FindElement(By.Name("commit")).Click();
            driver.Navigate().GoToUrl("https://home.openweathermap.org/");

            // Log in using the API key
            LogInUsingAPIKey(driver, "c22e6900d7a13035e66cdc7fd3aaf947"); // Replace with your actual API key

            // Perform weather-related actions for the logged-in user
            foreach (string city in cities)
            {
                SearchForWeather(driver, city);
            }

            Console.WriteLine("UI Tests completed.");
        }
    }

    static string GenerateApiUrl(string city)
    {
        string baseUrl = "https://api.openweathermap.org/data/2.5/weather";
        string apiKey = "c22e6900d7a13035e66cdc7fd3aaf947"; // Replace with your actual API key

        return $"{baseUrl}?q={city}&APPID={apiKey}";
    }

    static HttpResponseMessage CallWeatherAPI(string apiUrl)
    {
        using (HttpClient client = new HttpClient())
        {
            return client.GetAsync(apiUrl).Result;
        }
    }

    static void LogInUsingAPIKey(IWebDriver driver, string apiKey)
    {
        Console.WriteLine($"Logging in using API key: {apiKey}");

        System.Threading.Thread.Sleep(2000);
    }

    static void SearchForWeather(IWebDriver driver, string city)
    {
        Console.WriteLine($"Searching for weather in {city}");

        // Call the OpenWeatherMap API and print the details of the response
        string apiUrl = GenerateApiUrl(city);
        HttpResponseMessage response = CallWeatherAPI(apiUrl);
        Console.WriteLine($"API Response for {city}: {response.Content.ReadAsStringAsync().Result}");

        System.Threading.Thread.Sleep(2000);
    }
}
