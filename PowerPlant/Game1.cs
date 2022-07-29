using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics;
using System.Collections.Generic;
namespace PowerPlant
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private PowerPlant pPlant;
        private InputManager inputMan;
        private int RPM;
        private int throttle;
        private int cPower;
        private int flywheelWeight;
        private int revLimit;
        private int idle;

        private int millisecondsPerFrame = 1; //Update every 1 millisecond
        private double timeSinceLastUpdate = 0; //Accumulate the elapsed time


        private SpriteFont Font; 


        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            pPlant = new PowerPlant();
            List<int> carParameters = pPlant.ReadPara();
            flywheelWeight = carParameters[0];
            revLimit = carParameters[1];
            idle = carParameters[2];
            
            inputMan = new InputManager();
            
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
            timeSinceLastUpdate += gameTime.ElapsedGameTime.TotalMilliseconds;
            if (timeSinceLastUpdate >= millisecondsPerFrame)
            {
                throttle = inputMan.Throttle(_graphics);
                RPM = pPlant.RPMmod(throttle, flywheelWeight, cPower, revLimit, idle);
                cPower = pPlant.Power(RPM);
                timeSinceLastUpdate = 0;
            }


                base.Update(gameTime);
        }


        protected override void Draw(GameTime gameTime)
        {
            
            GraphicsDevice.Clear(Color.LightGreen);
            Vector2 PositionT = new Vector2(50, 50);
            Vector2 PositionR = new Vector2(50, 100);
            pPlant.DrawMessage(_spriteBatch,Font,throttle,PositionT);
            pPlant.DrawMessage(_spriteBatch, Font,RPM , PositionR);




            base.Draw(gameTime);
        }
    }
}
