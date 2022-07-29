using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace PowerPlant
{
    class PowerPlant
    {
        public string line = "";
        public string[] powerArray = new string[10];
        public string[] parArray = new string[10];
        int RPM = 950;


        public int ReadPower(int RPM)
        {
            int currentPower = 0;
            StreamReader sRead = new StreamReader("C:/Users/Jakub/source/repos/PowerPlant/PowerPlant/Content/98SupraDyno.txt");
            double roundRPM = Math.Round(RPM / 500.0) * 500;

            //Debug.WriteLine(roundRPM);
            do
            {
                line = sRead.ReadLine();
                powerArray = line.Split(':');
                if (int.Parse(powerArray[0]) == roundRPM)
                {
                    currentPower = int.Parse(powerArray[1]);
                }

            }
            while (!sRead.EndOfStream);



            sRead.Close();
            return currentPower;
        }
        public List<int> ReadPara()
        {
            List<int> carPars = new List<int>();
            StreamReader sRead = new StreamReader("C:/Users/Jakub/source/repos/PowerPlant/PowerPlant/Content/98SupraParameters.txt");
            do
            {
                line = sRead.ReadLine();
                parArray = line.Split(':');
                carPars.Add(int.Parse(parArray[1]));
            }
            while (!sRead.EndOfStream);
            sRead.Close();
          

            return carPars;
        }
        public int Power(int RPM)
        {
            string[] allPower = new string[10];
            int powerAtRpm = 1;
            powerAtRpm = ReadPower(RPM);
            //Debug.WriteLine(powerAtRpm);
            return powerAtRpm;
        }
        public int RPMmod(int throttle,int fwWeight,int powerAtRpm,int revLimit,int idle) 
        {
           
            int RPMrise = ((throttle *2)*powerAtRpm) / fwWeight;

            int MagicNum = 3;
            if (RPM > 1500)
            {
                MagicNum = 2;

            }
            if (RPM < 1500)
            {
                MagicNum = 5;
            }
            if (RPM < 1000)
            {
                MagicNum = 25;
            }

            int RPMfall = (RPM / fwWeight)/MagicNum;
            //Debug.WriteLine("RPMRISE{0}", RPMrise);

            //Debug.WriteLine("throttle{0}", throttle);

            //Debug.WriteLine("powerAtRPM{0}", powerAtRpm);

            //Debug.WriteLine("fweight{0}", fwWeight);

            //Debug.WriteLine("RPMlimit{0}", revLimit);

            //Debug.WriteLine("RPMfall{0}", RPMfall);

           
            if (RPM < revLimit && RPM + RPMrise < revLimit+250) 
            {
                RPM = RPM + RPMrise;
            }
            if (RPM > revLimit)
            {
                RPM = RPM - 10;
            }
          


            if (RPM > idle)
            {
                RPM = RPM - RPMfall;
            }


            Debug.WriteLine(RPM);
                return RPM;
        }
        public void DrawMessage(SpriteBatch spriteBatch,SpriteFont Font, int PARA,Vector2 pos)
        {
            spriteBatch.Begin(SpriteSortMode.Deferred,
            BlendState.AlphaBlend,
            SamplerState.PointClamp,
            null, null, null, null);

            spriteBatch.DrawString(Font, PARA.ToString(), pos, Color.DarkBlue, 0, new Vector2(5,5), 2.5f, SpriteEffects.None, 0.5f);

            spriteBatch.End();

        }
    }
}