using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace EnergyCtrlrAlg.States
{
    public class NeutralState : State
    {
        private int _requestProbability = 50;
        private SimultaneityCtrlr Ctrlr;
        private bool _urgentRequested = false;

        public override async void DecideRequestHandle(FlexibilityResource fr)
        {
            // now requests charge with 50% probability (_requestProbability = 50)
            Random rand = new Random();
            if (rand.Next(0,100) < _requestProbability)
            {
                // only request charge when battery is not fully charged
                if (this.Context.Soc < 100)
                {
                    await RequestChargeHandle(fr);
                }
            }
            else
            {
                // (fr id, soc(before/after), state, Ctrlr time, request sent, request accepted (y/n))
                string output =
                    $"{fr.FrId}, {fr.Soc}, {fr.Soc}, {this}, timeslot, {false}, {false}";
                await File.AppendAllTextAsync("/home/malte/RiderProjects/EnergyCtrlrAlg", output, Encoding.UTF8);
                // TODO wait
            }
        }

        public override async Task RequestChargeHandle(FlexibilityResource fr)
        { 
            bool chargeAccepted = await this.Ctrlr.ChargeAccepted(fr, this);
            // send message to cp
            if (!chargeAccepted && !this._urgentRequested)
            {
                this.Context.TransitionTo(new DeniedState(this.Ctrlr));
            }
        }

        public override void RequestUrgentHandle()
        {
            this.Context.TransitionTo(new UrgentState(this.Ctrlr));
            this._urgentRequested = true;
        }

        public NeutralState(SimultaneityCtrlr ctrlr) : base(ctrlr)
        {
            Ctrlr = ctrlr;
        }
    }
}