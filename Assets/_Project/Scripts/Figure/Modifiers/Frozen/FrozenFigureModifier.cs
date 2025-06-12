namespace _Project.Scripts.Figure.Modifiers.Frozen
{
    public class FrozenFigureModifier : IFigureModifier
    {
        private readonly FrozenView _frozenView;

        public FrozenFigureModifier(FrozenView frozenView)
        {
            _frozenView = frozenView;
        }
        
        public void Apply()
        {
            _frozenView.Show();
        }
    }
}