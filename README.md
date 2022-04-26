# test-api-game
Example with ASP .NET Core and SignalR for simple game

# How to run
To run end2end test you can use docker-compose file (or you might run applications in debug). 
Test emulates gameplay:
 - Players (host and second player) register in the system
 - Host creates the lobby
 - Second player searches opened lobbies
 - Second player joins the host's lobby
 - Players are describing gameplay in logs

You can see the gameplay in the logs of the battle-room-end2end-test service.

![img_1.png](img_1.png)