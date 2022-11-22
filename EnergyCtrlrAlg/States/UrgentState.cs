using System;

namespace EnergyCtrlrAlg.States
{
    public class UrgentState : RequestState
    {
        private int _requestProbability = 100;
        private bool urgentRequested = false;
        private SimultaneityCtrlr Ctrlr;

       
        public override void DecideRequestHandle()
        {
            Random rand = new Random();
            if (rand.Next(0, 99) < _requestProbability)
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
            // request charge from cp
            if (!this.Ctrlr.ChargeAccepted() && !urgentRequested)
            {
                this.context.TransitionTo(new NeutralState(this.Ctrlr));
            }
        }

        public override void RequestUrgentHandle()
        {
            this.urgentRequested = true;
        }

        public override void ChangeProbabilityHandle(int newProb)
        {
            this._requestProbability = newProb;
        }

        public UrgentState(SimultaneityCtrlr ctrlr) : base(ctrlr)
        {
        }
    }
}