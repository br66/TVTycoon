namespace Beginning.CSharp
{
    public interface IPersisitable // can be saved
    {
        void Save();
    }

    public struct Player : IPersisitable
    {
        public int Score;
        public int Lives;

        public string Name;
    
        public void Save()
        {

        }
    }
}