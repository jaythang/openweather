SDET Test August 2023

Test Scenario

You are testing a weather forecast application that provides weather information for different cities.
Your task is to create automated tests using Selenium for UI testing and nested loops for API testing to
verify the accuracy of the weather forecast data.

Prerequisites

Use Selenium for signing up on the [OpenWeatherMap](https://openweathermap.org/) website to
retrieve the API key, Do not Hard Code it. The solution needs to be runnable by providing different user
names and passwords.

- Use environment variables to setup username and password
- API key should be retrieved from the system every test
- Tests should be able to be run in any order

Instructions

1. Use C# as your programming language.
2. Utilize Selenium WebDriver for UI testing and HttpClient for API testing.
3. Write separate test cases for UI testing and API testing.
4. For UI testing, focus on the following scenarios:
- Search for weather in different cities.
- Verify that the displayed temperature matches the API data.
- Handle cases where no data is found or invalid data is displayed.
5. For API testing, use nested loops to cover various scenarios:
- Test weather data retrieval for different cities.
- Test cases with invalid city names.

2
- Check the temperature format and units.
6. Ensure your test cases are well-structured, thoroughly documented, and follow best practices.
7. Assume that the weather application's UI can be automated using Selenium, and the weather forecast
API follows the structure:
- Base URL: `https://api.openweathermap.org`
- Endpoint: `/data/2.5/weather`
- Query parameters: `q`, `appid`, `units`
- Example: `POST
https://openweathermap.org/data/2.5/weather?q=NewYork&appid=YOUR_API_KEY&units=metric`

8. Write the solution in a way that will allow running each and every test by itself, regardless of the
order of it.
For this, check the login status during runtime and determine should you re-login or not.

Evaluation Criteria

You will be evaluated based on your ability to design effective UI tests using Selenium and create
comprehensive API tests using nested loops.

Submission

Provide the source code for your test cases. Please submit the code through a code-sharing platform
