using System;
using System.Collections.Generic;

namespace CrewBoomAPI
{
    public static class CrewBoomAPIDatabase
    {
        private static Dictionary<int, Guid> _userCharacters;

        public static bool IsInitialized { get; private set; }
        public static event Action OnInitialized;
        public static event Action<Guid> OnOverride;

        public static void Initialize(Dictionary<int, Guid> userCharacters)
        {
            _userCharacters = userCharacters;

            OnInitialized?.Invoke();
            IsInitialized = true;
        }

        public static bool GetUserGuidForCharacter(int character, out Guid guid)
        {
            guid = Guid.Empty;

            if (_userCharacters.TryGetValue(character, out guid))
            {
                return true;
            }

            return false;
        }
        public static void OverrideNextCharacterLoadedWithGuid(Guid guid)
        {
            OnOverride?.Invoke(guid);
        }
    }
}
