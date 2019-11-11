Feature: TutaAPITesting

Background: 
	Given I am using the base url 'http://api.postcodes.io/postcodes/' value

Scenario: As a Service I validate admind_district value in API Response
	Given I setup the request to GET for resource 'LS3 1EP' value
	When I send the request
    Then I should receive a response
	And I should have a status code of 200
	And I validate admind_district should have 'Leeds' value 

