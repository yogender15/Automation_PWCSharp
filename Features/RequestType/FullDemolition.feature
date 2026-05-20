Feature: Full_Demolition
	Opens the CT app as a caseworker and creates full demolition request and validates it
Background: 
	Given I am logged in to the 'voa_VoaCouncilTax' app as a 'caseworker'
	And I manually create a valid 'full demolition' request

Scenario: Caseworker wants to create a Full Demolition request and validate it ensuring auto resolution of the request
	And the request is validated
	And I Save and Close the request validation Dialog
	Then the request is in status of 'Resolved'
	#Then the correspondence is sent with the template 'CT Notice - Notice of deletion' in the status of 'Pending Schedule Generation'
