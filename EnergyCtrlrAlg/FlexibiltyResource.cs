using EnergyCtrlrAlg.States;

namespace EnergyCtrlrAlg
{
    public class FlexibilityResource
    {
        /// <summary>
        /// indicates current state of charge of FR
        /// max 100, min 0
        /// </summary>
        /// determine charge energy: capacity * efficiency
        public int Soc;
        
        /// <summary>
        /// amount of time available for charging
        /// </summary>
        public int TimeAvailable;
        
        /// <summary>
        /// indicates whether urgent mode was requested
        /// </summary>
        public bool UrgentRequested;
        
        /// <summary>
        /// status of FR
        /// 0 = idling
        /// 1 = charging
        /// 2 = charged
        /// </summary>
        public int Status;
        
        /// <summary>
        /// unique Id
        /// </summary>
        public int FrId;

        private RequestState _state;
        
        public FlexibilityResource(RequestState initialState, int frId)
        {
            this.TransitionTo(initialState);
            this.FrId = frId;
        }
        
        public void TransitionTo(RequestState newState)
        {
            this._state = newState;
            this._state.SetContext(this);
        }

        public void DecideRequest(FlexibilityResource fr)
        {
            this._state.DecideRequestHandle(fr);
        }

        public void RequestCharge()
        {
            this._state.RequestChargeHandle(this);
        }

        public void RequestUrgent()
        {
            this._state.RequestUrgentHandle();
        }
    }
}