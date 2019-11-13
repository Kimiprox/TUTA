Feature: Meteo


Background: 
	Given I am using meteo base url 'https://api.meteo.lt/v1/places/' value

Scenario: As a Service I validate forecast value in API Response
	Given I setup the request to GET for city resource 'kaunas/forecasts/long-term' value
	When I send the meteo request
    Then I should receive meteo response
	And I should have response status code of 200
	And I validate 'airTemperature' content should have 'not null' value 
