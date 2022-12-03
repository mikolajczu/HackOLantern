namespace temp
{
    class SpeedModifier : Modifier
    {
        private float value;
        
        public SpeedModifier(int duration, float value)
        {
            Duration = duration;
            this.value = value;
        }

        public override void Apply(MovingEntity entity)
        {
            entity.Speed += value;
        }

        public override void Undo(MovingEntity entity)
        {
            entity.Speed -= value;
        }
    }
}