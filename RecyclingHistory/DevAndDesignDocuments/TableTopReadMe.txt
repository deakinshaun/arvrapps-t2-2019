Unit information:

Cavalary: 
	Movement speed 3
	Weapons effect range 1.5
	Accuracy 65%, 35% chance of automatic destroy, otherwise damage range 10-20%
	Reload time 1 second
	

Infantry:
	Movement speed 1
	Weapons effect range 5
	Accuracy 50%, 60% chance of automatic destroy, otherwise damage range 10-40%
	Reload time 5 seconds

Artillery:
	Movement speed 0.5
	Weapons effect range 100
	Accuracy 30%, 100% chance of automatic destroy
	Reload time 8 seconds

Interface:
Menu:
	Start new single game (Start a new game playing against AI)
	Start new game with friend (Start a networked game)
	Resume game (As applicable)
	Game rules/info
	Quit game

In game:
	Attack/Move (Select area/unit to move to/attack)
	Toggle targets/movement (Will show bread crumbs to indicate where units have been designated to go/attack)
	Main Menu (Returns to main menu)


Logic/Game rules:

Battle: Once units have been given a target or location to move to, they will attack if/when in range. A unit 
will continue to attack a designated target until that target is destroyed or the aggressor unit is destroyed. If targets
are not allocated to a unit, they will remain stationary but shoot any enemy when they come within range.

Weapons effect range Artillery: If an artillery unit is selected to attack another unit that is out of range, haptic feedback
will be given and possibly an audio and/or message saying out of range.  In this chase no target will be allocated and the 
attacking artillery unit must be moved within range. This does not apply to infantry or cavalry units as they will move to be within 
location of any target selected.

	