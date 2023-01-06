using System;
using System.Threading.Tasks;

namespace EnergyCtrlrAlg.States
{
    public class DeniedState : RequestState
    {
        private int _requestProbability = 20;
        private SimultaneityCtrlr Ctrlr;


        public override async void DecideRequestHandle()
        {
            Random rand = new Random();
            if (rand.Next(0,100) < _requestProbability)
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
            if (chargeAccepted)
            {
                this.Context.TransitionTo(new NeutralState(this.Ctrlr));
            }
        }

        public override void RequestUrgentHandle()
        {
            this.Context.TransitionTo(new UrgentState(this.Ctrlr));
        }

        public override void ChangeProbabilityHandle(int newProb)
        {
            this._requestProbability = newProb;
        }

        public DeniedState(SimultaneityCtrlr ctrlr) : base(ctrlr)
        {
            Ctrlr = ctrlr;
        }
    }
}