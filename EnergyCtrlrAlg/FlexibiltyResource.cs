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
        public bool UrgentRequested = false;
        
        public enum Status
        {
           idling,
           charging,
           charged
        }

        public Status FrStatus = Status.idling; 

        /// <summary>
        /// unique Id
        /// </summary>
        public int FrId;

        private State _state;
        
        public FlexibilityResource(State initialState, int frId, int timeAvailable, int initialSoc)
        {
            this.TransitionTo(initialState);
            this.FrId = frId;
            this.TimeAvailable = timeAvailable;
            this.Soc = initialSoc;
        }
        
        public void TransitionTo(State newState)
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