Feature: SpecFlowFeature1
	As a user I want to give myself a compliment
	So that I feel better about me

@WebUI
Scenario: Give yourself a big up
	Given I visit the home page
	When I click the button
	Then I get a warming message
