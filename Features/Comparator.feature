Feature: Comparator_Tool_
	Compares Hereditements in support of council tax band challenge
Background: 
	Given I am logged in to the 'voa_VoaCouncilTax' app as a 'caseworker'
	
Scenario: Ability to search comparables and show results	
	And I Open the comparator Tool
	When I search for an address with the following details
      #| SearchType | BuildingNameOrNumber | Street        | TownOrCity | PostCode |
      #| Address    | 10                   | Downing Street| London     | SW1A 2AA |
	  | PostCode |
	  | SW1A 2AA |
    Then the address search results should be displayed

Scenario: Compare Subject Hereditament to Comparables
	And I Open the comparator Tool
	And I search for an address with the following details
      #| SearchType | BuildingNameOrNumber | Street        | TownOrCity | PostCode |
      #| Address    | 10                   | Downing Street| London     | SW1A 2AA |
	  | PostCode |
	  | SW1A 2AA |
	And I select a subject hereditament
    And I select a comparables
	When I compare the comparable properties
	Then I am shown the acceptance Screen after the calculation is complete
