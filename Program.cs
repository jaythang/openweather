using System;
using System.Collections.Generic;
using System.Net.Http;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

class Program
{
    static string[] cities = { "London", "Tokyo", "New York" };

    // Variable to store the user's API key
    static string userApiKey = "c22e6900d7a13035e66cdc7fd3aaf947"; // Replace with your actual API key for the logged-in user

    // Variables to store test results
    static int apiTestsPassed = 0;
    static int apiTestsFailed = 0;
    static int uiTestsPassed = 0;
    static int uiTestsFailed = 0;

    static void Main()
    {
        // API Testing
        RunAPITests();

        // UI Testing
        RunUITests();

        // Display summary report
        DisplaySummaryReport();
    }

    static void RunAPITests()
    {
        Console.WriteLine("Running API Tests...");

        // Test login status during runtime
        if (!string.IsNullOrEmpty(userApiKey))
        {
            Console.WriteLine("User is already logged in.");
        }
        else
        {
            Console.WriteLine("User is not logged in. Logging in...");
            // Perform the login logic
            LogIn();
        }

        foreach (string city in cities)
        {
            string apiUrl = GenerateApiUrl(city);
            HttpResponseMessage response = CallWeatherAPI(apiUrl);

            Console.WriteLine($"City: {city}, Status Code: {response.StatusCode}");

            // Print the API response content
            Console.WriteLine($"API Response for {city}: {response.Content.ReadAsStringAsync().Result}");

            // Update test results
            UpdateApiTestResults(response.StatusCode, city);
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

            // Update test results
            UpdateApiTestResults(response.StatusCode, invalidCity);
        }

        Console.WriteLine("API Tests completed.\n");
    }

    static void RunUITests()
    {
        Console.WriteLine("Running UI Tests...");

        // Test login status during runtime
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


            // Check if the user is already logged in
            if (!string.IsNullOrEmpty(userApiKey))
           {
                Console.WriteLine("User is not logged in. Logging in...");
                // Perform the login logic
                LogInUsingAPIKey(driver, userApiKey);
            }

            else
             {
                Console.WriteLine("User is already logged in.");
            }
            
            // Perform weather-related actions for the user
            foreach (string city in cities)
            {
                SearchForWeather(driver, city);
            }

            //Logout User
            driver.Navigate().GoToUrl("https://home.openweathermap.org/");
            driver.FindElement(By.XPath("//div[@id='user-dropdown']/div")).Click();
            driver.FindElement(By.LinkText("Logout")).Click();

            Console.WriteLine("UI Tests completed.");
        }
    }

    static string GenerateApiUrl(string city)
    {
        string baseUrl = "https://api.openweathermap.org/data/2.5/weather";

        // Check if the user is logged in (API key is set)
        if (!string.IsNullOrEmpty(userApiKey))
        {
            return $"{baseUrl}?q={city}&APPID={userApiKey}";
        }
        else
        {
            return $"{baseUrl}?q={city}";
        }
    }

    static HttpResponseMessage CallWeatherAPI(string apiUrl)
    {
        using (HttpClient client = new HttpClient())
        {
            return client.GetAsync(apiUrl).Result;
        }
    }

    static void LogIn()
    {
        // Your login logic here...
        Console.WriteLine("Performing login...");
        // For demonstration purposes, let's wait for a few seconds
        System.Threading.Thread.Sleep(2000);
        // Set the userApiKey to a valid API key after successful login
        userApiKey = "VALID_API_KEY"; // Replace with the actual valid API key
    }

    static void LogInUsingAPIKey(IWebDriver driver, string apiKey)
    {
        // Your login logic here...
        Console.WriteLine($"Logging in using API key: {apiKey}");

        // For demonstration purposes, let's wait for a few seconds
        System.Threading.Thread.Sleep(2000);
    }

    static void SearchForWeather(IWebDriver driver, string city)
    {
        Console.WriteLine($"Searching for weather in {city}");

        // Call the OpenWeatherMap API and print the details of the response

        
        string apiUrl = GenerateApiUrl(city);
        HttpResponseMessage response = CallWeatherAPI(apiUrl);
        Console.WriteLine($"API Response for {city}: {response.Content.ReadAsStringAsync().Result}");

        // For demonstration purposes, let's wait for a few seconds
        System.Threading.Thread.Sleep(2000);

        // Update test results
        UpdateUITestResults(response.StatusCode, city);
    }

    static void RunAPITests1()
{
    Console.WriteLine("Running API Tests...");

    // Test login status during runtime
    if (!string.IsNullOrEmpty(userApiKey))
    {
        Console.WriteLine("User is already logged in.");
    }
    else
    {
        Console.WriteLine("User is not logged in. Logging in...");
        // Perform the login logic
        LogIn();
    }

    foreach (string city in cities)
    {
        string apiUrl = GenerateApiUrl(city);
        HttpResponseMessage response = CallWeatherAPI(apiUrl);

        Console.WriteLine($"City: {city}, Status Code: {response.StatusCode}");

        // Print the API response content
        Console.WriteLine($"API Response for {city}: {response.Content.ReadAsStringAsync().Result}");

        // Update test results based on status code
        UpdateApiTestResults(response.StatusCode, city);
    }

    // ... (existing code)
}

static void UpdateApiTestResults(System.Net.HttpStatusCode statusCode, string cityName)
{
    if (!string.IsNullOrEmpty(userApiKey))
    {
        // User is logged in, expect 200 status code
        if (statusCode == System.Net.HttpStatusCode.OK)
        {
            apiTestsPassed++;
        }
        else
        {
            apiTestsFailed++;
            Console.WriteLine($"API Test Failed for {cityName}");
        }
    }
    else
    {
        // User is not logged in, expect 401 status code
        if (statusCode == System.Net.HttpStatusCode.Unauthorized)
        {
            apiTestsPassed++;
        }
        else
        {
            apiTestsFailed++;
            Console.WriteLine($"API Test Failed for {cityName}");
        }
    }
}

    static void UpdateUITestResults(System.Net.HttpStatusCode statusCode, string cityName)
    {
        if (statusCode == System.Net.HttpStatusCode.OK)
        {
            uiTestsPassed++;
        }
        else
        {
            uiTestsFailed++;
            Console.WriteLine($"UI Test Failed for {cityName}");
        }
    }

    static void DisplaySummaryReport()
    {
        Console.WriteLine("\nSummary Report:");
        Console.WriteLine($"API Tests Passed: {apiTestsPassed}");
        Console.WriteLine($"API Tests Failed: {apiTestsFailed}");
        Console.WriteLine($"UI Tests Passed: {uiTestsPassed}");
        Console.WriteLine($"UI Tests Failed: {uiTestsFailed}");
    }
}
