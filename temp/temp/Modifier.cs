namespace temp
{
    abstract class Modifier
    {
        public int Duration { get; set; }

        public abstract void Apply(MovingEntity entity);
        public abstract void Undo(MovingEntity entity);
    }
}