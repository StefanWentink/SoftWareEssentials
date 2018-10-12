namespace SWE.Reflection.Test.Data
{
    public abstract class AbstractStub : IStub
    {
        public int GetInt()
        {
            return 1;
        }

        public abstract string GetString();
    }
}