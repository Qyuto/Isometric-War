using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Map
{
    public enum Tiles
    {
        None,
        Floor,
        Wall
    }
    
    public enum MapObjects
    {
        Enemy,
        Player,
        Boss,
        GarbageCollector
    }

    public class MapController : MonoBehaviour
    {
        private readonly List<Room> _rooms = new();
        private Tiles[,] _board;
        private GameObject[,] _boardObjects;
        [SerializeField] private int width, height;
        [SerializeField] private int minRoomSize, maxRoomSize;
        [SerializeField] private int corridorChance;
        [SerializeField] private int enemyCount;
        [SerializeField] private int maxEnemyPerRoom, minEnemyPerRoom;
        [SerializeField] private GameObject inst;
        [SerializeField] private List<GameObject> tiles;
        [SerializeField] private List<GameObject> mapObjects;


        private void Draw()
        {
            foreach (var room in _rooms)
            {
                inst.name = room.ToString();
                var roomInstance = Instantiate(inst, transform, true);
                foreach (var roomObject in room.RoomObjects)
                {
                    var x = (int)Random.Range(room.Rect.x + 1, room.Rect.xMax - 1);
                    var y = (int)Random.Range(room.Rect.y + 1, room.Rect.yMax - 1);
                    while (_boardObjects[x, y] != null)
                    {
                        x = (int)Random.Range(room.Rect.x + 1, room.Rect.xMax - 1);
                        y = (int)Random.Range(room.Rect.y + 1, room.Rect.yMax - 1);
                    }
                    _boardObjects[x, y] = roomObject;
                    Instantiate(roomObject, new Vector3(x, y),
                        Quaternion.identity);
                }
                for (var y = room.Rect.y; y <= room.Rect.yMax; y++)
                {
                    for (var x = room.Rect.x; x <= room.Rect.xMax; x++)
                    {
                        if ((x == room.Rect.x || x == room.Rect.xMax || y == room.Rect.y || y == room.Rect.yMax)
                            && _board[(int)(x + 1), (int)(y + 1)] == Tiles.None)
                        {
                            _board[(int)(x + 1), (int)(y + 1)] = Tiles.Wall;
                            Instantiate(tiles[(int)Tiles.Wall - 1], new Vector3(x, y),
                                    Quaternion.identity)
                                .transform.SetParent(roomInstance.transform);
                        }
                        else
                        {
                            _board[(int)(x + 1), (int)(y + 1)] = Tiles.Floor;
                            Instantiate(tiles[(int)Tiles.Floor - 1], new Vector3(x, y),
                                    Quaternion.identity)
                                .transform.SetParent(roomInstance.transform);
                        }
                    }
                }
            }

            var corridors = Instantiate(inst, transform, true);
            inst.name = "Corridor";
            for (var i = 1; i < width; i++)
            {
                for (var j = 1; j < height; j++)
                {
                    switch (_board[i + 1, j + 1])
                    {
                        case Tiles.Floor:
                            Instantiate(tiles[(int)Tiles.Floor - 1], new Vector3(i, j, 0f), Quaternion.identity)
                                .transform.SetParent(corridors.transform);
                            break;
                        case Tiles.None when CheckBoardPosition(i + 1, j + 1, Tiles.Floor):
                            Instantiate(tiles[(int)Tiles.Wall - 1], new Vector3(i, j, 0f), Quaternion.identity)
                                .transform.SetParent(corridors.transform);
                            break;
                        case Tiles.Wall:
                            break;
                    }
                }
            }
        }

        private bool CheckBoardPosition(int x, int y, Tiles tile)
        {
            return _board[x - 1, y - 1] == tile
                   || _board[x - 1, y] == tile
                   || _board[x - 1, y + 1] == tile
                   || _board[x, y - 1] == tile
                   || _board[x, y + 1] == tile
                   || _board[x + 1, y - 1] == tile
                   || _board[x + 1, y] == tile
                   || _board[x + 1, y + 1] == tile;
        }

        private void Generate(Room room, int step)
        {
            if (room.IsLeaf()
                && (room.Rect.width - minRoomSize >= maxRoomSize || room.Rect.height - minRoomSize >= maxRoomSize))
            {
                room.Split(minRoomSize);

                Generate(room.Left, step + 10);
                Generate(room.Right, step + 10);
                
                var direction = Random.Range(0.0f, 1.0f) > 0.5;
                CreateCorridor(room.Left, room.Right, room.IsHorizontal, direction);
                
                if (Random.Range(0, corridorChance) - step > 0
                    && !room.Left.IsLeaf() && !room.Right.IsLeaf())
                {
                    CreateCorridor(room.Left, room.Right, room.IsHorizontal, !direction);
                }
            }
            else
            {
                room.CreateRoom(minRoomSize);
                _rooms.Add(room);
            }
        }

        private void GenerateRoomObjects()
        {
            Room room;
            for (var mapObject = (int)MapObjects.Player; mapObject < mapObjects.Count; mapObject++)
            {
                room = _rooms[Random.Range(0, _rooms.Count)];
                while (room.RoomObjects.Count != 0)
                {
                    room = _rooms[Random.Range(0, _rooms.Count)];
                }
                room.RoomObjects.Add(mapObjects[mapObject]);
            }

            while (enemyCount > 0)
            {
                room = _rooms[Random.Range(0, _rooms.Count)];
                while (room.RoomObjects.Count != 0)
                {
                    room = _rooms[Random.Range(0, _rooms.Count)];
                }

                var enemyCountInRoom = Random.Range(Math.Min(minEnemyPerRoom, enemyCount),
                    Math.Min(maxEnemyPerRoom, enemyCount));
                for (var i = 0; i < enemyCountInRoom; i++)
                {
                    room.RoomObjects.Add(mapObjects[(int)MapObjects.Enemy]);
                }
                enemyCount -= enemyCountInRoom;
            }
        }

        private void CreateCorridor(Room room1, Room room2, bool isHor, bool mode)
        {
            var r1 = room1.GetRoom(room1.IsLeft, isHor, mode);
            var r2 = room2.GetRoom(room2.IsLeft, isHor, mode);
            var lPoint = new Vector2(Random.Range(r1.Rect.x + 2, r1.Rect.xMax - 2),
                Random.Range(r1.Rect.y + 2, r1.Rect.yMax - 2));
            var rPoint = new Vector2(Random.Range(r2.Rect.x + 2, r2.Rect.xMax - 2),
                Random.Range(r2.Rect.y + 2, r2.Rect.yMax - 2));

            if (lPoint.x > rPoint.x)
            {
                (lPoint, rPoint) = (rPoint, lPoint);
            }

            var w = (int)(lPoint.x - rPoint.x);
            var h = (int)(lPoint.y - rPoint.y);
            var corridors = new List<Rect>();

            if (w != 0)
            {
                corridors.Add(h < 0
                    ? new Rect(lPoint.x, lPoint.y, 1, Mathf.Abs(h))
                    : new Rect(lPoint.x, rPoint.y, 1, Mathf.Abs(h)));

                corridors.Add(new Rect(lPoint.x, rPoint.y, Mathf.Abs(w) + 1, 1));
            }
            else
            {
                corridors.Add(h < 0
                    ? new Rect((int)lPoint.x, (int)lPoint.y, 1, Mathf.Abs(h))
                    : new Rect((int)rPoint.x, (int)rPoint.y, 1, Mathf.Abs(h)));
            }
            
            foreach (var corridor in corridors)
            {
                for (var corridorX = (int)corridor.x; corridorX <= corridor.xMax; corridorX++)
                {
                    for (var corridorY = (int)corridor.y; corridorY <= corridor.yMax; corridorY++)
                    {
                        _board[corridorX + 1, corridorY + 1] = Tiles.Floor;
                    }
                }
            }
        }

        private void Start()
        {
            var root = new Room(new Rect(0, 0, width, height));
            _board = new Tiles[width + 2, height + 2];
            _boardObjects = new GameObject[width, height];
            Generate(root, 0);
            GenerateRoomObjects();
            Draw();
        }
    }
}