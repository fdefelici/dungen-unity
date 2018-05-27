using UnityEngine;
using System;
using DungeonGeneration.Generator.Plotters;

namespace DungeonGeneration.Renderer {
    public class BRDungeonRenderer : ScriptableObject {
        private DungeonGeneratorBehaviour _behavior;

        private BRDungeonRenderer(DungeonGeneratorBehaviour behaviour) {
            _behavior = behaviour;
        }

        public void convertToMeshes(int[,] map) {
            Destroy(GameObject.Find("BoardHolder"));

            GameObject boardHolder = new GameObject("BoardHolder");
            boardHolder.transform.parent = _behavior.transform;

            addMainMashes(boardHolder, map);
            overlapWallSerators(boardHolder, map);
        }

        private void addMainMashes(GameObject boardHolder, int[,] map) {
            float floorSpan = 1;
            float halfFloorSpan = floorSpan * 0.5f;
            float oneQuarterFloorSpan = floorSpan * 0.25f;
            float threeQuartersFloorSpan = floorSpan * 0.75f;
            /*
            Vector3 size = _floorPrefab.GetComponentInChildren<MeshRenderer>().bounds.size;
            float xSpacing = size.x;
            float zSpacing = size.z;
            */
            for (int row = 0; row < map.GetLength(0); row++) {
                for (int col = 0; col < map.GetLength(1); col++) {
                    int value = map[row, col];
                    DetailedTileType type = (DetailedTileType)value;

                    if (type == DetailedTileType.Floor) {
                        GameObject prefab = _behavior._floorPrefab;
                        float xPos = col * floorSpan;
                        float zPos = -row * floorSpan;
                        float yRot = 0f;
                        istantiate(prefab, xPos, zPos, yRot, boardHolder, row, col, type.ToString());
                    } else if (type == DetailedTileType.Wall_N) {
                        GameObject prefab = _behavior._wallPrefab;
                        float xPos = col * floorSpan - floorSpan;
                        float zPos = -row * floorSpan;
                        float yRot = 180f;
                        istantiate(prefab, xPos, zPos, yRot, boardHolder, row, col, type.ToString());
                    } else if (type == DetailedTileType.Wall_E) {
                        GameObject prefab = _behavior._wallPrefab;
                        float xPos = col * floorSpan - floorSpan;
                        float zPos = -row * floorSpan + floorSpan;
                        float yRot = 270f;
                        istantiate(prefab, xPos, zPos, yRot, boardHolder, row, col, type.ToString());
                    } else if (type == DetailedTileType.Wall_S) {
                        GameObject prefab = _behavior._wallPrefab;
                        float xPos = col * floorSpan;
                        float zPos = -row * floorSpan + floorSpan;
                        float yRot = 0f;
                        istantiate(prefab, xPos, zPos, yRot, boardHolder, row, col, type.ToString());
                    } else if (type == DetailedTileType.Wall_W) {
                        GameObject prefab = _behavior._wallPrefab;
                        float xPos = col * floorSpan;
                        float zPos = -row * floorSpan;
                        float yRot = 90f;
                        istantiate(prefab, xPos, zPos, yRot, boardHolder, row, col, type.ToString());
                    } else if (type == DetailedTileType.Corner_INN_NW) {
                        GameObject prefab = _behavior._cornerInnPrefab;
                        float xPos = col * floorSpan;
                        float zPos = -row * floorSpan;
                        float yRot = 180f;
                        istantiate(prefab, xPos, zPos, yRot, boardHolder, row, col, type.ToString());
                    } else if (type == DetailedTileType.Corner_INN_NE) {
                        GameObject prefab = _behavior._cornerInnPrefab;
                        float xPos = col * floorSpan - floorSpan;
                        float zPos = -row * floorSpan;
                        float yRot = 270f;
                        istantiate(prefab, xPos, zPos, yRot, boardHolder, row, col, type.ToString());
                    } else if (type == DetailedTileType.Corner_INN_SE) {
                        GameObject prefab = _behavior._cornerInnPrefab;
                        float xPos = col * floorSpan - floorSpan;
                        float zPos = -row * floorSpan + floorSpan;
                        float yRot = 0f;
                        istantiate(prefab, xPos, zPos, yRot, boardHolder, row, col, type.ToString());
                    } else if (type == DetailedTileType.Corner_INN_SW) {
                        GameObject prefab = _behavior._cornerInnPrefab;
                        float xPos = col * floorSpan;
                        float zPos = -row * floorSpan + floorSpan;
                        float yRot = 90f;
                        istantiate(prefab, xPos, zPos, yRot, boardHolder, row, col, type.ToString());
                    } else if (type == DetailedTileType.Corner_OUT_NW) {
                        GameObject prefab = _behavior._cornerOutPrefab;
                        float xPos = col * floorSpan - halfFloorSpan;
                        float zPos = -row * floorSpan + halfFloorSpan;
                        float yRot = 180f;
                        istantiate(prefab, xPos, zPos, yRot, boardHolder, row, col, type.ToString());
                    } else if (type == DetailedTileType.Corner_OUT_NE) {
                        GameObject prefab = _behavior._cornerOutPrefab;
                        float xPos = col * floorSpan - halfFloorSpan;
                        float zPos = -row * floorSpan + halfFloorSpan;
                        float yRot = 90f;
                        istantiate(prefab, xPos, zPos, yRot, boardHolder, row, col, type.ToString());
                    } else if (type == DetailedTileType.Corner_OUT_SW) {
                        GameObject prefab = _behavior._cornerOutPrefab;
                        float xPos = col * floorSpan - halfFloorSpan;
                        float zPos = -row * floorSpan + halfFloorSpan;
                        float yRot = 270f;
                        istantiate(prefab, xPos, zPos, yRot, boardHolder, row, col, type.ToString());
                    } else if (type == DetailedTileType.Corner_OUT_SE) {
                        GameObject prefab = _behavior._cornerOutPrefab;
                        float xPos = col * floorSpan - halfFloorSpan;
                        float zPos = -row * floorSpan + halfFloorSpan;
                        float yRot = 0f;
                        istantiate(prefab, xPos, zPos, yRot, boardHolder, row, col, type.ToString());
                    }
                }
            }
        }

        public static BRDungeonRenderer newInstance(DungeonGeneratorBehaviour behaviour) {
            BRDungeonRenderer renderer = ScriptableObject.CreateInstance<BRDungeonRenderer>();
            renderer.setBehaviour(behaviour);
            return renderer;
        }

        private void setBehaviour(DungeonGeneratorBehaviour dungeonGeneratorBehaviour) {
            _behavior = dungeonGeneratorBehaviour;
        }

        private void overlapWallSerators(GameObject boardHolder, int[,] map) {
            GameObject prefab = _behavior._wallSeparatorPrefab;
            String objectName = "Wall_Separator";

            float floorSpan = 1f;
            float halfFloorSpan = floorSpan * 0.5f;
            float oneQuarterFloorSpan = floorSpan * 0.25f;
            float threeQuartersFloorSpan = floorSpan * 0.75f;

            for (int row = 0; row < map.GetLength(0); row++) {
                for (int col = 0; col < map.GetLength(1); col++) {
                    int value = map[row, col];
                    DetailedTileType type = (DetailedTileType)value;

                    if (type == DetailedTileType.Wall_N) {
                        float xPos = col * floorSpan;
                        float zPos = -row * floorSpan + oneQuarterFloorSpan;
                        float yRot = 180f;
                        istantiate(prefab, xPos, zPos, yRot, boardHolder, row, col, objectName);
                    } else if (type == DetailedTileType.Wall_E) {
                        float xPos = col * floorSpan - threeQuartersFloorSpan;
                        float zPos = -row * floorSpan;
                        float yRot = 270f;
                        istantiate(prefab, xPos, zPos, yRot, boardHolder, row, col, objectName);
                    } else if (type == DetailedTileType.Wall_S) {
                        float xPos = col * floorSpan - floorSpan;
                        float zPos = -row * floorSpan + threeQuartersFloorSpan;
                        float yRot = 0f;
                        istantiate(prefab, xPos, zPos, yRot, boardHolder, row, col, objectName);
                    } else if (type == DetailedTileType.Wall_W) {
                        float xPos = col * floorSpan - oneQuarterFloorSpan;
                        float zPos = -row * floorSpan + floorSpan;
                        float yRot = 90f;
                        istantiate(prefab, xPos, zPos, yRot, boardHolder, row, col, objectName);
                    } else if (type == DetailedTileType.Corner_INN_NW) {
                        float xPos = col * floorSpan + floorSpan;
                        float zPos = -row * floorSpan + oneQuarterFloorSpan;
                        float yRot = 180f;
                        istantiate(prefab, xPos, zPos, yRot, boardHolder, row, col, objectName);
                    } else if (type == DetailedTileType.Corner_INN_NE) {
                        float xPos = col * floorSpan - threeQuartersFloorSpan;
                        float zPos = -row * floorSpan - floorSpan;
                        float yRot = 270f;
                        istantiate(prefab, xPos, zPos, yRot, boardHolder, row, col, objectName);
                    } else if (type == DetailedTileType.Corner_INN_SE) {
                        float xPos = col * floorSpan - 2 * floorSpan;
                        float zPos = -row * floorSpan + threeQuartersFloorSpan;
                        float yRot = 0f;
                        istantiate(prefab, xPos, zPos, yRot, boardHolder, row, col, objectName);
                    } else if (type == DetailedTileType.Corner_INN_SW) {
                        float xPos = col * floorSpan - oneQuarterFloorSpan;
                        float zPos = -row * floorSpan + 2 * floorSpan;
                        float yRot = 90f;
                        istantiate(prefab, xPos, zPos, yRot, boardHolder, row, col, objectName);
                    } else if (type == DetailedTileType.Corner_OUT_NW) {
                        float xPos = col * floorSpan - floorSpan;
                        float zPos = -row * floorSpan + threeQuartersFloorSpan;
                        float yRot = 0f;
                        istantiate(prefab, xPos, zPos, yRot, boardHolder, row, col, objectName);
                    } else if (type == DetailedTileType.Corner_OUT_NE) {
                        float xPos = col * floorSpan - threeQuartersFloorSpan;
                        float zPos = -row * floorSpan;
                        float yRot = 270f;
                        istantiate(prefab, xPos, zPos, yRot, boardHolder, row, col, objectName);
                    } else if (type == DetailedTileType.Corner_OUT_SW) {
                        float xPos = col * floorSpan - oneQuarterFloorSpan;
                        float zPos = -row * floorSpan + floorSpan;
                        float yRot = 90f;
                        istantiate(prefab, xPos, zPos, yRot, boardHolder, row, col, objectName);
                    } else if (type == DetailedTileType.Corner_OUT_SE) {
                        float xPos = col * floorSpan;
                        float zPos = -row * floorSpan + oneQuarterFloorSpan;
                        float yRot = 180f;
                        istantiate(prefab, xPos, zPos, yRot, boardHolder, row, col, objectName);
                    }
                }
            }

        }

        private void istantiate(GameObject prefab, float xPos, float zPos, float yRot, GameObject parent, int mapX, int mapZ, String name) {
            Vector3 position = new Vector3(xPos, 0, zPos);
            GameObject instance = (GameObject)Instantiate(prefab, position, Quaternion.identity);
            instance.transform.Rotate(0, yRot, 0);
            instance.name = "(" + mapX + "," + mapZ + ") " + name;
            instance.transform.parent = parent.transform;

            instance.transform.localScale = new Vector3(0.125f, 0.125f, 0.125f);
        }

    }
}
