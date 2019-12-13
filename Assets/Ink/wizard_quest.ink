VAR antenna_pos_1 = false
VAR antenna_pos_2 = false
VAR antenna_pos_3 = false

     =OBSERVATORY
//SAM FILL THAT
Atop a snowcapped outcrop, an ancient observatory is built into the rock.

The domed roof is closed, presumably due to the weather.

+[Knock on the door.]
{ antenna_pos_1 && antenna_pos_2 && antenna_pos_3:
    #name:Wizard #leftPortrait:Driver #rightPortrait:Wizard #currentPortrait:right
    Great, seems as though you've sorted it out!
    #name:Wizard
    I'll be able to get us past the static forcefield that protects the more delicate areas of the region.
    #name:Wizard
    Hopefully the Static God will be on our side.
 
    //unlock wizard
    {CustomEvent("UNLOCK Wizard")}
    ->DONE
    -else:


#name:Wizard #leftPortrait:Driver #rightPortrait:Wizard #currentPortrait:right
Come inside, quick, it's freezing out.
#name:Wizard
You'll catch your death if you stay out there.
#name:Wizard
I foresaw why you are here. And I will help you. Down with those fucking pigs.
#name:Wizard
Down with that approaching Darkness!
#name:Wizard
But I can't come with you yet, I am destined to watch over the three antennas in the nearby area.
#name:Wizard
If you could just, like, drive around, find the three antennas and switch them onto standby...
#name:Wizard
You'll have to like... hold on just write this down. West Yellow, North Blue, East Red. Simple.
#name:Wizard
Go back down to the A7 and head West.


->DONE
}
+[Move on.]
-
->DONE


=ANTENNA_1

A dark ash-grey antenna with a West facing satellite dish.

At it's base are three buttons.
+[Push the first button]
The antenna emits yellow light.
~antenna_pos_1 = true
->ANTENNA_1
+[Push the second button]
The antenna emits blue light.
~antenna_pos_1 = false
->ANTENNA_1
+[Push the third button]
The antenna emits red light.
~antenna_pos_1 = false
->ANTENNA_1
+[Move on.]
-
->DONE


=ANTENNA_2

A shiny silver antenna with a North facing satellite dish.

At it's base are three buttons.
+[Push the first button]
The antenna emits yellow light.
~antenna_pos_2 = false
->ANTENNA_2
+[Push the second button]
The antenna emits blue light.
~antenna_pos_2 = true
->ANTENNA_2
+[Push the third button]
The antenna emits red light.
~antenna_pos_2 = false
->ANTENNA_2
+[Move on.]
-
->DONE

=ANTENNA_3

A rusty orange antenna with an East facing satellite dish.

At it's base are three buttons.
+[Push the first button]
The antenna emits yellow light.
~antenna_pos_3 = false
->ANTENNA_3
+[Push the second button]
The antenna emits blue light.
~antenna_pos_3 = false
->ANTENNA_3
+[Push the third button]
The antenna emits red light.
~antenna_pos_3 = true
->ANTENNA_3
+[Move on.]
-
->DONE
