# ESC
Systems Programming Educational Game

## Contributors:
Sydnee Boothby -
sydnee.boothby@wsu.edu

Ingrid Llorente -
ingrid.llorente@wsu.edu

Bitna White -
bitna.white@wsu.edu

## Project Overview and Goals:
This project is an educational game designed with Unity, intended to teach systems programming concepts in an entertaining way. Learning complex computer science concepts can often be overwhelming, but metaphors can be a great way to learn, especially with cool gameplay on top. In our game, the player is transported to the world of a computer and must face different bosses, representing digital or physical computer components and functions, to ESC back to the real world.

## Description of Course Themes Used:
Each level or stage represents a different facet of linux program execution, each separated by the following levels in a narrative game:

MENU/START PAGE: Introduces main character, allows navigation to various stages. 

Level 1: Process Scheduling: Computer process enemies must be shot down in a specific order, coresponding to round robin, shortest job first, or  first-come-first-serve. If shot in the wrong order, the player recieves the damage instead. Enemies may require different numbers of shots/cycles to destroy.

Level 2: Memory Mapping (Physical to Virtual Memory and Caching): The player must traverse a maze within a limited amount of time. Path tiles may be cached or must be fetched from further/non virtual memory locations with time penalties. The player, like an operating system, wants to take the most effecient memory path and avoid costly fetches to further regions of the memory hierarchy. 

Level 3: Process Lifecycles and Forking: The player battles a computer process, but processes may split into children processes using fork(). Processes must be fought in a specific order, similar to how children processes must be executed before their parent processes when the wait() system call is evoked. 

Level 4: Control Flow and Signals: The player acts as a process and must dodge control signals like SIGKILL() that kill them. Other signals like SIGCHILD() and SIGSEGV() also fly towards the player, with penalties.  

FINAL BOSS: The ultimate boss of the computer is the kernel himself, personified as tux (but evil). The kernel decides the fate of all programs and thus whether or not the player can leave the computer world. The last level is an accumulation of gameplay from previous levels. If the kernel is defeated, the player gains kernel controls over the computer world and can use the exit() command to ESC. 

## Design Decisions and Trade Offs:
We decided to go with Unity as it used in industry and has many tutorials and resources for reference. Unity has a somewhat steep learning curve and uses C# for scripting, but we figured this would be a good challenge as it could prepare us for future work in game development and supports all the features we could possibly need to create a game. We also decided to make a majority of the assets on our own so our game is unique and has a cohesive feel. Lastly, separating our game into levels/Unity scenes made subdividing tasks easier as each of us could work on different levels simultaniously. Not all features of our level design could be completed in time as envisioned, but we ensured all course concepts were included and the structure was adequate to support future development. 

Concept Document (Includes narrative outline and concept art)
[ESC Game Ideas.pdf](https://github.com/user-attachments/files/27072093/ESC.Game.Ideas.pdf)


## Challenges Encountered and Lessons Learned:
Project planning had minor challenges at first, as not all group members had prior experience in game dev and we weren't sure how we wanted to apply course concepts. However, our group had a shared vision and all of us enjoy game design or playing games, so work progressed smoothly. We all also have extracurricular obligations and busy schedules, but consistent communication allowed us to reach our goals on time. Finally, making a narrative game in a single semester was an ambitious undertaking, but it allowed us to quickly learn industry standard tools and reinforce our learning of course concepts in a creative way. If we were to attempt a similar project in the future, a smaller, more focused scope may be easier to execute. 
