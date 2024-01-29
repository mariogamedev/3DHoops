
namespace Baller
{ 
    public class MatchConfigurationData 
    {
        public int BallerID { get; }
        public int BallID { get; } 

        public MatchConfigurationData(int ballerID, int ballID)
        {
            BallerID = ballerID;
            BallID = ballID;
        }
    }
}