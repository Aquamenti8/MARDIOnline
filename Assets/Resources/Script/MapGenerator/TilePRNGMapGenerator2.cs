using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TilePRNGMapGenerator2 : MonoBehaviour
{

    //public TileMap[] maps;                          //ini liste de tilemap          PUBLIC??
    public int mapIndex;                            //ini int index                 PUBLIC??

    public enum DrawMode { Classic, Smooth }        //ini enum drawmode
    public DrawMode drawMode;                       //ini a drawmode (can contain cmassic or smooth
     
    public Transform sprite;                        //recup la tansform d'un sprite PUBLIC??

    //TileMap currentMap;                             //appelle une tileMap currentMap
    public Coord mapSize;
    public float noiseScale;
    public int octaves;
    [Range(0, 1)]
    public float persistence;
    public float lacunarity;
    public Vector2 offset;
    public AnimationCurve tileHeightCurve;
    public int seed;
    [Range(0, .65f)]
    public float heightMultiplier = .5f;
    [Range(0, 1)]
    public float foliageRate = .5f;

    public Coord mapCenter
    {
        get
        {
            return new Coord(mapSize.x / 2, mapSize.y / 2);
        }
    }

    public bool autoUpdate;
    public TerrainType[] regions;

    void Start()
    {
        GenerateMap();
    }

    public void GenerateMap()
    {
        //currentMap = maps[mapIndex];                //set la currentMap comme étant la map numero Map index parmis les map
        float[,] noiseMap = Noise.GenerateNoiseMap(mapSize.x, mapSize.y, seed,
            noiseScale, octaves, persistence, lacunarity, offset);
                                                    //creer une noise Map, un tableau a 2 entrées qui prend plein de parametres qui n'existent malheureusement pas...
        System.Random prng = new System.Random(seed);


        //create map holder object
        string holderName = "Generated Map";
        if (transform.Find(holderName))
        {
            DestroyImmediate(transform.Find(holderName).gameObject);
        }

        Transform mapHolder = new GameObject(holderName).transform;
        mapHolder.parent = transform;

        //spawning tiles
        for (int x = 0; x < mapSize.x; x++)
        {
            for (int y = 0; y < mapSize.y; y++)
            {
                float tileHeight = noiseMap[x, y];
                TerrainType tileTerrain;
                for (int i = 0; i < regions.Length; i++)
                {
                    if (tileHeight <= regions[i].terrainHeight)
                    {
                        tileTerrain = regions[i];
                        Vector2 tilePosition = CoordToPosition(x, y);
                        float evaluatedHeight = tileHeightCurve.Evaluate(tileHeight) * 10;
                        int z = 0;
                        for (z = 0; z < Mathf.FloorToInt(evaluatedHeight); z++)
                        {
                            Transform newTile = Instantiate(sprite, tilePosition + (new Vector2(0f, heightMultiplier) * z), Quaternion.identity) as Transform;

                            newTile.parent = mapHolder;

                            SpriteRenderer spriteRenderer = newTile.GetComponent<SpriteRenderer>();
                            spriteRenderer.sprite = tileTerrain.sprite;
                            spriteRenderer.sortingOrder = ((x + 1) * (y + 1)) + z;
                        }
                        if (drawMode == DrawMode.Smooth && evaluatedHeight % 1 != 0f)
                        {
                            Transform newTile = Instantiate(sprite, tilePosition + (new Vector2(0f, heightMultiplier) * (z - 1)) + new Vector2(0f, (evaluatedHeight % 1) *heightMultiplier), Quaternion.identity) as Transform;

                            newTile.parent = mapHolder;

                            SpriteRenderer spriteRenderer = newTile.GetComponent<SpriteRenderer>();
                            spriteRenderer.sprite = tileTerrain.sprite;
                            spriteRenderer.sortingOrder = ((x + 1) * (y + 1)) + z + 1;
                            z++;
                        }

                        if (prng.Next(0, 100) < foliageRate * 100)
                        {
                            float foliageRandomNumber = prng.Next(100) * .01f;
                            for (int f = 0; f < regions[i].Foliage.Length; f++)
                            {
                                if (foliageRandomNumber <= regions[i].Foliage[f].cumulativeWeight)
                                {
                                    Vector2 topSmoothTileAddedHeight;
                                    if (drawMode == DrawMode.Smooth)
                                    {
                                        topSmoothTileAddedHeight = new Vector2(0f, (evaluatedHeight % 1) * heightMultiplier - .5f);
                                    }
                                    else
                                    {
                                        topSmoothTileAddedHeight = new Vector2(0, .15f);
                                    }
                                    Transform newFoliage = Instantiate(sprite, tilePosition + (new Vector2(0f, heightMultiplier) * (z - 1)) + topSmoothTileAddedHeight, Quaternion.identity) as Transform;

                                    newFoliage.parent = mapHolder;

                                    SpriteRenderer spriteRenderer = newFoliage.GetComponent<SpriteRenderer>();
                                    spriteRenderer.sprite = regions[i].Foliage[f].sprite;
                                    spriteRenderer.sortingOrder = ((x + 1) * (y + 1)) + z + 1;
                                    break;
                                }
                            }
                        }

                        break;
                    }
                }





            }
        }

    }

    Vector2 CoordToPosition(int x, int y)
    {
        float zeroX = 0f + .5f * x - .5f * y;
        float zeroY = (.25f * mapSize.y + .25f * mapSize.x) / 2f - .25f * y - .25f * x;
        return new Vector2(zeroX, zeroY);
    }

    [System.Serializable]
    public struct Coord
    {
        public int x;
        public int y;

        public Coord(int _x, int _y)
        {
            x = _x;
            y = _y;
        }
    }

    /*
    [System.Serializable]
    public class TileMap
    {
        public Coord mapSize;
        public float noiseScale;
        public int octaves;
        [Range(0, 1)]
        public float persistence;
        public float lacunarity;
        public Vector2 offset;
        public AnimationCurve tileHeightCurve;
        public int seed;
        [Range(0, .65f)]
        public float heightMultiplier = .5f;
        [Range(0, 1)]
        public float foliageRate = .5f;

        public Coord mapCenter
        {
            get
            {
                return new Coord(mapSize.x / 2, mapSize.y / 2);
            }
        }
    }
    */

    void OnValidate()
    {
        if (mapSize.x < 1)
        {
            mapSize.x = 1;
        }
        if (mapSize.y < 1)
        {
            mapSize.y = 1;
        }
        if (lacunarity < 1)
        {
            lacunarity = 1;
        }
        if (octaves < 0)
        {
            octaves = 0;
        }


    }
    [System.Serializable]
    public struct TerrainType
    {
        public string name;
        public float terrainHeight;
        public Sprite sprite;
        public FoliageType[] Foliage;
    }

    [System.Serializable]
    public struct FoliageType
    {
        public string name;
        public float cumulativeWeight;
        public Sprite sprite;
    }


}
