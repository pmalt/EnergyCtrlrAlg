using System.Threading.Tasks;

namespace EnergyCtrlrAlg.States
{
    public abstract class State
    {
        protected FlexibilityResource Context;
        
        /// <summary>
        /// probability a request is made in %
        /// </summary>
        private int _requestProbability;

        private SimultaneityCtrlr _ctrlr;

        /// <summary>
        /// indicates whether urgent mode was requested and state should be kept as UrgentState 
        /// </summary>
        private bool _urgentRequested = false;

        public State(SimultaneityCtrlr ctrlr)
        {
            _ctrlr = ctrlr;
        }

        public void SetContext(FlexibilityResource ctx)
        {
            this.Context = ctx;
        }

        /// <summary>
        /// use RequestProbability to decide whether or not to request a charge
        /// </summary>
        public abstract void DecideRequestHandle(FlexibilityResource fr);

        /// <summary>
        /// request charge packet and change state if called by decideRequest
        /// </summary>
        public abstract Task RequestChargeHandle(FlexibilityResource fr);

        /// <summary>
        /// request urgent charging, change state to UrgentState
        /// </summary>
        public abstract void RequestUrgentHandle();
    }
}