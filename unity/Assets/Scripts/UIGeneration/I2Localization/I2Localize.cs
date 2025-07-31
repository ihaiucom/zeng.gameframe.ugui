using UnityEngine;

namespace I2.Loc
{
	public static class I2Localize
	{

		public static string LangIcon 		{ get{ return LocalizationManager.GetTranslation ("LangIcon"); } }
		public static string LanguageIcon 		{ get{ return LocalizationManager.GetTranslation ("LanguageIcon"); } }
		public static string Test 		{ get{ return LocalizationManager.GetTranslation ("Test"); } }
	}

    public static class I2Terms
	{

		public const string LangIcon = "LangIcon";
		public const string LanguageIcon = "LanguageIcon";
		public const string Test = "Test";
	}
}