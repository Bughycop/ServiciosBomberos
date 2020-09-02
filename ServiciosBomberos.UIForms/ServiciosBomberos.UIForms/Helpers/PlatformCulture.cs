namespace ServiciosBomberos.UIForms.Helpers
{
    using System;

    public class PlatformCulture
    {
        #region Propiedades
        public string PlatformString { get; private set; }

        public string LanguageCode { get; private set; }

        public string LocaleCode { get; private set; }
        #endregion

        #region Constructores
        public PlatformCulture(string platformCultureString)
        {
            if (string.IsNullOrEmpty(platformCultureString))
            {
                throw new ArgumentException("Expected culture identifier", "platformCultureString"); // en C# 6 use nameof("platformCultureString")
            }

            PlatformString = platformCultureString.Replace("_", "-"); // .NET espera guion, no guion bajo

            var dashIndex = PlatformString.IndexOf("-", StringComparison.Ordinal);

            if (dashIndex > 0)
            {
                var parts = PlatformString.Split('-');
                LanguageCode = parts[0];
                LocaleCode = parts[1];
            }
            else
            {
                LanguageCode = PlatformString;
                LocaleCode = "";
            }
        }
        #endregion

        #region Overrides
        public override string ToString()
        {
            return PlatformString;
        }
        #endregion
    }
}
