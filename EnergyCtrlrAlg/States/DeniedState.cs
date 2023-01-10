using System;
using System.Threading.Tasks;

namespace EnergyCtrlrAlg.States
{
    public class DeniedState : State
    {
        private int _requestProbability = 20;
        private SimultaneityCtrlr Ctrlr;


        public override async void DecideRequestHandle(FlexibilityResource fr)
        {
            Random rand = new Random();
            if (rand.Next(0,100) < _requestProbability)
            {
                if (this.Context.Soc < 100)
                {
                    await RequestChargeHandle(fr);
                }
            }
            else
            {
                // wait
            }
        }

        public override async Task RequestChargeHandle(FlexibilityResource fr)
        {
            bool chargeAccepted = await this.Ctrlr.ChargeAccepted(fr, this);
            if (chargeAccepted)
            {
                this.Context.TransitionTo(new NeutralState(this.Ctrlr));
            }
        }

        public override void RequestUrgentHandle()
        {
            this.Context.TransitionTo(new UrgentState(this.Ctrlr));
        }

        public DeniedState(SimultaneityCtrlr ctrlr) : base(ctrlr)
        {
            Ctrlr = ctrlr;
        }
    }
}