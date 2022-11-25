using System;
using System.Threading.Tasks;

namespace EnergyCtrlrAlg.States
{
    public class UrgentState : RequestState
    {
        private int _requestProbability = 100;
        private bool _urgentRequested = false;
        private SimultaneityCtrlr Ctrlr;

       
        public override async void DecideRequestHandle()
        {
            Random rand = new Random();
            if (rand.Next(0, 99) < _requestProbability)
            {
                if (this.Context.Soc < 100)
                {
                    await RequestChargeHandle();
                }
            }
            else
            {
                // wait
            }
        }

        public override async Task RequestChargeHandle()
        {
            bool chargeAccepted = await this.Ctrlr.ChargeAccepted();
            // request charge from cp
            if (!chargeAccepted && !_urgentRequested)
            {
                this.Context.TransitionTo(new NeutralState(this.Ctrlr));
            }
        }

        public override void RequestUrgentHandle()
        {
            this._urgentRequested = true;
        }

        public override void ChangeProbabilityHandle(int newProb)
        {
            this._requestProbability = newProb;
        }

        public UrgentState(SimultaneityCtrlr ctrlr) : base(ctrlr)
        {
            Ctrlr = ctrlr;
        }
    }
}