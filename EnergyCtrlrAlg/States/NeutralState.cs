using System;

namespace EnergyCtrlrAlg.States
{
    public class NeutralState : RequestState
    {
        private int _requestProbability = 50;
        private SimultaneityCtrlr Ctrlr;
        private bool _urgentRequested = false;

        public override void DecideRequestHandle()
        {
            // now requests charge with 50% probability
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
           // send message to cp
           if (!Ctrlr.ChargeAccepted() && !_urgentRequested)
           {
               this.context.TransitionTo(new DeniedState(this.Ctrlr));
           }
        }

        public override void RequestUrgentHandle()
        {
            this.context.TransitionTo(new UrgentState(this.Ctrlr));
            this._urgentRequested = true;
        }

        public override void ChangeProbabilityHandle(int newProb)
        {
            this._requestProbability = newProb;
        }

        public NeutralState(SimultaneityCtrlr ctrlr) : base(ctrlr)
        {
        }
    }
}