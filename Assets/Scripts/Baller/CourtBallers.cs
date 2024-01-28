using System.Collections.Generic;

namespace Baller
{
    public class CourtBallers
    {
        private List<Baller> _activeBallers = new List<Baller>();

        public void AddBaller(Baller baller)
        {
            _activeBallers.Add(baller);
        }

        public void RemoveBaller(Baller baller)
        {
            _activeBallers.Remove(baller);
        }
    }
}