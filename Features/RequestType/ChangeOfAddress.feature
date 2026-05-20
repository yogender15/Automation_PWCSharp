Feature: Change_Of_Address
	Opens the CT app as a caseworker and creates a change of address request and validates it
Background: 
	Given I am logged in to the 'voa_VoaCouncilTax' app as a 'caseworker'
	And I manually create a valid 'change of address' request

Scenario: Caseworker wants to create a change of address request and complete it
	And the request is validated
	And I Save and Close the request validation Dialog
	Then the request is in status of 'Resolved'
