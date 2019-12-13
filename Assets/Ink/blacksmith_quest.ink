VAR blacksmith_quest_given = false
VAR blacksmith_quest_completed = false
VAR talked_to_blacksmith = false
=HOUSE_ON_RIVER

A small house with a thatched roof and a waterwheel is nested comfortably on the bank of the river among the reeds.

Outside a blacksmith hammers at an anvil.

+{CHOOSE_A_WEAPON==false}[Talk to the blacksmith.]
    
    { talked_to_blacksmith == false:
        #name:Blacksmith 
        #rightPortrait:Generic1 
        #currentPortrait:right
        Hail and well met, travellers. What can I do for you?
        ~talked_to_blacksmith = true
    }

    { CharactersInTeam ? Warrior:

            { blacksmith_quest_given == false:

                #name:Blacksmith #rightPortrait:Generic1 #currentPortrait:right
                Ah good, one of you is well-versed in decay!
                #name:Warrior #leftPortrait:Warrior  #currentPortrait:left
                Hail Mould!
                #name:Blacksmith #currentPortrait:right
                Hail Mould!
                #name:Blacksmith
                I can help your standing with the Mould God if you bring me some ore. Create you some new weapons.
            }
    
            #name:Blacksmith #rightPortrait:Generic1 #currentPortrait:right
            There is a mine down South from here, you'll have to cross back over the river. You should be able to find some fungal ore there.
            #name:Blacksmith
            Bring it to me.

            ~blacksmith_quest_given = true

             {  blacksmith_quest_completed:
               ->CHOOSE_A_WEAPON

                -else:
                    #name:Blacksmith
                    Until then we have nothing to talk about.
             }
            //gain like 2 levels of Mould faith

        -else:
            //If not warrior:
            #name:Blacksmith #rightPortrait:Generic1 #currentPortrait:right
            I'm afraid I don't think I'll be able to help you. 
            #name:Blacksmith
            You don't seem to have any members of your party who worship Decay.
            #name:Blacksmith
            Come see me another time.
    }

->DONE

+[Move on.]
->DONE

// //when you have the ore?

=CHOOSE_A_WEAPON
    
 #name:Blacksmith #rightPortrait:Generic1 #currentPortrait:right
                Ah, you found it, great.
                #name:Blacksmith
                Would you take a look at that... beautiful tin.
                #name:Blacksmith
                Haven't seen any so pure for so long!
                #name:Blacksmith
                Beautiful. I'll make this up into a weapon for you immediately. What would you like?

                *[Sword.]
                #name:Warrior #leftPortrait:Warrior #currentPortrait:left
                A sword!    
                *[Dagger.]
                #name:Warrior #leftPortrait:Warrior #currentPortrait:left
                A dagger!
                *[Axe.]
                #name:Warrior #leftPortrait:Warrior #currentPortrait:left
                An axe!

                -
                #name:Blacksmith #currentPortrait:right
                Perfect! One moment!
                #name:Blacksmith
                There you go!

                //+2 mould faith
                #name:Blacksmith #faith:2
                That'll show those fucking fascists what for!
                ~faith += 2
->DONE

=MINERS_HOUSE

A single miner's house remains where a cluster used to be. A large hole in the roof lets the rain in.

*[Knock.]

#leftPortrait:Driver #rightPortrait:Generic2 #name:Miner #currentPortrait:right
Who the blazes knocks on my door???
#name:Miner
Travellers? What do you want?

**[Ask about the mine.]
#name:Driver #currentPortrait:left
Can you tell us about the mine?
#name:Miner #currentPortrait:right
That blasted mine! Ruined me.
#name:Miner
Went blind in that mine. So many lost canaries. Miracle I'm still alive!
#name:Miner
And what do I have to show for it? A pension? A house? Sure. But what else? I've lost everything.
#name:Miner
So easy to get lost in there. Just have to remember the passage.
#name:Miner
North, East, North.
#name:Miner
I need to sit down. Get out of here!

**[Ask about the area.]
#name:Driver #currentPortrait:left
Can you tell us about the area?
#name:Miner #currentPortrait:right
What about it? I don't know it.
#name:Miner
The state of my body I can't leave this house.
#name:Miner
Lost my sight in that mine!
#name:Miner
Lost everything.
#name:Miner
Nothing around here anyway. Can't help you. Now get out!

+[Move on.]
-
->DONE





=MINE

An old brick mineshaft has punctured the ground and probes blindly into the sky. At the base, rubble fills up the doorway and mould grows over the chunks of rock and wood.

It is dark, dank, dripping with water, decaying. 

*[Enter the mine.]


You stand at a crossroads.
->MINE_CROSSROADS

+[Move on.]
->DONE

=MINE_CROSSROADS

+[Go North.]

The wooden support structures are rotting and creak as you walk past.


 ++[Go West.]

You walk through into another crossroads.

    +++[Go North.]
    You seem to find yourself back at the crossroads at the entrance.
    ->MINE_CROSSROADS
    
    +++[Go South.]
    You seem to find yourself back at the crossroads at the entrance.
    ->MINE_CROSSROADS
    
    +++[Go West.]
    You seem to find yourself back at the crossroads at the entrance.
    ->MINE_CROSSROADS

 ++[Go East.]

You pass a makeshift graveyard, with a miner's helmet atop each pile of dirt.



     +++[Go North.]
    
    You enter a large section of cave with glistening ore, some pickaxes, and pleasant flourescent lighting from some luminescent mushrooms.
    
        ****[Mine the ore.]
        //gain ore.
        
        You mine the ore.
        ~blacksmith_quest_completed = true

        ----
        The action must have unstabled the mine - untouched for so long - and the roof caves in.
        
        You manage to escape, and head back to the car.
        
        ->DONE
    
        +++[Go South.]
    
    You seem to find yourself back at the crossroads at the entrance.
    
    ->MINE_CROSSROADS
    
        +++[Go East.]
    
        You seem to find yourself back at the crossroads at the entrance.
    
    ->MINE_CROSSROADS

   

    ++[Go North.]

You walk through into another crossroads.

+++[Go North.]
    You seem to find yourself back at the crossroads at the entrance.
    ->MINE_CROSSROADS
    
    +++[Go East.]
    You seem to find yourself back at the crossroads at the entrance.
    ->MINE_CROSSROADS
    
    +++[Go West.]
    You seem to find yourself back at the crossroads at the entrance.
    ->MINE_CROSSROADS

+[Go South.]

You walk through into another crossroads. 
 ++[Go West.]

You walk through into another crossroads.

    +++[Go North.]
    You seem to find yourself back at the crossroads at the entrance.
    ->MINE_CROSSROADS
    
    +++[Go South.]
    You seem to find yourself back at the crossroads at the entrance.
    ->MINE_CROSSROADS
    
    +++[Go West.]
    You seem to find yourself back at the crossroads at the entrance.
    ->MINE_CROSSROADS
    
   
 ++[Go East.]

You walk through into another crossroads.

    +++[Go North.]
    You seem to find yourself back at the crossroads at the entrance.
    ->MINE_CROSSROADS
    
    +++[Go South.]
    You seem to find yourself back at the crossroads at the entrance.
    ->MINE_CROSSROADS
    
    +++[Go East.]
    You seem to find yourself back at the crossroads at the entrance.
    ->MINE_CROSSROADS
    
    
    
     ++[Go South.]

You walk through into another crossroads.

    +++[Go South.]
    You seem to find yourself back at the crossroads at the entrance.
    ->MINE_CROSSROADS
    
    +++[Go East.]
    You seem to find yourself back at the crossroads at the entrance.
    ->MINE_CROSSROADS
    
    +++[Go West.]
    You seem to find yourself back at the crossroads at the entrance.
    ->MINE_CROSSROADS


+[Go East.]

You walk through into another crossroads.


++[Go East.]

You walk through into another crossroads.

    +++[Go North.]
    You seem to find yourself back at the crossroads at the entrance.
    ->MINE_CROSSROADS
    
    +++[Go South.]
    You seem to find yourself back at the crossroads at the entrance.
    ->MINE_CROSSROADS
    
    +++[Go East.]
    You seem to find yourself back at the crossroads at the entrance.
    ->MINE_CROSSROADS
    
   
 ++[Go South.]

You walk through into another crossroads.

    +++[Go East.]
    You seem to find yourself back at the crossroads at the entrance.
    ->MINE_CROSSROADS
    
    +++[Go South.]
    You seem to find yourself back at the crossroads at the entrance.
    ->MINE_CROSSROADS
    
    +++[Go West.]
    You seem to find yourself back at the crossroads at the entrance.
    ->MINE_CROSSROADS
    
    
    
     ++[Go North.]

You walk through into another crossroads.

    +++[Go North.]
    You seem to find yourself back at the crossroads at the entrance.
    ->MINE_CROSSROADS
    
    +++[Go East.]
    You seem to find yourself back at the crossroads at the entrance.
    ->MINE_CROSSROADS
    
    +++[Go West.]
    You seem to find yourself back at the crossroads at the entrance.
    ->MINE_CROSSROADS


//if no warrior:
You can't cross the barricade with your current team.

->DONE
