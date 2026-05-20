Feature: New_Property_Re-Entry
	Opens the CT app as a caseworker and creates a new property re-entry request and validates it
Background: 
	Given I am logged in to the 'voa_VoaCouncilTax' app as a 'caseworker'
	And I manually create a valid 'new property re-entry' request

Scenario: Caseworker wants to create a New Property Individual request and complete it
	And the request is validated
	And I open the job from the request form
	And I pick the job
	And I undertake the valuation exercise
	When I release and publish the request
	Then the request is in status of 'Resolved'
