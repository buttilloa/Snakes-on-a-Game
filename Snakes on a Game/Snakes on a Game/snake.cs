﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Snakes_on_a_Game
{
    public class Snake
    {
        List<Vector2> snakeList = new List<Vector2>();
        public int Facing = 2; // 0= Down, 1= right, 2= up, 3= left \\
        public int Count = 0;
        public Boolean isAlive = true;
        public int DrawSize;
        int speed = 0;
        public Snake(int x, int y, int size)
        {
            speed = size;
            int temp = size;
            snakeList.Add(new Vector2(x, y));
            snakeList.Add(new Vector2(x, y+size));
            size += temp; ;
            snakeList.Add(new Vector2(x, y+size));
            size += temp; ;
            snakeList.Add(new Vector2(x, y+size));
            size += temp; ;
            snakeList.Add(new Vector2(x, y+size));
            Count = 5;
        }
        public void AddBlock()
        {
            snakeList.Add(new Vector2(snakeList[snakeList.Count-1].X,snakeList[snakeList.Count-1].Y+21));
            Count++;
        }
        public void Update()
        {
            if (isAlive)
            {
                Count = snakeList.Count;

                for (int i = snakeList.Count - 1; i > 0; i--)
                {
                    snakeList[i] = snakeList[i - 1];

                }
                if (Facing == 0)
                    snakeList[0] = new Vector2(snakeList[0].X, (snakeList[0].Y) + speed);
                if (Facing == 1)
                    snakeList[0] = new Vector2((snakeList[0].X + speed), (snakeList[0].Y));
                if (Facing == 2)
                    snakeList[0] = new Vector2(snakeList[0].X, (snakeList[0].Y) - speed);
                if (Facing == 3)
                    snakeList[0] = new Vector2((snakeList[0].X - speed), (snakeList[0].Y));
            }
        }
        public Boolean CheckCollisions(Rectangle EnemySnake,ref Snake snake2, GameWindow wind)
        {
            Rectangle Front = new Rectangle((int)snakeList[0].X, (int)snakeList[0].Y, 20, 20); 
            for (int i = 2; i < snakeList.Count; i++)
            {
                Rectangle Body = new Rectangle((int)snakeList[i].X, (int)snakeList[i].Y, 20, 20);
                if (EnemySnake.Intersects(Body)) snake2.isAlive = false;
                if (Front.Intersects(Body)) return true;

            }
            int X = (int)snakeList[0].X;
            int Y = (int)snakeList[0].Y;
            
            if (X > wind.ClientBounds.Width || X < 0 || Y > wind.ClientBounds.Height || Y < 0) return true;
            
            
            return false;
        }
       
        public Rectangle getFront()
        {
           Rectangle Front =  new Rectangle((int)snakeList[0].X, (int)snakeList[0].Y, 20, 20);
           return Front;
        }
        public Boolean DidEatPellet(Rectangle Pellet)
        {

            Rectangle Front = new Rectangle((int)snakeList[0].X, (int)snakeList[0].Y, 20, 20); 
            if(Pellet.Intersects(Front)){
                AddBlock();
              return true;
            } else return false;
    
        }
        
        public void SetSpeed(int NewSpeed)
        {
            speed = NewSpeed;
        }
        public Vector2 GetInstance(int X)
        {
           
            return snakeList[X];
        }
    }
}
