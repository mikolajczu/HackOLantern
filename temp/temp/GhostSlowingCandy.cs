using System.Drawing;
namespace temp
{
    class GhostSlowingCandy : Candy
    {
        private Ghost ghostRef;

        public GhostSlowingCandy(Ghost ghost)
        {
            image = Image.FromFile("../../Images/slowcandy.png");
            ghostRef = ghost;
        }

        public override void CauseEffect()
        {
            ghostRef.AddModifier(new SpeedModifier(60 * 5, -5));
        }
    }
}