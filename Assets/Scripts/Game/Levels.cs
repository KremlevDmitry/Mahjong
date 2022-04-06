using System.Collections.Generic;
using UnityEngine;

public class Levels : Singleton<Levels>
{
    public struct RecordValue
    {
        public int ChapterId;
        public int LevelId;

        public RecordValue(int chapterId, int levelId)
        {
            ChapterId = chapterId;
            LevelId = levelId;
        }
    }

    private static class Record
    {
        private const string MAX_CHAPTER = "MaxChapter";
        private static int? chapterId = null;
        private static int _chapterId
        {
            get => chapterId ??= PlayerPrefs.GetInt(MAX_CHAPTER, 0);
            set
            {
                chapterId = value;
                PlayerPrefs.SetInt(MAX_CHAPTER, value);
            }
        }

        private const string MAX_LEVEL = "MaxLevel";
        private static int? levelId = null;
        private static int _levelId
        {
            get => levelId ??= PlayerPrefs.GetInt(MAX_LEVEL, 0);
            set
            {
                levelId = value;
                PlayerPrefs.SetInt(MAX_LEVEL, value);
            }
        }


        public static RecordValue Value
        {
            get => new RecordValue(_chapterId, _levelId);
            set
            {
                _chapterId = value.ChapterId;
                _levelId = value.LevelId;
            }
        }

        public static void Reset()
        {
            Value = new RecordValue(0, 0);
        }
    }



    [SerializeField]
    private List<Chapter> _chapters = default;


    public Level Current =>
        _chapters[Record.Value.ChapterId].Levels[Record.Value.LevelId];


    public void ResetProgress()
    {
        Record.Reset();
    }

    public void IncreaseProgress()
    {
        var current = Record.Value;

        current.LevelId++;
        if (current.LevelId > _chapters[current.ChapterId].Levels.Count)
        {
            current.LevelId = 0;
            if (current.ChapterId < _chapters.Count - 1)
            {
                current.ChapterId++;
            }
        }

        Record.Value = current;
    }
}
