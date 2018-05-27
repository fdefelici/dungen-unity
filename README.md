# Dungeon Generator Plugin for Unity
Plugin for Unity3d to procedurally generate dungeons with three methods:
* let the plugin generate meshes;
* provide to the plugin the assets to be used to dinamically compose the dungeon;
*  mixing provided assets and mesh generation.

# How it works
The procedural generation algorithm is based on `Cellular Automata` model, mixed with concept of `Room and Corridor`. By this, it is possible to benefict of the dynamicity of cellular automata (in terms of shape variations) but having full control over rooms and corridors generated (something that is not really feasible using only cellular automata model). 

The algorithm implemented can be customized througth Unity Inspector with a good set of input parameters, and can handle different kind of "rendering” such castle like, cave like or desert/forest like, by tuning input parameters and applying proper graphical assets. Generation is seed based in order to reproduce the same dungeon if same input parameters are used.

# Usage Examples
Import the project in Unity3D and under the `Scenes/` folder the are three sample scenes demostrating different dungeon generation:
* `CastleGen`: using castle-like assets ([example](./docs/castlegen-example.jpg))
* `CaveGen`: meshes created dinamically ([example](./docs/cavegen-example.jpg))
* `ForestGen`: mixed, using assets and mesh created dynamically ([example](./docs/forestgen-example.jpg))

To check it out, open one of the scenes described above e click Play button. You can tweak the parameters in real time using the Inspector. To regenerate the dungeon (while in Play mode) just click on the Game view.

# Build and Test
If you use Visual Studio:
* Open the project in Unity3D
* Open a .cs script and let Unity generate VS project
* In the VS project make sure to have installed NUnit and NUnit3TestAdapter to enable unit test discovery
* Use VS Test Runner to execute all the unit tests.
