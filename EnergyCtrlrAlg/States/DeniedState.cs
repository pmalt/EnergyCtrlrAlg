using System;

namespace EnergyCtrlrAlg.States
{
    public class DeniedState : RequestState
    {
        private int _requestProbability = 20;
        private SimultaneityCtrlr Ctrlr;


        public override void DecideRequestHandle()
        {
            // now requests charge with 20% probability
            Random rand = new Random();
            if (rand.Next(0,100) < _requestProbability)
            {
                RequestChargeHandle();
            }
            else
            {
                // wait
            }
        }

        public override void RequestChargeHandle()
        {
            if (Ctrlr.ChargeAccepted())
            {
                this.context.TransitionTo(new NeutralState(this.Ctrlr));
            }
        }

        public override void RequestUrgentHandle()
        {
            this.context.TransitionTo(new UrgentState(this.Ctrlr));
        }

        public override void ChangeProbabilityHandle(int newProb)
        {
            this._requestProbability = newProb;
        }

        public DeniedState(SimultaneityCtrlr ctrlr) : base(ctrlr)
        {
        }
    }
}