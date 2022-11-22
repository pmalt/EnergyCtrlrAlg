namespace EnergyCtrlrAlg.States
{
    public class ChargedState : RequestState
    {
        public ChargedState(SimultaneityCtrlr ctrlr) : base(ctrlr)
        {
        }

        public override void DecideRequestHandle()
        {
            // do nothing
        }

        public override void RequestChargeHandle()
        {
            
        }

        public override void RequestUrgentHandle()
        {
           
        }

        public override void ChangeProbabilityHandle(int newProb)
        {
            
        }
    }
}