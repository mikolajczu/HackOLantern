using System.Drawing;

namespace temp
{
    class PlayerSpeedingCandy : Candy
    {
        private Player playerRef;

        public PlayerSpeedingCandy(Player player)
        {
            image = Image.FromFile("../../Images/speedcandy.png");
            playerRef = player;
        }

        public override void CauseEffect()
        {
            playerRef.AddModifier(new SpeedModifier(60 * 5, 10));
        }
    }
}