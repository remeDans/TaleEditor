using System;
using TestingEditor;


namespace TaleEditor
{
    public class Word
    {
        private String id, wordImage,nameImage;

        public String ID
        {
            get { return id; }
        }
        public String NameImage
        {
            get { return nameImage; }
        }
        //para identificar si pertenece a la misma palabra

        public String WordImage
        {
            get { return wordImage; }
        }

        private WordType type;
        public WordType Type
        {
            get { return type; }
        }
        public Word(String ID, String word, String name, WordType type)
        {
            this.id = ID;
            this.nameImage = name;
            this.wordImage = word;
            this.type = type;
        }

        
    }
}
