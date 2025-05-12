The whole game is built on a state machine, each state that responds to a global logical state of the game: bootstrap, loading saves, menus, gameplay and so on
Minimum serialisation in the scene, all dependencies are thrown through DI, namely Zenject ensuring low code coupling
Each object is created in a corresponding factory to ensure transparency of the life cycle of objects
The number of Updates is minimised and all behavioural updates occur through Tick in corresponding states.


![Movie_005online-video-cutter com-ezgif com-optimize](https://github.com/user-attachments/assets/03e0d4ea-7738-43b0-a776-7569207a191e)
