namespace EnergyCtrlrAlg
{
    // useless as of now, may be necessary later
    // especially if/when parameters are more accurate
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