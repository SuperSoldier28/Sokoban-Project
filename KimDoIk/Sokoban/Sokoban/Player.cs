using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sokoban
{
    internal class Player
    {
        private int playerx;
        private int playery;
        private string image;
        private Direction direction;

        public Player()
        {
            playerx = 0;
            playery = 0;
            image = "P";
            direction = Direction.None;
        }

        public void Render()
        {
            Console.SetCursorPosition(playerx, playery);
            Console.Write(image);
        }


        // 기능 => 메소드 => Player 타입을 다루는 인터페이스(Interface)
        public void Move()
        {

            if (key == ConsoleKey.LeftArrow)
            {
                playerx = Math.Max(0, playerx - 1);
                direction = Direction.Left;
            }
            if (key == ConsoleKey.RightArrow)
            {
                playerx = Math.Min(playerx + 1, 23);
                direction = Direction.Right;
            }
            if (key == ConsoleKey.UpArrow)
            {
                playery = Math.Max(0, playery - 1);
                direction = Direction.Up;
            }
            if (key == ConsoleKey.DownArrow)
            {
                playery = Math.Min(23, playery + 1);
                direction = Direction.Down;
            }
        }
    }
}
