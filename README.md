# Xenko.ClientServerSample

Simple Xenko Game and its .NET Core server that process remotely physics raycasts.

Press space or right click to "fire" and server will check if the sphere is hit.

## Build

Make sure to run the server first then the game.

Setting both projects as startup projects works fine too (right click on solution -> `Set Startup Projects`).

## Future

Right now the server uses Xenko API manually to load a scene, as a quick proof of concept.

Later it might be easier to have a `HeadlessGame` type to automatize loading of scene without graphics API and still be able to process specific C# scripts.