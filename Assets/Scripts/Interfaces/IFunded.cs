namespace Interface
{
    public interface IFunded
    {
        public int GetMoney();
        public void AddMoney(int value);
        public void ReduceMoney(int value);
    }
}