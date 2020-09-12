namespace ScavengerWorld.Units.Actions
{
    public class MoveAction : UnitAction
    {
        public enum Direction { NORTH, EAST, SOUTH, WEST }

        public Direction MoveDirection { get; private set; }
        public MoveAction(Direction direction)
        {
            MoveDirection = direction;
        }
    }
}
