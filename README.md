# Bloodstained Multiworld
The goal of this project is to bring multiworld (more specifically, [Archipelago](https://archipelago.gg/)) to **Bloodstained Ritual of the Night**

## Structure
* BloodstainedMemoryManipulator
  * Takes care of interacting with the running Bloodstained process and acts as an interface to run logic or read state from the running game.
* ArchipelagoWorld
  * Archipelago world implementation
* ArchipelagoClient
  * Takes care of communicating with an Archipelago server to send and receive items as well as commands.
* Tools
  * xor.py: used with `key.dat` to manually (de/en)crypt savefiles
  * pointers.CT: Cheat Table for Cheat Engine including info about various pointers in the game, as well as some code pointers and comments in the disassembly
* Notes
  * Includes random documents for note-taking.