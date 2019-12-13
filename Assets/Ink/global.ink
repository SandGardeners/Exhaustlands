INCLUDE locations.ink
INCLUDE blacksmith_quest.ink
INCLUDE wizard_quest.ink
INCLUDE intro.ink
INCLUDE ending.ink
INCLUDE dialogues.ink
EXTERNAL CustomEvent(string event)

LIST CharactersInTeam = Cartographer, Warrior, Wizard, Druid, Gardener
VAR faith = 0

 == function CustomEvent(string event) ==
~return