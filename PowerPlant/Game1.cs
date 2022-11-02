using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics;
using System.Collections.Generic;
using System;

namespace PowerPlant
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private PowerPlant pPlant;
        private InputManager inputMan;
        private Sound soundMan;
        private MFTire mf;
        private ratioDiff thisRD;
        private int gear;
        private double RPM;
        private double TransSpeed;
        private double DiffSpeed;
        private double throttle;
        private double cPower;
        private double flywheelWeight;
        private double revLimit;
        private double idle;
        private int millisecondsPerFrame = 1; //Update every 1 millisecond
        private double timeSinceLastUpdate = 0; //Accumulate the elapsed time
        //private AudioEngine AudioMan;

        private SpriteFont Font; 


        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {

            thisRD = new ratioDiff();
            pPlant = new PowerPlant();
            List <float> carParameters = pPlant.ReadPara();
            flywheelWeight = (int)carParameters[0];
            revLimit = (int)carParameters[1];
            idle = (int)carParameters[2];
            
            inputMan = new InputManager();

            soundMan = new Sound();

            mf = new MFTire();

            
            //AudioMan = new AudioEngine();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            Font = Content.Load<SpriteFont>("File");

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            soundMan.sound();

            mf.Initialize();
            
            thisRD = pPlant.gearReader(gear = inputMan.GearSelect(_graphics));

            timeSinceLastUpdate += gameTime.ElapsedGameTime.TotalMilliseconds;
            if (timeSinceLastUpdate >= millisecondsPerFrame)
            {
                throttle = inputMan.Throttle(_graphics);
                RPM = pPlant.RPMmod(throttle, flywheelWeight, cPower, revLimit, idle,1);
                cPower = pPlant.Power(RPM);
                TransSpeed = pPlant.Transmission(thisRD, RPM);
                DiffSpeed = pPlant.Differential(thisRD, TransSpeed);

                timeSinceLastUpdate = 0;
            }


                base.Update(gameTime);
        }


        protected override void Draw(GameTime gameTime)
        {
            
            GraphicsDevice.Clear(Color.Black);
            Vector2 PositionThrot = new Vector2(550, 50);
            
            Vector2 PositionRPM = new Vector2(50, 50);
            Vector2 PositionG = new Vector2(50, 100);

            Vector2 PositionTrans = new Vector2(50, 350);
            Vector2 PositionDiff = new Vector2(50, 400);
            pPlant.DrawMessage(_spriteBatch,Font, ("Throttle:" + throttle.ToString()),PositionThrot);
            pPlant.DrawMessage(_spriteBatch, Font, "RPM:"+ Math.Round(RPM), PositionRPM);
            pPlant.DrawMessage(_spriteBatch, Font, "Trans RPM:" + Math.Round(TransSpeed), PositionTrans);
            pPlant.DrawMessage(_spriteBatch, Font, "Differential RPM:"+ Math.Round(DiffSpeed), PositionDiff);
            pPlant.DrawMessage(_spriteBatch, Font,"Gear: "+ gear, PositionG);



            base.Draw(gameTime);
        }
    }
}
