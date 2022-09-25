using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics;

namespace PowerPlant
{
    class InputManager
    {
        int throttle = 0;
        int steering = 0;
        float controller;
        int Gear;

        public int Throttle(GraphicsDeviceManager inGraphics)
        {
            KeyboardState state;

            state = Keyboard.GetState();

            if (state.IsKeyDown(Keys.W))
            {
                if (throttle < 100)
                {
                    throttle = throttle + 2;
                }
                
            }
            if (state.IsKeyDown(Keys.W) == false)
            {
                if (throttle > 0)
                {
                    throttle = throttle -2;
                }
                if (throttle < 4) 
                {
                    throttle = 0;
                }


            }

            controller = (GamePad.GetState(PlayerIndex.One).Triggers.Right);
            controller = controller * 100;
            throttle = (int)controller;
            if (state.IsKeyDown(Keys.S))
            {

            }

            //Debug.WriteLine("throttle{0}", throttle);

            return throttle;
        }
        public int GearSelect(GraphicsDeviceManager inGraphics, List<float> carPars)
        {
            KeyboardState state;

            state = Keyboard.GetState();

            if (state.IsKeyDown(Keys.NumPad1))
            {
                Gear = 1;
            }
            if (state.IsKeyDown(Keys.NumPad2))
            {
                Gear = 2;
            }
            if (state.IsKeyDown(Keys.NumPad3))
            {
                Gear = 3;
            }
            if (state.IsKeyDown(Keys.NumPad4))
            {
                Gear = 4;
            }
            if (state.IsKeyDown(Keys.NumPad5))
            {
                Gear = 5;
            }
            if (state.IsKeyDown(Keys.NumPad6))
            {
                Gear = 6;
            }
            if (state.IsKeyDown(Keys.NumPad7))
            {
                Gear = 7;
            }
            if (state.IsKeyDown(Keys.NumPad8))
            {
                Gear = 8;
            }
            if (state.IsKeyDown(Keys.NumPad9))
            {
                Gear = 9;

            }
            if (state.IsKeyDown(Keys.NumPad0))
            {
                Gear = 10;
            }
            if (state.IsKeyDown(Keys.OemMinus))
            {
                Gear = 11;
            }

            return Gear;
        }
        public int Steering(GraphicsDeviceManager inGraphics)
        {

            KeyboardState state;

            state = Keyboard.GetState();

            if (state.IsKeyDown(Keys.A))
            {


            }

            if (state.IsKeyDown(Keys.D))
            {

            }
            return steering;
        }
    }
   
}

