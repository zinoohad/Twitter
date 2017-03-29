using System.Collections.Generic;
using System.Data;
namespace TwitterCollector.Objects
{
    public class SubjectO
    {
        public string Name { get; set;}

        public int ID { get; set; }

        public string LanguageName = "English";

        public int LanguageID = 84;

        public string LanguageCode = "en";

        public List<KeywordO> Keywords = new List<KeywordO>();

        public SubjectO() { }

        public SubjectO(DataRow dr)
        {
            ID = int.Parse(dr["ID"].ToString());
            Name = dr["Subject"].ToString();
            LanguageID = int.Parse(dr["LanguageID"].ToString());
            LanguageName = dr["LanguageName"].ToString();
            LanguageCode = dr["LanguageCode"].ToString();
        }

        public SubjectO(int id, string subjectName)
        {
            this.ID = id;
            this.Name = subjectName;
        }
        public void SetLanguage(DataRow dr)
        {
            LanguageID = int.Parse(dr["SubjectLanguageID"].ToString());
            LanguageName = dr["SubjectLanguageCode"].ToString();
            LanguageName = dr["SubjectLanguageName"].ToString();
        }
    }

    public class KeywordO
    {
        public string Name { get; set; }

        public int ID { get; set; }

        public string LanguageName = "English";

        public int LanguageID = 84;

        public string LanguageCode = "en";

        public KeywordO() { }

        public KeywordO(DataRow dr)
        {
            ID = int.Parse(dr["KeywordID"].ToString());
            Name = dr["Keyword"].ToString();
            LanguageID = int.Parse(dr["KeywordLanguageID"].ToString());
            LanguageName = dr["KeywordLanguageName"].ToString();
            LanguageCode = dr["KeywordLanguageCode"].ToString();
        }

        public KeywordO(int id, string keywordName)
        {
            this.ID = id;
            this.Name = keywordName;
        }
    }
}