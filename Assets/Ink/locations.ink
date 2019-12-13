VAR firequestgiven = false



// THESE SIGNPOSTS ARE NEAR THE FACTORY BASE

// THE FACTORY IS AT A CROSSROADS AND SPLITS OFF INTO FOUR DIRECTIONS
->MINE

=SIGNPOST_NORTH
^ the Static Mountains
->DONE

=SIGNPOST_EAST
< the Forest of Moss
->DONE

=SIGNPOST_WEST
> the Valley of Sand
->DONE

=SIGNPOST_SOUTH
v the Moulding Coast
->DONE



//UNIVERSAL PLACES

=MOTEL

You {~approach a|arrive at a|pull up at a|pull up outside a|roll up to a|drive up to a} {~small|dingy|rundown|well-used|dimly-lit|beige and intimidating|dusty|mostly empty|busy} motel.

+[Check in.]
#hack:motel
You check in to the motel.
+[Move on.]

-
->DONE




=GAS_STATION

You {~approach a|arrive at a|pull up at a|pull up outside a|roll up to a|drive up to a} {~small|rusty, old|rundown|well-used|new and glistening|quaint little|dusty|mostly empty|busy} gas station.

{~The neon lights glimmer across the forecourt.|The hum of your engine harmonises with the crickets.|The breeze is cool and sharp.|Crisp packets and cigarette butts crush beneath your feet.|The prices seem to have gone up again.|You forget for a moment whether your car takes diesel or unleaded.|You get a bit of oil on your finger from the cap.|The smell of the spilt oil on the ground is addicting, nostalgic.|A newspaper catches flight and dances along the ground in front.|You glance at the advertisments for jobs stuck to the window. It seems all these places have since closed down.|The cashier hums a song that you can't quite remember.|You consider the available snacks, but realise you probably can't afford any.}

+[Fill up.]
#hack:fuel
You fill the car up with gas.

+[Move on.]

-

->DONE

=SERVICE_STATION

You {~approach a|arrive at a|pull up at a|pull up outside a|roll up to a|drive up to a} {~glistening, gleaming|shining white|luminescent} service station.

{~It shines like a beacon in the hopeless darkness.|You can smell the grease from out in the car park.}

+[Fill up.]
#hack:fuel
You fill the car up with gas.
+[Check in.]
#hack:motel
You check in to the small on-site hotel.

+[Move on.]

-
->DONE

//MOSS FOREST PLACES

=GREENHOUSE

A complex network of dome-shaped greenhouses appear abandoned, hidden behind trees.

+[Go inside.]

It is humid inside the greenhouse. Long corridors are lined with moulding fruit trees, rotting wooden tables, plastic sheeting, ultraviolet lamps, disconnected pipes, puddles of water, bottles of fertiliser.

The air is sickly sweet.

Behind a glass wall is someone's office - messy- you can see some succulents in little pots, a gold scyth, a few jars of mushrooms.

**[Call out.]

#name:Driver #leftPortrait:Driver #rightPortrait:Druid #currentPortrait:left
Hello.

#name:Druid #currentPortrait:right
Hello? Who's there?
#name:Druid
You look like those folks over at the Power Station.
#name:Druid
If you want me to join you, I'm already there.
#name:Druid
I hate those pigs.
#name:Druid
What I need from you first, though, is all the fuel you've got in your tank.

    ***[Give fuel.]
    You give them all your fuel.
    
    #name:Druid
    Great. That's a lot of trust, friend. But thank you.
    #name:Druid
    I appreciate this.
    #name:Druid
    While I'm with you, we won't need fuel.
    #name:Druid
    I might come in handy with any natural obstacles or things like that, I worship the Moss God so I get perks.
    {CustomEvent("UNLOCK Druid")}

++[Leave.]
--
->DONE
+[Move on.]
-
->DONE

=FOREST_ENTRANCE

The entrance to the forest is blocked by fallen trees that have become blanketed in Moss over time. Inside, you can see the forest is dark, ominous, but patches of light peak through the dense canopy.

//if Druid
*[Clear the entrance.]

The Druid clears the moss and rotting wood from the entrance to the forest.

*[Move on.]
-
->DONE


=BIG_TREE

An ancient tree, older than time itself, stands colossal on a hill, casting its proud shadow across the fields.

It creaks and aches in the wind, bark peeling, battered, but still clinging on.

->DONE

=MOSS_SHRINE

A shrine to the Moss God rests, undisturbed, in a clearing dripping with old rainwater.

An orb of green glows, sustained in the still air, above the mossy stone blocks, radiating light around the waxy leaves.

*[Touch the orb.]
#faith:2
You touch the orb.
~faith += 2
//gain like 2 moss faith boy

+[Move on.]

-
->DONE

=MOSS_PILLARS

Crumbling stone pillars are stained from exhaust emissions. Moss grows up the West-facing side.

->DONE

=CARVING

On the side of a slope a large section of moss has been scraped away. From a distance you can see that it spells out a word, but can't tell what it is.

->DONE

=STONE_SLAB

A stone slab, fragile to touch, hides under layers of thick moss. A carving in it reads:

01101000 01101111 01110111 00100000 01100011 01100001 01101110 00100000 01110111 01100101 00100000 01100111 01110010 01101111 01110111 00100000 01101001 01101110 00100000 01110011 01110101 01100011 01101000 00100000 01100011 01101111 01101110 01100100 01101001 01110100 01101001 01101111 01101110 01110011 00111111 0001010

->DONE

=BURNING_BUSH

A large clump of lichen seems to be on fire. A wiry stream of smoke cuts through the muggy air.

->DONE


=CAMPFIRE

The ash heap of an extinguished campfire is damp from the rain. Water drips from the leaves overhead.

The camp seems to have been left in a hurry... half-empty bottles of beer, packets of crisps, backpacks, jackets. Any tracks left behind have been erased by the weather.

*[Search a backpack.]

You find coordinates noted on a scrap of paper.

They tell you to travel SOUTH WEST of the burning bush... Whatever that means.
+[Move on.]

-
->DONE




=CABIN

A log cabin hides among the pines and moss. 

Fire glow from the hearth behind the curtains lights the surrounding area orange, a porch with boots outside, an axe in a stump, a pile of logs, rocks, a large four wheel drive.

The curtains are red.

+[Knock on the door.]

#name:Hermit
Who's there at this sort of time?

    



+[Move on.]
-
->DONE

=QUARRY

A quarry, vast, exposes the ground by the river. 

->DONE
//MOULD PLACES

=HITCHHIKER

{A hitchhiker walks along the side of the road, thumb outstretched, backpack, clutching a large two-handed sword that emits some kind of holy light.|A backpack is abandoned at the side of the road.}

*[Pull over and pick them up.]

#leftPortrait:Driver #rightPortrait:Warrior #currentPortrait:right #name:Warrior
Thank Mould! Finally someone responds to my thumb! Been walking this road for moons and moons.
#name:Warrior
You folks are part of the uprising, right?
#name:Warrior
Fuck yeah let's smash some fascists.
#name:Warrior
So what's the deal? Are you heading out towards the coast? I've been trying to get to this cove down there... I can't remember the name.
#name:Warrior 
Those blasted wolves knocked out my memory cells when they jumped me! Damn dogs!
#name:Warrior
If we could stop by that would be cool.
{CustomEvent("UNLOCK Warrior")}
+[Move on.]

-
->DONE


=COVE

A discrete cove with white sands and a grey looping tide looks out onto the Lethe Sea.

{CharactersInTeam?Warrior:

#currentPortrait:left #leftPortrait:Warrior #name:Warrior
This is the place. I can feel it. There's something here.

The warrior walks down to the beach below.

#name:Warrior
Or... Is it? I'm confused.
#name:Warrior
Oh wait hold on, what's this?

The warrior finds, embedded into the cliff, a moulding shrine. Spores and geometric fungus spreading across the old idol.

#name:Warrior
Well, this isn't what I was remembering, but I suppose it will help.
#name:Warrior
We'll need all the divine favour we can get up against the Darkness.

The warrior prays.
{~->FAITH_1|The Mould God has nothing left for you here.}

-else:
You don't notice anything remarkable about the place, but it is beautiful. 

A coastline corroding away into the water, perfect for a moment, before the erosion continues and wipes it all away.

}
->DONE
=FAITH_1

~faith+=1

->DONE


=MOULD_SHRINE

->DONE

=CLIFF

A chalky path winds up the side of the cliff from the beach below. The ground around crumbles, rolls down, meets the ocean, is washed away.

->DONE


=LIGHTHOUSE

A red and white lighthouse is the tallest thing on the horizon, light beaming into the stormy waters.

*[Call for the lighthouse keeper.]

A

*[Move on.]
-
->DONE




=MUSHROOM_HOTSPRING





->DONE



//STATIC PLACES

=SOURCE

The 

->DONE

=MOTORWAY_BLOCKAGE

The motorway is blocked. Cars park across all the lanes, facing a projected image on a screen. Some kind of film showing?

You can make out what looks to be somebody wandering about in a hotel room.

*[Watch for a bit longer.]

The person sits on the bed, gets up, goes to the bathroom, orders room service, has a drink from the minibar, brushes their teeth, watches TV, goes to sleep, wakes up, has a shower, has breakfast, and leaves.

It keeps looping, and doesn't seem as though it will change.

**[Move on.]
--
+ [Move on.]

-


->DONE


=PYLON

A pylon beast creeps around the frozen rock. Limbs creaking with metallic exhaustion. It has a gentle sigh.

The wires connecting it to its friends fizz with potential energy.

*[Try to get its attention.]

You try to get the attention of the pylon beast, but it is preoccupied.

You watch as it wanders away into the snowy distance.


+[Move on.]
-

->DONE

=PYLONS

Pylons and pipelines scar the landscapes, like a trail to remember where home is... where home <i>was</i>.


->DONE

=TV_WALL

A 4 by 4 stack of TVs is half burried in a snow drift.

Through washes of static you can see fragments of a multichannel video piece. It depicts a group of people driving long distances in a small car, from different angles, cutting randomly between each shot.

->DONE

=SNOWMAN

A snowperson stands all alone in a field next to the road. They wear a smart hat, a scarf, smoke a pipe, and their carrot nose appears fresh.

#name:Snowperson #rightPortrait:Generic3 #currentPortrait:right
Psst. Friends.
#name:Snowperson
Rumour has it that just off the blabhalbah, by the blah a, there's a secret road that takes you to a hot spring.
#name:Snowperson
I would've check it out meself but... probably wouldn't be the best idea. Yknow?
#name:Snowperson
Hohoho.

->DONE

=STATIC_HOTSPRING

Clouds of steam emerge from a hotspring. The water bubbles and crackles, glowing energy, stained white rocks, specks of moss floating, hairs, a few people relaxing in the warm.

*[Take a swim.]

At first the water feels too hot, a shock to be out of the cold, but after a while you all become used the temperature.

#faith:1
You relax, trying not to think about the darkness approaching, and, for a brief moment, feel at peace.

~faith += 1
//+1 faith

+[Move on.]

-

->DONE

=ICICLE

You peer into an icy cavern where you can see a radio has been frozen inside a stalactite. It emits bursts of sound.

#toggleShaking:on
"We will not be forgotten!"
#toggleShaking:on
"We will fight!"
#toggleShaking:on
"It is going to keep snowing tonight!"
#toggleShaking:on
"Don't leave your bunkers!"

->DONE

=FROZEN_LAKE

A large flat-screen TV lies dormant beneath a frozen lake. The static texture on the screen makes the surface appear as if it is rippling, but it remains completely still.
->DONE

=FIRE

A lonesome traveller sits at a campfire with their dog and a backpack. They have gone blue from the cold and cry softly into their hands.

*[Talk to them.]
#name:Driver #rightPortrait:Generic1 #currentPortrait:left #leftPortrait:Driver
Hail and well met, traveller.
#name:Traveller #currentPortrait:right
Hey folks.
    ++["Are you ok?"]
    #name:Driver #currentPortrait:left
    Are you ok?    
    ++["How's it going?"]
    #name:Driver #currentPortrait:left
    How's it going?
    --
    #name:Traveller #currentPortrait:right
    I dunno, yeah, not too bad. Can't complain.
    #name:Traveller
    Pretty cold, though.
    ++["Where are you going?"]
    #name:Driver #currentPortrait:left
    Where are you going?
    ++["What are you doing?"]
    #name:Driver #currentPortrait:left
    What are you doing?
    --
    #name:Traveller #currentPortrait:right
    I'm in search of the Static God.
    #name:Traveller
    But to tell you the truth I'm quite lost.
    #name:Traveller
    I heard that They were residing in the Forest of Moss at the moment but I searched the place and found nothing.
    #name:Traveller
    Now I'm here at the Static Mountains, but I can't find anything here either.
    ++["Can we help?"]
    #name:Driver #currentPortrait:left
    Can we help?
    ++["What can we do?"]
    #name:Driver #currentPortrait:left
    What can we do?
    --
    #name:Traveller #currentPortrait:right
    I want this to be something I accomplish myself, but... I...
    #name:Traveller
    Could you deliver this? To my family home.
    #name:Traveller
    It's just a little letter I wrote, but it's really important to me.
    #name:Traveller
    We live East of the mine, just off the A49.
    #name:Traveller
    Thank you. I'll never forget this kindness.
    ~firequestgiven=true
+[Move on.]
-
->DONE

=NEWSAGENTS

What used to be a newsagents. It is pasted with newspapers, adverts, magazines so thickly that the door won't open.

->DONE

=CHIPPY

The familiar chippy smell is equally as strong as the cold smell of death and decay.

->DONE

=FIREQUEST

You realise it must be the family home of the traveller you met.

*[Knock.]

#name:Family #rightPortrait:Generic3 #leftPortrait:Driver #currentPortrait:right
Hello? Who's there?

++["We have a message from your child."]
#name:Driver #currentPortrait:left
We have a message from your child.
The family member takes the note, and you notice them begin to cry as they close the door.
->DONE
++["We are just messengers."]
#name:Driver #currentPortrait:left
We are just messengers.
~faith +=2
#faith:2
The family member takes the note, and you notice them begin to cry as they close the door.
//+2 faith boyyyyy
->DONE
+[Move on.]
->DONE


=SMALL_HOUSE

A small brick house, weathered from the wind. There are a few empty flower pots by the window.
{ firequestgiven == true:
	->FIREQUEST
-else:

You don't notice anything remarkable.
}


->DONE

=FOREST_TEMPLE

A large geometric temple, covered in moss, gentle green glow - it beckons you.

*[Step closer.]

#faith:3
You absorb the power of growth and life from the Moss God, who watches over, eyes glistening with love and pride.
~faith += 3
//+3 faith

+[Move on.]
-
->DONE
=COAST_TEMPLE

A large circular temple, covered in mould, gentle blue glow - it beckons you.

*[Step closer.]
#faith:3
You absorb the power of death and decay from the Mould God, who watches over, smiling like a proud parent.
~faith += 3
//+3 faith

+[Move on.]
-
->DONE
=MOUNTAIN_TEMPLE

A large abstracted temple, glitching and spluttering with static glow - it beckons you.

*[Step closer.]
#faith:3
You absorb the power of chaos and communication from the Static God, who watches over, flickering like a family member's ghost.
~faith += 3
//+3 faith

+[Move on.]
-
->DONE


=GLACIER

Up close to the glacier you can feel the pain of the ice as it melts gradually, and slides down the mountainside.

->DONE

//SAND PLACES

=STATUE

A once-great statue of the Sand God crumbles, half collapsed. The powerful stature reduced to a fading notion of what it used to be.

*[Pray.]

Nothing seems to happen. This God must not be listening.

+[Move on.]
-
->DONE


