﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace MiniRogue
{
    abstract class Card
    {


        // Properties
        public string Name { get; set; }

        private bool flipped;

        public bool Flipped
        {
            get { return flipped; }
            set
            {
                flipped = value;
                if (value == true)
                {
                    CurrentTexture = CardTexture;
                }
                else { CurrentTexture = BackTexture; }
            }
        }


        private Rectangle cardRectangle;

        public Rectangle CardRectangle
        {
            get { return cardRectangle; }
            set { cardRectangle = value; }
        }

        public Texture2D BackTexture { get; set; }

        public Texture2D CardTexture { get; set; }

        public Texture2D CurrentTexture { get; set; }

        public float XPos { get; set; }

        public float YPos { get; set; }

        public float LevelXpos { get; set; }

        public float LevelYPos { get; set; }

        public MouseState CurrentMouseState { get; set; }

        public MouseState PreviousMouseState { get; set; }

        public BasicDie Dice { get; set; }

        public Dictionary<string, Button> Buttons { get; set; }

        public List<Button> CurrentButtons { get; set; }

        private Vector2 scaleVector;

        public Vector2 ScaleVector
        {
            get { return scaleVector; }
            set { scaleVector = value; }
        }

        public Random Rng { get; set; }


        //public Vector2 ScaleVector { get; set; }

        // Constructors
        public Card(string name, Texture2D cardTexture, Texture2D cardBack, Dictionary<string, Button> buttons)
        {

            CardTexture = cardTexture;
            BackTexture = cardBack;
            CurrentTexture = cardBack;
            CardRectangle = new Rectangle(0, 0, 494, 708);
            Dice = new BasicDie();
            Name = name;
            Buttons = buttons;
            ScaleVector = new Vector2(.43f, .43f);
            Rng = new Random();
            
        }

        // Methods

        public abstract void DrawCard(SpriteBatch sBatch,SpriteFont font);

        public abstract bool HandleCard(Player player, MouseState current, MouseState previous, float xPos, float yPos);


        public bool SingleMouseClick()
        {
           
            if (CurrentMouseState.LeftButton == ButtonState.Pressed && PreviousMouseState.LeftButton == ButtonState.Released)
            {
                return true;
            }
            return false;
        }






    }
}
