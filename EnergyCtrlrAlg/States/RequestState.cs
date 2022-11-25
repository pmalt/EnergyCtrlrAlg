using System.Threading.Tasks;

namespace EnergyCtrlrAlg.States
{
    public abstract class RequestState
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

        public RequestState(SimultaneityCtrlr ctrlr)
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
        public abstract void DecideRequestHandle();

        /// <summary>
        /// request charge packet and change state if called by decideRequest
        /// </summary>
        public abstract Task RequestChargeHandle();

        /// <summary>
        /// request urgent charging, change state to UrgentState
        /// </summary>
        public abstract void RequestUrgentHandle();

        /// <summary>
        /// method to change request probability 
        /// </summary>
        /// <param name="newProb">percent number that probability should be changed to</param>
        public abstract void ChangeProbabilityHandle(int newProb);
    }
}