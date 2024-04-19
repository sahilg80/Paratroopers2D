Paratroopers Game

Welcome to the Paratroopers game! In this 2D Unity game, you control a player tasked with shooting down helicopters and paratroopers to earn points. Here's how it works:

    1. Starting the Game: Press the space button to begin.
    2. Gameplay: Helicopters will spawn randomly from both directions, dropping paratroopers. Use the space button to shoot them down.
    3. Scoring: Each time you hit a helicopter or a paratrooper, your score increases. You can see your score at the top of the UI panel.
    4. Game Over: If enough paratroopers land on the ground, they'll start advancing towards you. If they reach your position, it's game over.

Key Features:

    1. Scriptable Objects: Used to manage and configure data for helicopters, paratroopers, and the player.
    2. MVC Design Pattern: Manages entities like helicopters, paratroopers, players, and bullets.
    3. State Design: Controls the state of paratroopers, making it easy to manage their behavior.
    4. Object Pooling: Efficiently manages memory by reusing helicopter, paratrooper, and bullet objects.
    5. Paratrooper AI: Paratroopers have AI that makes them attack the player if enough of them land on the ground.