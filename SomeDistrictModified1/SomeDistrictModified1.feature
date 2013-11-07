Feature: SomeDistrictModified1
	In order to avoid silly mistakes
	As a math idiot
	I want to be told the sum of two numbers

@mytag
Scenario: Some DistrictModified
   Given the alteryx service is running at "http://gallery.alteryx.com"
   When I invoke GET at "api/Districts" for "Communications District"
   Then I see that district description contains the text "Need to establish B2B potential by geographic territory"

