Feature: Auto_Allocation_
	Auto Allocates request and job ownership (Auto Allocation Routing) on validation completion
Background: 
	Given I am logged in to the 'voa_VoaCouncilTax' app as a 'caseworker'
	And I manually create a valid 'new property individual' request
	When the request is validated

Scenario: Caseworker validates a request and it is auto allocated to the CT National Team for Billing Authority	
	##Then the job is assigned to the 'CT NATIONAL TEAM 22' team
	Then the job is assigned to national team of the billing authority

Scenario: Caseworker validates a request and it is not left with the creator	
	Then the job is not assigned to the creator of the request