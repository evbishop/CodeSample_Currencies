using System;

namespace CodeSample_Currency.SaveData
{
    [Serializable]
    public abstract class SaveData
    {
        public abstract void Save();
        public abstract void Load();

        protected virtual string Path => $"Playthrough/{GetType().Name}.es3";
    }
}
