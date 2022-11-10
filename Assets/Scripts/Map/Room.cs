using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Map
{
    public class Room
    {
        public Room Left, Right;
        public Rect Rect { get; private set; }
        public bool IsLeft, IsHorizontal;

        public Room(Rect rect)
        {
            this.Rect = rect;
        }

        public bool IsLeaf()
        {
            return Left == null && Right == null;
        }

        public void Split(int minRoomSize, int maxRoomSize=100)
        {
            bool splitH;
            if (Rect.width / Rect.height >= 1.25)
            {
                splitH = false;
            }
            else if (Rect.height / Rect.width >= 1.25)
            {
                splitH = true;
            }
            else
            {
                splitH = Random.Range(0.0f, 1.0f) > 0.5;
            }

            if (splitH)
            {
                var split = Random.Range(minRoomSize, (int)(Rect.height - minRoomSize));

                Left = new Room(new Rect(Rect.x, Rect.y, Rect.width, split));
                Right = new Room(new Rect(Rect.x, Rect.y + split, Rect.width, Rect.height - split));
            }
            else
            {
                var split = Random.Range(minRoomSize, (int)(Rect.width - minRoomSize));
                
                Left = new Room(new Rect(Rect.x, Rect.y, split, Rect.height));
                Right = new Room(new Rect(Rect.x + split, Rect.y, Rect.width - split, Rect.height));
            }

            IsHorizontal = splitH;
            Left.IsLeft = true;
        }

        public Room GetRoom(bool isLeft, bool isHor, bool mode)
        {
            if (IsLeaf())
            {
                return this;
            }

            if (isHor != IsHorizontal)
            {
                if (mode)
                {
                    var room1 = Left.GetRoom(isLeft, isHor, true);
                    return room1;
                }
                var room2 = Right.GetRoom(isLeft, isHor, false);
                return room2;
            }

            if (!isLeft)
            {
                var lRoom = Left.GetRoom(false, isHor, mode);
                return lRoom;
            }

            var rRoom = Right.GetRoom(true, isHor, mode);
            return rRoom;
        }


        public void CreateRoom(int minRoomSize)
        {
            if (!IsLeaf())
            {
                return;
            }
            var roomWidth = (int)Random.Range(Math.Max(Rect.width / 2, minRoomSize), Rect.width);
            var roomHeight = (int)Random.Range(Math.Max(Rect.height / 2, minRoomSize), Rect.height);
            var roomX = (int)Random.Range(1, Rect.width - roomWidth - 1);
            var roomY = (int)Random.Range(1, Rect.height - roomHeight - 1);

            Rect = new Rect(Rect.x + roomX, Rect.y + roomY, roomWidth, roomHeight);
        }
    }
}