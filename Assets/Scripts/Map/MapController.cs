using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Map
{
    public enum MapObjects
    {
        None,
        Floor,
        Wall
    }

    public class MapController : MonoBehaviour
    {
        private readonly List<Room> _rooms = new();
        private MapObjects[,] _board;
        [SerializeField] private int width, height;
        [SerializeField] private int minRoomSize, maxRoomSize;
        [SerializeField] private List<GameObject> tiles;
        [SerializeField] private GameObject inst;
        [SerializeField] private int corridorChance;


        private void Draw()
        {
            foreach (var room in _rooms)
            {
                inst.name = room.ToString();
                var roomInstance = Instantiate(inst, transform, true);
                for (var y = room.Rect.y; y <= room.Rect.yMax; y++)
                {
                    for (var x = room.Rect.x; x <= room.Rect.xMax; x++)
                    {
                        if ((x == room.Rect.x || x == room.Rect.xMax || y == room.Rect.y || y == room.Rect.yMax)
                            && _board[(int)(x + 1), (int)(y + 1)] == MapObjects.None)
                        {
                            _board[(int)(x + 1), (int)(y + 1)] = MapObjects.Wall;
                            Instantiate(tiles[(int)MapObjects.Wall - 1], new Vector3(x, y),
                                    Quaternion.identity)
                                .transform.SetParent(roomInstance.transform);
                        }
                        else
                        {
                            _board[(int)(x + 1), (int)(y + 1)] = MapObjects.Floor;
                            Instantiate(tiles[(int)MapObjects.Floor - 1], new Vector3(x, y),
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
                    if (_board[i + 1, j + 1] == MapObjects.Floor)
                    {
                        Instantiate(tiles[(int)MapObjects.Floor - 1], new Vector3(i, j, 0f), Quaternion.identity)
                            .transform.SetParent(corridors.transform);
                    }
                    else if (_board[i + 1, j + 1] == MapObjects.None
                             && CheckBoardPosition(i + 1, j + 1, MapObjects.Floor))
                    {
                        Instantiate(tiles[(int)MapObjects.Wall - 1], new Vector3(i, j, 0f), Quaternion.identity)
                            .transform.SetParent(corridors.transform);
                    }
                }
            }
        }

        private bool CheckBoardPosition(int x, int y, MapObjects mapObject)
        {
            return _board[x - 1, y - 1] == mapObject
                   || _board[x - 1, y] == mapObject
                   || _board[x - 1, y + 1] == mapObject
                   || _board[x, y - 1] == mapObject
                   || _board[x, y + 1] == mapObject
                   || _board[x + 1, y - 1] == mapObject
                   || _board[x + 1, y] == mapObject
                   || _board[x + 1, y + 1] == mapObject;
        }

        private void Generate(Room room, int step)
        {
            if (room.IsLeaf()
                && (room.Rect.width - minRoomSize >= maxRoomSize || room.Rect.height - minRoomSize >= maxRoomSize))
            {
                room.Split(minRoomSize, maxRoomSize);

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
                if (Random.Range(0, 1) > 2)
                {
                    corridors.Add(new Rect(lPoint.x, lPoint.y, Mathf.Abs(w) + 1, 1));

                    corridors.Add(h < 0
                        ? new Rect(rPoint.x, lPoint.y, 1, Mathf.Abs(h))
                        : new Rect(rPoint.x, lPoint.y, 1, -Mathf.Abs(h)));
                }
                else
                {
                    corridors.Add(h < 0
                        ? new Rect(lPoint.x, lPoint.y, 1, Mathf.Abs(h))
                        : new Rect(lPoint.x, rPoint.y, 1, Mathf.Abs(h)));

                    corridors.Add(new Rect(lPoint.x, rPoint.y, Mathf.Abs(w) + 1, 1));
                }
            }
            else
            {
                corridors.Add(h < 0
                    ? new Rect((int)lPoint.x, (int)lPoint.y, 1, Mathf.Abs(h))
                    : new Rect((int)rPoint.x, (int)rPoint.y, 1, Mathf.Abs(h)));
            }
            foreach (var corridor in corridors)
            {
                for (var i = (int)corridor.x; i <= corridor.xMax; i++)
                {
                    for (var j = (int)corridor.y; j <= corridor.yMax; j++)
                    {
                        _board[i + 1, j + 1] = MapObjects.Floor;
                    }
                }
            }
        }

        private void Start()
        {
            var root = new Room(new Rect(0, 0, width, height));
            _board = new MapObjects[width + 2, height + 2];
            Generate(root, 0);
            Draw();
        }
    }
}