using System;
using System.Collections.Generic;
using System.IO;
namespace MyVoice.Model
{

    public class Phrase
    {
        public Phrase(string text, bool isInWordsList = false, bool isRecorded = false)
        {
            this.Text = text;
            this.IsRecorded = isRecorded;
            this.IsInWordsList = isInWordsList;
        }
        public bool IsRecorded { get; set; }
        public bool IsInWordsList { get; set; }
        public string Text { get; private set; }
        public override string ToString()
        {
            return Text;
        }
    }

    public class Phrases
    {
        private readonly List<Phrase> mPhrasesList = new List<Phrase>();
        private readonly Dictionary<string, Phrase> mPhrasesDict = new Dictionary<string, Phrase>();
        private readonly List<Phrase> mPhrasesToShow = new List<Phrase>();

        public void LoadList(string phrasesFile, string soundFilesPath)
        {
            Clear();
            // load words list
            string[] list = FileManager.ReadWordsFromFile(phrasesFile);
            foreach (string text in list)
                AddText(text, true);

            // load files list
            list = FileManager.GetWordsFromFilesList(soundFilesPath);
            foreach (string text in list)
                AddText(text, false);

            // copy phrases list to mPhrasesToShow
            mPhrasesToShow.AddRange(mPhrasesList);

            return;
        }

        private void AddText(string text, bool isFromList)
        {
            mPhrasesDict.TryGetValue(text, out Phrase phrase);
            if (phrase == null)
            {
                phrase = new Phrase(text);
                if (isFromList)
                    phrase.IsInWordsList = true;
                else
                    phrase.IsRecorded = true;

                mPhrasesList.Add(phrase);
                mPhrasesDict.Add(text, phrase);
            }
            else
            {
                if (isFromList)
                    phrase.IsInWordsList = true;
                else
                    phrase.IsRecorded = true;
            }
        }

        public List<Phrase> List
        {
            get
            {
                return mPhrasesToShow;
            }
        }

        private void Clear()
        {
            this.mPhrasesDict.Clear();
            this.mPhrasesList.Clear();
            this.mPhrasesToShow.Clear();
        }

    }

}