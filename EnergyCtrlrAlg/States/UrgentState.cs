using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace EnergyCtrlrAlg.States
{
    public class UrgentState : State
    {
        private int _requestProbability = 100;
        private bool _urgentRequested = false;
        private SimultaneityCtrlr Ctrlr;

       
        public override async void DecideRequestHandle(FlexibilityResource fr)
        {
            Random rand = new Random();
            if (rand.Next(0, 99) < _requestProbability)
            {
                if (this.Context.Soc < 100)
                {
                    await RequestChargeHandle(fr);
                }
            }
            else
            {
                // will not happen unless probability is changed
                // (fr id, soc(before/after), state, Ctrlr time, request sent, request accepted (y/n))
                string output =
                    $"{fr.FrId}, {fr.Soc}, {fr.Soc}, {this}, timeslot, {false}, {false}";
                await File.AppendAllTextAsync("/home/malte/RiderProjects/EnergyCtrlrAlg", output, Encoding.UTF8);
            }
        }

        public override async Task RequestChargeHandle(FlexibilityResource fr)
        {
            bool chargeAccepted = await this.Ctrlr.ChargeAccepted(fr, this);
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

        public UrgentState(SimultaneityCtrlr ctrlr) : base(ctrlr)
        {
            Ctrlr = ctrlr;
        }
    }
}