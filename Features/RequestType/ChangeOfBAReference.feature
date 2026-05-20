Feature: Change_Of_Billing_Authority_Reference
	Opens the CT app as a caseworker and creates a change of ba reference request and validates it
Background: 
	Given I am logged in to the 'voa_VoaCouncilTax' app as a 'caseworker'
	And I manually create a valid 'change of ba reference' request

Scenario: Validation error when no BA reference is supplied by the caseworker
	And the request is validated
	And I Save and Close the request validation Dialog
	And I open the job from the request form
	And I pick the job
	And I undertake the valuation exercise
	Then The following errors should be presented from the pre-release validation
		| ErrorMessage                                                                             |
		| PBARA-00001 : Proposed BA Reference Amendments cannot be less than 2 related to Incident |
		| BA-00002 : Error ExecuteReleaseBAReference: 'Proposed BA Reference Ammendments cannot be less than 2 related to Incident |

Scenario: Caseworker wants to create a change of BA reference request with autoprocessing and complete it
	And the request is validated
	And I Save and Close the request validation Dialog
	And I initiate a new BA Reference form for autoprocessing
	Then the request is in status of 'Resolved'

Scenario: Caseworker wants to create a change of BA reference request without autoprocessing and complete it
	And the request is validated
	And I Save and Close the request validation Dialog
	And I initiate a new BA Reference form from the request form
	And I open the job from the request form
	And I pick the job
	And I undertake the valuation exercise
	When I release and publish the request
	Then the request is in status of 'Resolved'