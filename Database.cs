using System;
using System.Collections.Generic;

namespace BrcCustomCharactersAPI
{
    public static class Database
    {
        private static bool _hasOverride;
        private static Guid _nextOverride;
        private static Dictionary<int, Guid> _userReplacements;

        public static bool IsInitialized { get; private set; }
        public static event System.Action OnInitialized;
        public static event System.Action<Guid> OnOverride;

        public static void Initialize(Dictionary<int, Guid> userReplacements)
        {
            _userReplacements = userReplacements;

            OnInitialized?.Invoke();
            IsInitialized = true;
        }

        public static bool GetUserGuidForCharacter(int character, out Guid guid)
        {
            guid = Guid.Empty;

            if (_userReplacements.ContainsKey(character))
            {
                guid = _userReplacements[character];
                return true;
            }

            return false;
        }
        public static void OverrideNextCharacterLoadedWithGuid(Guid guid)
        {
            _nextOverride = guid;

            OnOverride?.Invoke(guid);
        }
    }
}
