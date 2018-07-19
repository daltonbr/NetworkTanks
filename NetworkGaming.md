# Network Gaming

## Networking Gaming Overview

**Game State**: If you take every instantiated game object in  your game, at any discrete point in time, the state of each object together in total forms a whole state for the entire game. This is the game state.

A saved game contains all the information needed to stop playing, and then start again later as though you'd never stopped. It's **serialized** game state.

Network serialization

### Peer to peer

Age of Empires - 1997
Every computer in the game is connect to every other computer.
Every move is simultaneously transmitted to all other player's computers.

That covers how the data are transmitted, but not how the game state is handled.
The original Peer to Peer systems use a **client authoritative** architecture.

**Client authoritative**: Each client is responsible for maintaning and communicating the state of the game. When conflicts arise, the local client has the **autority** locally to "fix" the conflict.

* Game play is limited to the speed of the slowest connection
* if two computers disagree on state, there is no "fair" way to resolve it

### Client Server

John Carmack - Quake iD Software

**Server Authoritative**: The game runs entirely on the server. Each client (i.e., player experience) is a simulation of the current state of the game on the server.

"I am now allowing the client to guess at the results of the users movement until the authoritative response from the server comes through. This is a biiiig architectural change. The client now needs to know about the solidity of objects, friction, gravity, etc. I am sad to see the elegant client-as-terminal setup go away, but I am practical above idealistic."
- John Carmack on QuackWorld.

This hybrid solution hides latency problems

## Unity: The High Level API (HLAPI)

Unity-Specific Networking
Low- and High-Level APIs

Application Programmer Interface (API)
Libraries or services that serve as a well-documented source of program functionality; typically implemented as a "black box", meaning you consume the services without necessarily knowing how the inner workings operate.

1. **High-Level API (HLAPI)** An implementation designed to be useful and productive, and built on top of the low-level API
2. **Low-level API (LLAPI)** Protocol-level implementation useful if you want to make your own multiplayer API

**Protocol:**  A set of network commands that everyone agrees to use. On the internet, this is the Transmission Control Protocol/Internet Protocol (TCP/IP)

### Low-Level API

* Operates at the protocol level
* Fine-grained control over features
* Ultimate flexibility
* Only used to build your own multiplayer API

### High-Level API

* Built on the Low-Level API
* Represents a logical implementation
* Covers most use cases (Disconnects or drop outs)
* Very easy to use

Implements a **client server** scheme where **one player** in the network game acts as the **server**, and all other players connect to that instance of the game. It covers this as the most common use case and it handles the most common detail work you'd need, such as what happens if someone disconnects or drops out.

## Notes

**CommandAttribute**
class in UnityEngine.NetworkingOther
This is an attribute that can be put on methods of NetworkBehaviour classes to allow them to be **invoked on the server** by sending a command **from a client**.

**Remote Procedure Call**
We put the prefix **Rpc** in our method. It's like the above Command, but in reverse.
An Rpc is a method that's issued on the server, but executed on the clients.