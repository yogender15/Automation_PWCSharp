Feature: Effective Date Change
	Opens the CT app as a caseworker and creates a effective date change request and validates it
Background: 
	Given I am logged in to the 'voa_VoaCouncilTax' app as a 'caseworker'
	And I manually create a valid 'effective date change' request

Scenario: Caseworker creates a effective date change request	
	When the request is opened
	Then the request is created successfully

Scenario: Caseworker wants to create a effective date change and Validate it ensuring primary job is created
	When the request is validated
	Then the request with a primary job is created successfully

Scenario: Caseworker wants to create a effective date change request and pick the job
	And the request is validated
	And I open the job from the request form
	And I pick the job
	Then The primary job is assigned to me

Scenario: Caseworker wants to create a effective date change request and complete it
	And the request is validated
	And I open the job from the request form
	And I pick the job
	And I undertake the valuation exercise
	When I release and publish the request
	Then the request is in status of 'Resolved'
	