namespace Laboratory_6.Model
{
    public class Language
    {
        public string LanguageName { get; set; }
        public LanguageProficiency FluentIn { get; set; }

        public Language()
        {
            LanguageName = string.Empty;
        }
    }
}
