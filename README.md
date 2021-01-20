# MonkeysAndBananas 
### v1.0.0
#### https://rickyzonzon.itch.io/monkeys-and-bananas
#### https://gamejolt.com/games/Monkeysandbananas/577522
 Simple simulation of Darwinism and evolution in monkeys with C#. Created in Unity, developed all art assets with Piskel. Genetic evolution algorithm using uniform crossover and random resetting mutation.
 
 ### Monkey Basics
  Monkeys need energy in order to survive, but each monkey's energy depletes over time. To keep their energy from depleting to zero, monkeys have to eat bananas. Monkeys have several genes that contribute to a monkey's ability to find and eat bananas. Once a monkey's energy has reached a certian point, they are able to breed with another monkey. When two monkeys breed, another monkey spawns that has some of the genetic makeup of its two parents. The genes are inherited using uniform crossover and random resetting mutation; meaning, each parent has a 50 percent chance of passing on each gene to the child. There is a 27 percent chance that a monkey will undergo a mutation, where a random gene is picked and is reset to a random value. A mutated monkey will also not inherit color, but will be assigned a new random color.
  #### - Genes:
   - Color
   - Name: First name is random, last name is inherited from one of the parents.
   - Intelligence: determines the banana detection radius of a monkey
   - Size: determines the physical size of the monkey
   - Targeting Speed: determines the movement speed of the monkey while they are targeting a banana or another monkey
   - Wandering Speed: determines the movement speed of the monkey while they are searching for a target
   - Targeting Stamina: determines how much energy is lost over time while the monkey is targeting something
   - Wandering Stamina: determines how much energy is lost over time while the monkey is wandering
   - Max Climb: determines the maximum height of a tree that a monkey is able to climb to get a banana
   - Breeding Threshold: determines the minimum amount of energy necessary to breed
   - Energy Passover: determines how much energy is passed on to each child

### Main Menu & UI
  #### - Main Menu
   - Start: opens the game setup menu
    - Game Setup: the player is able to tweak several details about the simulation (it is recommended that the player leave them on the default settings for the first few plays)
   - About: opens a menu containing information about development
   - Quit: closes the game
  #### - In-Game UI
   - Stats Menu:
    - The button on the left side of the screen will open a tab that shows different information about the game (similar information that is in the game setup menu)
   - Button Menu (from right to left):
    - Close: end the game and return to the main menu
    - Settings: currently background music, ambient noise, monkey sfx, and ui sfx volume are the only things that are changable in the settings
    - Destroy: remove collidable objects (boulders, ponds, bamboo)
    - Pause/Play: pause and play the simulation, camera movement and zoom are still enabled
    - Create Monkey: spawn a new monkey with customizable genes
    - Create Tree: spawn a new tree/banana with customizable height and energy
    - Create Object: spawn an object
  #### - Controls:
    - The camera moves with the mouse and zooms with the scrollwheel. Press C to stop camera movement and zoom.
    - Left-Click:
     - [On monkey] Bring up info about the monkey and its genes, as well as the current state of the monkey (baby, bored, breedable, mutated)
      - Monkey States:
       - Baby: Once a monkey reaches 8 months old (in game time), it is no longer a baby. While a monkey is still a baby, it cannot breed.
       - Bored: If a monkey is tracking some target and is unable to reach the target after a certain amount of time, the monkey will become bored and stop tracking that target (mechanism to prevent being stuck for too long).
       - Breedable: If a monkey has enough energy to breed, it is breedable.
       - Mutated: If a monkey has had a gene mutation, it is mutated. 
     - [On tree] Bring up info about the tree, specifically its height and how much energy will be provided to the monkey
     - [Creating] Place the monkey/tree/object that you are creating
      - If the thing you are creating is red, you cannot place it where your cursor is. If the thing you are creating is green, you can place it in that spot.
    - Escape:
     - [Creating] Press escape if you want to cancel.
    
    
    
    
