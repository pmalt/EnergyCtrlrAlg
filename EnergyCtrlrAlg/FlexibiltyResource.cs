using System;
using EnergyCtrlrAlg.States;

namespace EnergyCtrlrAlg
{
    public class FlexibilityResource
    {
        /// <summary>
        /// indicates current state of charge of FR
        /// max 100, min 0
        /// </summary>
        /// maybe use state of energy instead
        public int Soc;
        
        /// <summary>
        /// amount of time available for charging
        /// </summary>
        private int _timeAvailable;
        
        /// <summary>
        /// indicates whether urgent mode was requested
        /// </summary>
        public bool UrgentRequested = false;
        
        /// <summary>
        /// idling, charging, charged
        /// </summary>
        public StatusEnum Status = StatusEnum.Idling; 

        /// <summary>
        /// unique Id
        /// </summary>
        public readonly string FrId;

        private State _state;
        
        public FlexibilityResource(State initialState, string frId, int timeAvailable, int initialSoc, TimeSpan arrivalTime)
        {
            this.TransitionTo(initialState);
            this.FrId = frId;
            this._timeAvailable = timeAvailable;
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
        public enum StatusEnum
        {
            Idling,
            Charging,
            Charged
        }

    }
}