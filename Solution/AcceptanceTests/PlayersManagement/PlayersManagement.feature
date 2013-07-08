Feature: PlayersManagement
	In order to be able to manage club members and their data
	As an admin
	I want to be able to see a list of all players, add a new player to the club, modify player data and delete a player

Scenario: See all players
	Given I already have some players existing in the system
	When I navigate to the players list
	Then I will see a list with the two players 

Scenario: Add new player to the club
	Given I Enter the following information for a new player
	| Name | Email     | JoinDate   | ContactNo | Address  |
	| "A"  | "a@a.com" | 12/24/1986 | "123"     | "my adr" |
	When Submiting the player data
	Then The new player is added in the system

Scenario: Edit player data
	Given I have an existing player in the system
	And I change his profile data to the following
	| Name | Email     | JoinDate | ContactNo | Address |
	| "c"  | "h@h.com" | 1/1/2000 | "000"     | "a"     |
	When I submit the changes
	Then The modified data in stored in the system

Scenario: Remove player from club
	Given I have an existing player in the system
	When I delete the player
	Then The player is no longer stored in the system