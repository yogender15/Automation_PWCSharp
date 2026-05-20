@Ignore
Feature: Estates
	Estate Management for the VOS System
Background: 

Scenario: Estate Manager Creates an Estate File
		Given I am logged in to the 'voa_VoaCouncilTax' app as a 'caseworker'
		And I open and existing job

#	Given I am logged in to the 'voa_VoaCouncilTax' app as a 'caseworker'
#	And I create an estate file
#
#Scenario: Estate Manager Creates too many plots
#	When I create '300' plots
#	Then I am presented with an error
#
#Scenario: Estate Manager Creates plots
#	When I create '100' plots
#	Then The plots are created successfully
#
#Scenario: Estate Manager Renames House Type
#	And I rename a house type on the estate file
#	Then The house type is renamed successfully and stored in the audit history
#
#Scenario: Estate Manager Amends House Type
#	And I amend a house type on the estate file
#	Then Consequentials jobs are created successfully and stored in the audit history
#
#Scenario: Estate Manager Resets a plot
#	And I amend a house type on the estate file
#	Then Consequentials jobs are created successfully and stored in the audit history