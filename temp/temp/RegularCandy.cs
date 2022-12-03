using System.Drawing;

namespace temp
{
    class RegularCandy : Candy
    {
        private Player playerRef;
        
        public RegularCandy(Player player)
        {
            playerRef = player;
            image = Image.FromFile("../../Images/normiecandy.png");
        }

        public override void CauseEffect()
        {
            playerRef.CandyCount++;
        }
    }
}