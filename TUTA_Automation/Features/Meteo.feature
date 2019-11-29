Feature: Meteo


Background: 
	Given I am using meteo base url 'https://api.meteo.lt/v1/places/' value

Scenario: As a Service I validate forecast value in API Response
	Given I setup the request to GET for city resource 'kaunas/forecasts/long-term' value
	When I send the meteo request
    Then I should receive meteo response
	And I should have response status code of 200

	#1 Exercise. Add additional assertion to validate choosen value (1 bandymas nesigavo)
	
	Scenario: As a Service I validate forcast condition code value in API Response
	Given I setup the request to GET for city resource 'kaunas/forecasts/long-term' value
	When I send the meteo request
    Then I should receive meteo response
	And I should have response status code of 200
	And I validate condition Code content should have 'not empty' value

	#1 Exercise. Add additional assertion to validate choosen value (2 bandymas gavosi)
	
	Scenario: As a Service I validate place code value in API Response
	Given I setup the request to GET for city resource 'kaunas/forecasts/long-term' value
	When I send the meteo request
    Then I should receive meteo response
	And I should have response status code of 200
	And I validate place code content should have 'not empty' value

	#2 Exercise. Add negative test case
	
	Scenario: As a Service I validate place country value in API Response
	Given I setup the request to GET for city resource 'kaunas/forecasts/long-term' value
	When I send the meteo request
    Then I should receive meteo response
	And I should have response status code of 200
	And I validate place country content should have '<empty>' value

	#3 Exercise. Update test case ussing 'Outline and Examples'  

	Scenario Outline: As a Service I validate place name value in API Response
	Given I setup the request to GET for city resource 'kaunas/forecasts/long-term' value
	When I send the meteo request
    Then I should receive meteo response
	And I should have response status code of 200
	And I validate place name should have <name> value
	Examples: 
	| name     |
	| Kaunas   |
	| Klaipėda |