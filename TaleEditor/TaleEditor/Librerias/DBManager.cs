using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using TestingEditor;
using TaleEditor;
using System.Text;



namespace TaleEditor
{
    public class DBManager
    {
        private const string wFilename = "images.db";
        private const string vFilename = "Castellano_verbs.db";

        private SQLiteConnection wordConexion;
       private SQLiteConnection verbConexion;


        public DBManager()
        {

        }

        private void OpenDBWordConexion()
        {
           wordConexion = new SQLiteConnection("Data Source=" + wFilename + ";Version=3;UseUTF8Encoding=True;");
            try
            {
               wordConexion.Open();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        private void OpenDBVerbConexion()
        {
           verbConexion = new SQLiteConnection("Data Source=" + vFilename + ";Version=3;UseUTF8Encoding=True;");
            try
            {
                verbConexion.Open();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        private void CloseDBWordConexion()
        {
            wordConexion.Close();
            wordConexion.Dispose();
        }

        private void CloseDBVerbConexion()
        {
            verbConexion.Close();
           verbConexion.Dispose();
        }

        public String GetVerbs(String word)
        {
            List<Word> ret = new List<Word>();
            String resultado = "";

            OpenDBVerbConexion();

            SQLiteCommand command = new SQLiteCommand("SELECT verb FROM verbs WHERE form =" + "'" + word.ToLower() + "'", verbConexion);
            SQLiteDataReader result = command.ExecuteReader();

            resultado =result[0].ToString();

            command.Dispose();
            result.Close();
            CloseDBVerbConexion();

            return resultado;

        }


        public List<Word> GetWords(String word)
        {
            int index = 1;
            List<Word> ret = new List<Word>();

            Boolean isWord = false;

            OpenDBWordConexion();

            SQLiteCommand command = new SQLiteCommand("SELECT name, idT FROM main WHERE word =" + "'" + word.ToString().ToLower() + "'" + "and idL='0'", wordConexion);
            SQLiteDataReader result = command.ExecuteReader();
            if (result.HasRows)
            {
                while (result.Read())
                {
                    ret.Add(new Word(word + " " + index, word, result[0].ToString(), (WordType)int.Parse(result[1].ToString())));
                    index++;
                }
                isWord = true;
            }
            else
            {
                ret = null;
            }

            command.Dispose();
            result.Close();
            CloseDBWordConexion();

            String verb = GetVerbs(word);
            if (verb != "")
            {
                if (isWord == false)
                {
                    if (ret != null)
                    {
                        ret.Clear();
                    }

                    ret = new List<Word>();
                    index = 1;
                }

                OpenDBWordConexion();
                SQLiteCommand commandVerb = new SQLiteCommand("SELECT name, idT FROM main WHERE word =" + "'" + verb.ToString().ToLower() + "'" + "and idL='0'", wordConexion);
                SQLiteDataReader resultVerb = commandVerb.ExecuteReader();
                if (resultVerb.HasRows)
                {
                    while (resultVerb.Read())
                    {
                        ret.Add(new Word(word + " " + index, word, resultVerb[0].ToString(), (WordType)int.Parse(resultVerb[1].ToString())));
                        index++;
                    }
                }
                commandVerb.Dispose();
                resultVerb.Close();
                CloseDBWordConexion();



                //----- eliminar imagenes repetidas --------
                List<String> retImageName=new List<String>();

                // vuelco a temporal
                foreach (Word w in ret)
                {
                    //retImageName.AddRange(w.NameImage);
                    //ListafincasDuplicadas.Add(n);
                    // filtro
                    //Listafincas = ListafincasDuplicadas.Distinct().ToList();
                }

            }


            return ret;
        }

        public List<Word> GetSentence(List<String> phrase, List<Word> results = null)
        {
            Boolean found = false;

            if (results == null)
            {
                results = new List<Word>();
            }

            // Las palabras que no he encontrado en orden inverso
            List<String> newPhraseInverted = null;

            // Busco
            List<Word> consulta = SearchWords(Utils.BuildString(phrase));

            if (consulta.Count >= 1)
            {
                results.AddRange(consulta);

                found = true;
            }
            else
            {

                if (phrase.Count > 1)
                {
                    newPhraseInverted = new List<string>();
                    found = false;
                }

                if (phrase.Count == 1)
                {
                    results.Add(new Word(phrase.First(), phrase.First(), null, WordType.Ninguno));
                    found = true;
                }
            }

            while (!found && phrase.Count > 0)
            {
                newPhraseInverted.Add(phrase.Last());
                phrase.RemoveAt(phrase.Count - 1);

                consulta = SearchWords(Utils.BuildString(phrase)); //??

                if (consulta.Count >= 1)
                {
                    // Añado resultado a la lista
                    results.AddRange(consulta);
                    found = true;
                }
                else
                {
                    if (phrase.Count > 1)
                    {
                        found = false;
                    }

                    if (phrase.Count == 1)
                    {
                        results.Add(new Word(phrase.First(), phrase.First(), null, WordType.Ninguno));
                        found = true;
                    }

                }

            }

            if (newPhraseInverted != null && phrase.Count > 0)
            {

                // Repito con el resto
                newPhraseInverted.Reverse();
                return GetSentence(newPhraseInverted, results);
            }
            else
            {
                return results;
            }



        }

        public List<Word> SearchWords(String texto)
        {
            List<Word> test = new List<Word>();
            texto = texto.Trim(); // SI YA LO HACE LA FUNCIÓN A LA QUE LLAMAS EN LA LINEA SIGUIENTE ¿PARA QUE LO HACES AQUI? ?

            List<Word> consulta = this.GetWords(texto);
            
            if (consulta != null && consulta.Count >= 1)
            {
                test.AddRange(consulta);
            }

            return test;
        }

    }
    }
