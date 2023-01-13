using System.ComponentModel;
using System.Dynamic;
using System.Security.Cryptography.X509Certificates;

namespace Sokoban
{
    enum Direction // 방향을 저장하는 타입
    {
        None,
        Left,
        Right,
        Up,
        Down
    }

    class Player
    {
        // private는 class Player 안에서만 사용이 가능합니다.
        private int playerx;
        private int playery;
        private string image;
        private Direction direction;

        // 접근자(Getter) & 설정자(Setter)
        // 접근자 : 데이터에 접근하는 메소드 => 외부에서 내 데이터 공개
        // 설정자 : 데이터를 설정하는 메소드 => 외부에 의해서 내 데이터 수정 가능

        // 접근자는 외부에 데이터를 열어주고, 설정자는 열어준 데이터를 수정할 수 있게끔 해줍니다.

        /// <summary>
        /// 생성자
        /// </summary>
        public Player()
        {
            playerx = 0;
            playery = 0;
            image = "P";
            direction = Direction.None;
        }

        public int GetX() => playerx; // 외부에서 접근해야 되기 때문에 public을 사용합니다.
        public int GetY() => playery; // 외부에서 접근해야 되기 때문에 public을 사용합니다.

        public void SetX(int x) => playerx = x; // 외부에서 접근해야 되기 때문에 public을 사용합니다.
        public void SetY(int y) => playery = y; // 외부에서 접근해야 되기 때문에 public을 사용합니다.

        public Direction GetPX() => direction;



        /// <summary>
        /// 플레이어를 그려준다.
        /// </summary>
        public void Render()
        {
            Console.SetCursorPosition(playerx, playery);
            Console.Write(image);
        }
        // 1. Method
        // Move

        /// <summary>
        /// 플레이어를 움직여줍니다.
        /// </summary>
        /// <param name="key"></param>
        public void PlayerMove(ConsoleKey key)
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
        // Render


        // 2. Data 
    }

    class Wall
    {
        private int wallX;
        private int wallY;
        private string image;
        /// <summary>
        /// 생성자
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public Wall(int x, int y)
        {
            wallX = x;
            wallY = y;
            image = "W";
        }

        public void Render()
        {
            Console.SetCursorPosition(wallX, wallY);
            Console.Write(image);
        }

        public void WallMove(Player player)
        {
            int playerX = player.GetX(); // playerX에 오픈된 GetX를 넣어줍니다.

            int playerY = player.GetY(); // playerX에 오픈된 GetY를 넣어줍니다.

            Direction _moveDirection = player.GetPX();

            if (playerX == wallX && playerY == wallY)
            {
                switch (_moveDirection)
                {
                    case Direction.Left:
                        playerX = wallX + 1;
                        break;

                    case Direction.Right:
                        playerX = wallX - 1;
                        break;

                    case Direction.Up:
                        playerY = wallY + 1;
                        break;

                    case Direction.Down:
                        playerY = wallY - 1;
                        break;

                    default:
                        Console.Clear();
                        Console.WriteLine($"[Error] 플레이어 이동 방향 데이터가 오류 입니다. : {_moveDirection}");

                        return;
                }
            }

            player.SetX(playerX); // 변경된 playerX 값을 바꿔주도록 설정해줍니다.
            player.SetY(playerY); // 변경된 playerY 값을 바꿔주도록 설정해줍니다.

        }
    }

    class Sokoban
    {
        static void Main()
        {


            // 초기 세팅
            Console.ResetColor();                                // 컬러를 초기화하는 것.
            Console.CursorVisible = false;                       // 커서 숨기기
            Console.Title = "할머니 위에서 행복하게 지내";         // 타이틀 설정한다.
            Console.BackgroundColor = ConsoleColor.DarkGreen;   // 배경색 설정
            Console.ForegroundColor = ConsoleColor.Yellow;      // 글꼴 설정
            Console.Clear();                                    //출력된 내용을 지운다.

            // 유지보수를 편하게 하기 위해 만들어줍니다.
            // 기호 상수 정의 : 박스와 골의 개수
            const int BOX_COUNT = 2;
            const int GOAL_COUNT = BOX_COUNT; // 골이랑 박스의 개수는 같다고 설정해줍니다. 

            // 벽의 개수를 설정합니다.
            const int WALL_COUNT = 3;

            // 플레이어 객체 생성자 호출
            Player player = new Player();

            // 벽 객체 생성자 호출
            Wall[] wall = new Wall[WALL_COUNT];
            wall[0] = new Wall(6, 6);
            wall[1] = new Wall(8, 15);
            wall[2] = new Wall(9, 8);

            // 플레이어의 이동 방향을 저장하기 위한 변수
            Direction playerMoveDirection = Direction.None;



            // 게임 루프 구성
            while (true)
            {
                //-----------------------Render--------------------- // 플레이어가 알 수 있게 화면을 그려준다.
                Render();

                //-----------------------ProcessInput--------------------- // 마지막 호출 이후 발생한 모든 사용자 입력을 처리합니다.
                ConsoleKey key = Console.ReadKey().Key;

                //------------------------Update-------------------------// 게임을 업데이트합니다.

                // 플레이어를 움직여줍니다.
                player.PlayerMove(key);

                for (int i = 0; i < WALL_COUNT; i++)
                {
                    wall[i].WallMove(player);
                }


            }

            #region Render 모듈화
            //Render 모듈화
            void Render()
            {
                Console.Clear();

                // 플레이어
                player.Render();


                // 벽
                for (int i = 0; i < WALL_COUNT; i++)
                {
                    wall[i].Render();
                }
            }

            // 오브젝트를 그립니다.
            void RenderObject(int x, int y, string image)
            {
                Console.SetCursorPosition(x, y);
                Console.Write(image);
            }

            void RenderObjectColor(int x, int y, string image, ConsoleColor color)
            {
                ConsoleColor _Color1 = Console.ForegroundColor;

                Console.ForegroundColor = color;
                Console.SetCursorPosition(x, y);
                Console.Write(image);

                Console.ForegroundColor = _Color1;
            }
            #endregion
        }
    }
}

/*
  // 첫 번째 박스, 두 번째 박스 위치를 저장하기 위한 변수를 배열로 작성
            int[] boxPositionX = { 5, 11 };
            int[] boxPositionY = { 7, 13 };

            // 벽 위치를 저장하기 위한 변수를 다양하게 만들기 위해 배열로 작성
            int[] wallX = new int[WALL_COUNT]{6, 8, 9 };
            int[] wallY = new int[WALL_COUNT]{ 6, 15, 8 };

            // 골 위치를 여러개 저장하기 위한 변수를 배열로 작성
            int[] goalPositionX = { 9, 7 };
            int[] goalPositionY = { 10, 7 };

            // 박스가 골 지점에 들어간지 체크하기 위한 배열
            bool[] goalPicture = new bool[GOAL_COUNT];

            // 박스 충돌 처리
            // 현재 내가 밀고 있는 박스가 몇번째 박스인지 알게 해주는 역할을 합니다.
            int pushedBoxId = 0;

<23.01.12. (목)>

   // 플레이어가 벽에 닿을 때 플레이어는 움직이지 못한다.
                // 벽의 개수를 다양하게 만들어줬기 때문에, 반복문을 사용합니다.
                for(int i = 0; i < WALL_COUNT; i++)
                {
                    if (playerX == wallX[i] && playerY == wallY[i])
                    {
                        switch (playerMoveDirection)
                        {
                            case Direction.Left:
                                playerX = wallX[i] + 1;
                                break;

                            case Direction.Right:
                                playerX = wallX[i] - 1;
                                break;

                            case Direction.Up:
                                playerY = wallY[i] + 1;
                                break;

                            case Direction.Down:
                                playerY = wallY[i] - 1;
                                break;

                            default:
                                Console.Clear();
                                Console.WriteLine($"[Error] 플레이어 이동 방향 데이터가 오류 입니다. : {playerMoveDirection}");

                                return;
                        }
                    }
                }

                // 플레이어가 박스를 밀때 박스가 움직일 수 있도록 만듭니다.
                // 박스의 개수를 다양하게 만들어줬습니다. 횟수를 알기 때문에 for문으로 만들어줍니다.
                for (int i = 0; i < BOX_COUNT; i++)
                {
                    if (playerX == boxPositionX[i] && playerY == boxPositionY[i])
                    {
                        switch (playerMoveDirection)
                        {
                            case Direction.Left:
                                boxPositionX[i] = Math.Max(0, playerX - 1);
                                playerX = boxPositionX[i] + 1;
                                break;

                            case Direction.Right:
                                boxPositionX[i] = Math.Min(23, playerX + 1);
                                playerX = boxPositionX[i] - 1;
                                break;

                            case Direction.Up:
                                boxPositionY[i] = Math.Max(0, playerY - 1);
                                playerY = boxPositionY[i] + 1;
                                break;

                            case Direction.Down:
                                boxPositionY[i] = Math.Min(23, playerY + 1);
                                playerY = boxPositionY[i] - 1;
                                break;

                            default:
                                Console.Clear();
                                Console.WriteLine($"[Error] 플레이어 이동 방향 데이터가 오류 입니다. : {playerMoveDirection}");

                                return;
                        }
                        pushedBoxId = i;
                        // 박스끼리 충돌 처리를 위해 만들어줍니다.
                        // 현재 내가 밀고 있는 박스가 몇번째 박스인지 알게 해주는 역할을 합니다.
                    }

                }

                // 플레이어가 박스를 밀 때 박스가 벽을 마주한다면 못움직여야한다.
                // 박스의 개수를 배열로 만들어줬습니다. 횟수를 알기 때문에 for문으로 만들어줍니다.
                // 벽의 개수를 배열로 만들어줬습니다.
                for (int i = 0; i < BOX_COUNT; i++)
                {
                    for (int count = 0; count < WALL_COUNT; count++) 
                    {
                        if (boxPositionX[i] == wallX[count] && boxPositionY[i] == wallY[count])
                        {
                            switch (playerMoveDirection)
                            {
                                case Direction.Left:
                                    boxPositionX[i] = wallX[count] + 1;
                                    playerX = boxPositionX[i] + 1;
                                    break;

                                case Direction.Right:
                                    boxPositionX[i] = wallX[count] - 1;
                                    playerX = boxPositionX[i] - 1;
                                    break;

                                case Direction.Up:
                                    boxPositionY[i] = wallY[count] + 1;
                                    playerY = boxPositionY[i] + 1;
                                    break;

                                case Direction.Down:
                                    boxPositionY[i] = wallY[count] - 1;
                                    playerY = boxPositionY[i] - 1;
                                    break;

                                default:
                                    Console.Clear();
                                    Console.WriteLine($"[Error] 플레이어 이동 방향 데이터가 오류 입니다. : {playerMoveDirection}");

                                    return;
                            }
                            break; // 플레이어는 1개의 박스밖에 못밉니다. 당연히 박스 1개만 벽에 부딪히겠죠.
                                   // 그렇다면 더 이상 반복을 돌 필요가 없습니다. 그래서 나와줘야합니다.
                                   // 나와주지 않는다면 실행 시간이 길어지고, 낭비하게 됩니다.
                        }
                    }
                }

                // 박스끼리 충돌 처리
                // colliedBoxId는 부딪힘을 당한 박스
                // pushedBoxId는 현재 내가 밀고 있는 박스
                for (int colliedBoxId = 0; colliedBoxId < BOX_COUNT; colliedBoxId++)
                {
                    // 예외 처리 : 같은 박스라면 처리할 필요가 없다.
                    if (pushedBoxId == colliedBoxId)
                    {
                        continue;
                        // continue를 하지 않고, break를 사용한다면 첫 번째 박스를 민다고 했을 때 첫 번째 박스가 두 번째 박스를 통과한다. 하지만 두 번째 박스를 밀때 첫 번째 박스한테 막힌다.
                        // 두 번째 박스를 밀때는 제대로 막힌다.
                        // 이유는 쉽다. 두번 째 박스는 pushedBoxId가 1, colliedBoxId가 0이라서 현재 조건식의 맞지 않아 밑에 있는 조건식으로 내려가기 때문이다.
                        // pushedBoxId == colliedBoxId 이 서로 같을 때 break를 한다면 현재 반복을 나가게 된다.
                        // 그러니 break가 아닌 continue를 사용해 pushedBoxId == colliedBoxId 이 서로 같을 때 밑에 명령문을 건너 뛰고 다시 반복할 수 있도록 만들어줘야한다.

                    }

                    // 플레이어가 밀고 있는 박스와 가만히 있는 박스가 부딪힌다면?
                    if (boxPositionX[pushedBoxId] == boxPositionX[colliedBoxId] && boxPositionY[pushedBoxId] == boxPositionY[colliedBoxId])
                    {
                        switch (playerMoveDirection)
                        {
                            case Direction.Left:
                                // 규칙 ( 왼쪽으로 밀었을 때)
                                // 1. 플레이어가 push한 박스는 collied된 박스보다 한칸 오른쪽에 있어야한다.
                                // 2. 플레리어는 push한 박스 오른쪽의 있어야한다.

                                boxPositionX[pushedBoxId] = boxPositionX[colliedBoxId] + 1;
                                playerX = boxPositionX[pushedBoxId] + 1;
                                break;

                            // 아래 문장의 규칙을 찾으면 위의 문장으로 간단하게 표현이 가능합니다.
                            //if (pushedBoxId == 0)
                            //{
                            //    boxPositionX[0] = boxPositionX[1] + 1;
                            //    playerX = boxPositionX[0] + 1;
                            //}
                            //else // pushedBoxId == 1
                            //{
                            //    boxPositionX[1] = boxPositionX[0] + 1;
                            //    playerX = boxPositionX[1] + 1;
                            //}
                            //break;

                            case Direction.Right:
                                boxPositionX[pushedBoxId] = boxPositionX[colliedBoxId] - 1;
                                playerX = boxPositionX[pushedBoxId] - 1;
                                break;

                            case Direction.Up:
                                boxPositionY[pushedBoxId] = boxPositionY[colliedBoxId] + 1;
                                playerY = boxPositionY[pushedBoxId] + 1;
                                break;

                            case Direction.Down:
                                boxPositionY[pushedBoxId] = boxPositionY[colliedBoxId] - 1;
                                playerY = boxPositionY[pushedBoxId] - 1;
                                break;
                            default:
                                Console.Clear();
                                Console.WriteLine($"[Error] 플레이어 이동 방향 데이터가 오류입니다. : {playerMoveDirection}");

                                return;
                        }

                    }
                }

                // 박스와 골의 처리
                // 골인 했을 때 게임 끝나도록 설정
                // 신경 써야 될 부분은 모든 박스가 골의 들어가야 끝이 나도록 만들어야한다.
                int boxCount = 0;

                for (int goalId = 0; goalId < GOAL_COUNT; goalId++) // 모든 골 지점에 대해서
                {
                    goalPicture[goalId] = false; // 박스가 골 지점(true)에서 벗어났으면(false)로 초기화시켜준다. ★을 G로 변경 시켜줘야한다.

                    for (int boxId = 0; boxId < BOX_COUNT; boxId++) // 모든 박스에 대해서
                    {
                        //  박스가 골 지점 위에 있는지 확인한다.
                        if (boxPositionX[boxId] == goalPositionX[goalId] && boxPositionY[boxId] == goalPositionY[goalId])
                        {
                            ++boxCount;
                            goalPicture[goalId] = true; // 박스가 골 위에 있다는 사실을 저장해준다.

                            // 더 이상 조사할 필요가 없으므로 탈출한다.
                            break;
                        }

                    }
                }

                // 모든 골 지점에 박스가 올라와있다는 것을 판별해주기 위해 개수를 새준다.
                if (boxCount == GOAL_COUNT)
                {
                    Console.Clear();
                    Console.WriteLine("축하합니다. 클리어 하셨습니다.");

                    break;
                }
 */