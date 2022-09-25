using System;
using System.Collections.Generic;
using System.Text;

namespace MfTire
{
    class MFTire
    {
        float wheelAngularVelocity;
        float wheelHubLongitudinalVelocity;
        float verticalLoad;
        float nominalVerticalLoad;
        public void Initialize(float wheelAngularVelocity, float wheelHubLongitudinalForce, float verticalLoad, float nomialVerticalLoad)
        {

        }
        public float tireTreadLongitudinalVelocity(float WR, float WAV)
        {
            float TTLV = WR * WAV;


            return TTLV;
        }

        public float wheelSlipVelocity(float WHLV, float WR, float WAV)
        {
            float TTLV = tireTreadLongitudinalVelocity(WR, wheelAngularVelocity);
            float WSV = TTLV - WHLV;

            return WSV;
        }

        public float wheelSlip(float WHLV, float WR, float WAV)
        {
            float WSV = wheelSlipVelocity(WHLV, WR, WAV);
            float WS = WSV / wheelHubLongitudinalVelocity;

            return WS;
        }
        public float forceAtContactPoint(float verticalLoad, float wheelSlip)
        {



            return 1;
        }
    }

}

