﻿Feature: SearchHotelLogName
Search a hotel for one adult and log the hotel's name in TestNG

Scenario Outline: 1 - Verify navigate to particular Page
	Given I launch the Browser and navigate to Webpage
	When I verify the 'Goibibo - Best Travel Website. Book Hotels, Flights, Trains, Bus and Cabs with upto 50% off' Page is displayed
	Then I click on '<Element to Click>' Button
Examples:
    |   Element to Click | 
	|       Hotels       | 
	|       Flights      | 

Scenario: 2 - Enter Trip detils
	Given I select India and enter City name
	When I Select one adult under Rooms option
	And Click on the “Get Set Go” button
	Then I Log hotel name and hotel search count
