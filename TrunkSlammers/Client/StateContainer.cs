using System;
using TrunkSlammers.Shared;

namespace TrunkSlammers.Client
{
    public class StateContainer
    {
        public League League { get; set; } = null;
        public Event Event { get; set; } = null;

        public event Action OnLeagueChange;
        public event Action OnEventChange;

        public void SetLeague(League value)
        {
            League = value;
            NotifyLeagueChanged();
        }

        public void SetEvent(Event value)
        {
            Event = value;
            NotifyEventChanged();
        }

        private void NotifyLeagueChanged() => OnLeagueChange?.Invoke();
        private void NotifyEventChanged() => OnEventChange?.Invoke();

    }
}
