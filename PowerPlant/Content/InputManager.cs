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

            Debug.WriteLine("throttle{0}", throttle);

            return throttle;
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

