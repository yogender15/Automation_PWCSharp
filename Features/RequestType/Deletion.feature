Feature: Deletion
	Opens the CT app as a caseworker and creates deletion request and validates it
Background: 
	Given I am logged in to the 'voa_VoaCouncilTax' app as a 'caseworker'
	And I manually create a valid 'deletion' request

Scenario: Caseworker wants to create a Deletion request and complete it
	And the request is validated
	And I Save and Close the request validation Dialog
	And I open the job from the request form
	And I pick the job
	And I undertake the valuation exercise
	When I release and publish the request
	Then the request is in status of 'Resolved'
	#Then the correspondence is sent with the template 'CT Notice - Notice of deletion' in the status of 'Pending Schedule Generation'