using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Media.Imaging;
using System.Linq;
using System.Security.AccessControl;
using System.Drawing;

namespace TaleEditor
{
    public static class Utils
    {

        public static DBManager dbManager=new DBManager();
        public static char[] delimiterChars = { ' ', ',', '.', ':', '\t' }; //no puede ser constante


        public static List<String> CutSentence(String cadena)
        {
            List<String> frase = new List<string>(cadena.Split(delimiterChars));
            return frase;
        }

        public static String BuildString(List<String> listString)
        {
            String ret = "";
            
            for(int i = 0; i < listString.Count; i++)
            {
                ret += listString[i];
                if(i < listString.Count-1)
                    ret += " ";
            }

            return ret;
        }


        /*public static List<Word> GetSentenceByGroups(List<String> phrase, List<Word> results = null)
        {
            Boolean found = false;

            if (results == null)
            {
                results = new List<Word>();
            }

            // Las palabras que no he encontrado en orden inverso
            List<String> newPhraseInverted = null;

            // Busco
            List<Word> consulta = Utils.SearchWords(BuildString(phrase));
           
            if (consulta.Count >= 1)
            {
                results.AddRange(consulta);
                
                found = true;
            }
            else
            {
                newPhraseInverted = new List<string>();

                found = false;

                foreach(String p in phrase)
                  {
                      if (!p.Contains(' '))//&& !found
                    {
                        results.Add(new Word("", Utils.GetStringWidthoutAccents(p), "", TestingEditor.WordType.Ninguno));

                        //results.Add(null);
                    }
                  }
            }

            while (!found && phrase.Count > 0)
            {
                newPhraseInverted.Add(phrase.Last());
                phrase.RemoveAt(phrase.Count - 1);

                consulta = Utils.SearchWords(BuildString(phrase)); //??

                if (consulta.Count >= 1)
                {
                    // Añado resultado a la lista
                    results.AddRange(consulta);
                    found = true;
                }
                else
                {
                    found = false;
                    foreach (String p in phrase)
                      {
                          if (!p.Contains(' '))//&& !found
                        {
                            results.Add(new Word("", Utils.GetStringWidthoutAccents(p), "", TestingEditor.WordType.Ninguno));
                            //results.Add(null);
                        }
                      }
                 
                }

            }

            if (newPhraseInverted != null && phrase.Count > 0)
            {
                // Repito con el resto
                newPhraseInverted.Reverse();
                return GetSentenceByGroups(newPhraseInverted, results);
            }
            else
            {
                
                return results;
            }

        }*/



       /* public static List<Word> SearchWords(String texto)
        {
            List<Word> test = new List<Word>();
            texto = texto.Trim(); // SI YA LO HACE LA FUNCIÓN A LA QUE LLAMAS EN LA LINEA SIGUIENTE ¿PARA QUE LO HACES AQUI?

            List<Word> consulta = Utils.SearchWord(texto);
            if (consulta.Count >= 1)
            {
                test.AddRange(consulta);
            }

            return test;
        }*/

        public static bool hasAccents(String word)
        {
            bool ret = false;

            if(word.Contains('á')|| word.Contains('é')|| word.Contains('í')|| word.Contains('ó')|| word.Contains('ú'))
            {
                ret = true;
            }

            return ret;
        }

        public static bool hasVowel(String word)
        {
            bool ret = false;

            if (word.Contains('a') || word.Contains('e') || word.Contains('i') || word.Contains('o') || word.Contains('u')||word.Contains('á') || word.Contains('é') || word.Contains('í') || word.Contains('ó') || word.Contains('ú'))
            {
                ret = true;
            }
            else
            {
                ret = false;
            }

            return ret;
        }


        public static String GetStringWidthoutAccents(String word)
        {
            // TAN COMPLEJO TENÍA QUE SER QUITAR LOS ACENTOS?

            word.Trim();
            word.ToLower();
            // int i = 0;

            word.Replace('á', 'a');
            word.Replace('é', 'e');
            word.Replace('í', 'i');
            word.Replace('ó', 'o');
            word.Replace('ú', 'u');

            return word;

            /*String cadena = word;
            String auxCadena = cadena;



            foreach (Char cha in cadena)
            {
                switch (cha)
                {
                    case 'á':
                        auxCadena = cadena;
                        auxCadena = auxCadena.Insert(i, "a");
                        auxCadena = auxCadena.Remove(i + 1, 1);
                        break;

                    case 'é':
                        auxCadena = cadena;
                        auxCadena = auxCadena.Insert(i, "e");
                        auxCadena = auxCadena.Remove(i + 1, 1);
                        break;

                    case 'í':
                        auxCadena = cadena;
                        auxCadena = auxCadena.Insert(i, "i");
                        auxCadena = auxCadena.Remove(i + 1, 1);
                        break;

                    case 'ó':
                        auxCadena = cadena;
                        auxCadena = auxCadena.Insert(i, "o");
                        auxCadena = auxCadena.Remove(i + 1, 1);
              
                        break;
                    case 'ú':
                        auxCadena = cadena;
                        auxCadena = auxCadena.Insert(i, "u");
                        auxCadena = auxCadena.Remove(i + 1, 1);
                        break;
                    default:
                        break;
                }
                i++;
            }

            return auxCadena;*/
        }

        public static List<String> GetWordWidthAccents(String word)
        {
            word.Trim();
            word.ToLower();
            List <String> res=new List<String>();
            int i = 0;
            String cadena = word;
            String auxCadena;
            res.Add(cadena);
            foreach (Char cha in cadena)
            {
                switch (cha)
                {
                    case 'a':
                        auxCadena = cadena;
                        auxCadena = auxCadena.Insert(i, "á");
                        auxCadena = auxCadena.Remove(i+ 1, 1);
                        res.Add(auxCadena);
                        break;

                    case 'e':
                        auxCadena = cadena;
                        auxCadena = auxCadena.Insert(i, "é");
                        auxCadena = auxCadena.Remove(i + 1, 1);
                        res.Add(auxCadena);
                        break;
                    case 'i':
                        auxCadena = cadena;
                        auxCadena = auxCadena.Insert(i, "í");
                        auxCadena = auxCadena.Remove(i + 1, 1);
                        res.Add(auxCadena);
                        break;
                    case 'o':
                        auxCadena = cadena;
                        auxCadena = auxCadena.Insert(i, "ó");
                        auxCadena = auxCadena.Remove(i + 1, 1);
                        res.Add(auxCadena);
                        break;
                    case 'u':
                        auxCadena = cadena;
                        auxCadena = auxCadena.Insert(i, "ú");
                        auxCadena = auxCadena.Remove(i + 1, 1);
                        res.Add(auxCadena);
                        break;
                    default:
                        break;
                }
                i++;
            }
            return res;
        }



        public static BitmapImage GetBitmapFromUri(String path, UriKind uriKind = UriKind.Absolute)
        {
            var bitMapImagePic = new BitmapImage();
            if (path != "")
            {
                if (File.Exists(path) || uriKind == UriKind.Relative)
                {
                    bitMapImagePic.BeginInit();
                    if (uriKind == UriKind.Absolute)
                    {
                        bitMapImagePic.CacheOption = BitmapCacheOption.OnLoad;
                    }

                    bitMapImagePic.CreateOptions = BitmapCreateOptions.IgnoreImageCache;
                    bitMapImagePic.UriSource = new Uri(path, uriKind);
                    bitMapImagePic.EndInit();

                    if (uriKind == UriKind.Absolute)
                    {
                        bitMapImagePic.Freeze();
                    }
                }
                else
                {
                    bitMapImagePic = null;
                }
            }
            return bitMapImagePic;
       }
    

        public static List<Word> SearchWord(String texto)
        {
            texto.Trim();
            //texto.ToLower();
            List<Word> test = new List<Word>();

            List<Word> consulta = dbManager.GetWords(texto);
            if(consulta!=null)
            {
                test = consulta;
            }

            return test;
        }

        public static Word WordByID(String id, List<Word> words)
        {
            Word ret = null;

            foreach (Word word1 in words)
            {
                if (word1.ID == id)
                {
                    ret = word1;
                    break;
                }
            }
            return ret;
        }

        public static Boolean isArchive(String nameArchive)
        {
            Boolean res = false;
            if(nameArchive != "")
            {
                if(nameArchive.Count()>=5)
                {
                    String b = nameArchive;
                    int tamBackground = b.Length;
                    String p = nameArchive.Substring(tamBackground - 4);
                    if (p.Contains("."))
                    {
                        res = true;
                    }
                }

            }

            return res;
        }

        public static void CopyFiles(String pathDirectory, String path, String atributte)
        {
           // CreateDirectory(pathDirectory);

            DirectoryInfo dir = new DirectoryInfo(pathDirectory);
            FileInfo[] files = dir.GetFiles(atributte + ".*", SearchOption.TopDirectoryOnly);

            String extension = GetExtension(path);
            String path2 = pathDirectory + "\\" + atributte + "." + extension;

            if (files.Length > 0)
            {
                //File exists
                foreach (FileInfo file in files)
                {
                    file.Delete();
                    if(isArchive(path))
                    {
                        if (!String.Equals(path, path2))
                            File.Copy(path, path2, true);
                    }

                }
            }
            else
            {
                if (isArchive(path))
                {
                    if (!String.Equals(path, path2))
                        File.Copy(path, path2, true);
                }

            }


        }


        public static FileInfo[] SeachFileDirectory(String path,String nameFile)
        {
            DirectoryInfo dir = new DirectoryInfo(path);
            FileInfo[] files = dir.GetFiles(nameFile + ".*", SearchOption.TopDirectoryOnly);
            return files;
        }

        public static void CreateDirectory(String path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
                
            }
        }

        public static void CreateHiddenDirectory(String path)
        {
            if (!Directory.Exists(path))
            {
                DirectoryInfo di = Directory.CreateDirectory(path);
                di.Attributes = FileAttributes.Directory | FileAttributes.Hidden;
            }
        }

        public static void CreateHiddenDirectory2(String path)
        {
            DirectoryInfo di = new DirectoryInfo(path);

            //See if directory has hidden flag, if not, make hidden
            if ((di.Attributes & FileAttributes.Hidden) != FileAttributes.Hidden)
            {
                //Add Hidden flag    
                di.Attributes |= FileAttributes.Hidden;
            }
        }




    public static String GetExtension(String fileName)
        {
            String extension = "";
            int tamName = fileName.Length;
            if (tamName > 4)
            {
                extension = fileName.Substring(tamName - 3);
            }

            return extension;
        }

        public static String ChangeToRelativePath(String path)
        {
            String res = "";
            if (path!="")
            {
                String auxPath = path;
                auxPath = auxPath.Replace('\\', '/');
                var cadena = auxPath.Split(new[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
                res = cadena.Last();
            }
            return res;
        }

        public static void DeleteDirectory(String path)
        {
            try
            {
                if (Directory.Exists(path))
                    Directory.Delete(path, true);
            }
            catch
            {

            }

        }

        /*public static List<Word> GetSentence(String cadena)
        {
            int i = 0;
            List<String> frase = Utils.CutSentence(cadena);
            List<Word> palabras = new List<Word>();

            foreach (String s in frase)
            {
                List<Word> consulta = Utils.SearchWords(s);
                if (consulta.Count >= 1)
                {
                    palabras.AddRange(consulta);
                }
                else
                {
                    //palabras.Add(new Word("", s, "", TestingEditor.WordType.Ninguno));
                }
                i++;
            }
            return palabras;
        }*/

    }
}
