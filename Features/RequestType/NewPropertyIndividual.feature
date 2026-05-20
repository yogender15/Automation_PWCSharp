Feature: New_Property_Individual
	Opens the CT app as a caseworker and creates a new property individual request and validates it
Background: 
	Given I am logged in to the 'voa_VoaCouncilTax' app as a 'caseworker'
	And I manually create a valid 'new property individual' request

Scenario: Caseworker creates a New Property Individual request	
	When the request is opened
	Then the request is created successfully

Scenario: Caseworker wants to create a New Property Individual request and Validate it ensuring primary job is created
	When the request is validated
	Then the request with a primary job is created successfully

Scenario: Caseworker permissions are correct for New Property Individual request
	When the request is opened
	Then the 'request' form displays the correct fields and commands for a 'caseworker'

Scenario: Caseworker wants to create a New Property Individual request and pick the job
	And the request is validated
	And I open the job from the request form
	And I pick the job
	Then The primary job is assigned to me

Scenario: Caseworker wants to create a New Property Individual request and complete it
	And the request is validated
	And I open the job from the request form
	And I pick the job
	And I undertake the valuation exercise
	When I release and publish the request
	Then the request is in status of 'Resolved'
	Then the correspondence is sent with the template 'CT Notice - Notice of New Entry' in the status of 'Pending Schedule Generation'