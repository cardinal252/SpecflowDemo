Feature: Home Page
	In order to ensure the page works

@WebUI
Scenario: User can see the home page
	Given I am an anonymous user
	When  I visit the home page
	Then I can see the title Sitecore Experience Platform
