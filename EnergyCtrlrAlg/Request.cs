namespace EnergyCtrlrAlg
{
    public abstract class Request
    {
        public int RequestedCapacity;
        public Phases Phase;
    }

    public enum Phases
    {
        One,
        Two,
        Three,
        All
    }
}