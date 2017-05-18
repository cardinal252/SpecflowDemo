Feature: Renderings Present
	In order to ensure the given rendering is present

@WebUI
Scenario: User can see the rendering
	Given I am an anonymous user
	When  I visit the home page
	Then I see the sample inner rendering
