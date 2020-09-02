namespace ServiciosBomberos.UIForms.Helpers
{
    using Interfaces;
    using Resources;
    using Xamarin.Forms;

    public static class Languages
    {
        static Languages()
        {
            var ci = DependencyService.Get<ILocalize>().GetCurrentCultureInfo();
            Resource.Culture = ci;
            DependencyService.Get<ILocalize>().SetLocale(ci);
        }

        public static string AcceptLbl => Resource.AcceptLbl;
        public static string ErrorLbl => Resource.ErrorLbl;
        public static string EmailErrorLbl => Resource.EmailErrorLbl;
        public static string PasswordErrorLbl => Resource.PasswordErrorLbl;
        public static string WrongLoginLbl => Resource.WrongLoginLbl;
        public static string Login => Resource.Login;
        public static string EmailLbl => Resource.EmailLbl;
        public static string EmailPlaceHolder => Resource.EmailPlaceHolder;
        public static string PasswordPlaceHolder => Resource.PasswordPlaceHolder;
        public static string RememberLbl => Resource.RememberLbl;
        public static string PasswordLbl => Resource.PasswordLbl;
        public static string ForgotPasswordLbl => Resource.ForgotPasswordLbl;
        public static string ServicesTitle => Resource.ServicesTitle;
        public static string DayLbl => Resource.DayLbl;
        public static string ReturnLbl => Resource.ReturnLbl;
        public static string FiremanLbl => Resource.FiremanLbl;
        public static string ReinforcementLbl => Resource.ReinforcementLbl;
        public static string TypesTitle => Resource.TypesTitle;
        public static string MenuLbl => Resource.MenuLbl;
        public static string WelcomeLbl => Resource.WelcomeLbl;
        public static string AboutLbl => Resource.AboutLbl;
        public static string ModifyUserLbl => Resource.ModifyUserLbl;
        public static string LogoutLbl => Resource.LogoutLbl;
        public static string RequiredFiremanLbl => Resource.RequiredFiremanLbl;
        public static string RequiredInitHourLbl => Resource.RequiredInitHourLbl;
        public static string RequiredFinishHourLbl => Resource.RequiredFinishHourLbl;
        public static string ServiceTypeLbl => Resource.ServiceTypeLbl;
        public static string InsertNameLbl => Resource.InsertNameLbl;
        public static string ChooseCategoryLbl => Resource.ChooseCategoryLbl;
        public static string OkLbl => Resource.OkLbl;
        public static string PasswordMatchLbl => Resource.PasswordMatchLbl;
        public static string PasswordConfirmLbl => Resource.PasswordConfirmLbl;
        public static string PasswordLengthLbl => Resource.PasswordLengthLbl;
        public static string PasswordNewLbl => Resource.PasswordNewLbl;
        public static string PasswordIncorrectLbl => Resource.PasswordIncorrectLbl;
        public static string PasswordActualLbl => Resource.PasswordActualLbl;
        public static string ConfirmLbl => Resource.ConfirmLbl;
        public static string SureDeleteLbl => Resource.SureDeleteLbl;
        public static string YesLbl => Resource.YesLbl;
        public static string NoLbl => Resource.NoLbl;
        public static string RequiredFirstNameLbl => Resource.RequiredFirstNameLbl;
        public static string RequiredSecondNameLbl => Resource.RequiredSecondNameLbl;
        public static string RequiredTelephoneNumberLbl => Resource.RequiredTelephoneNumberLbl;
        public static string UserChangedLbl => Resource.UserChangedLbl;
        public static string SuEmailLbl => Resource.SuEmailLbl;
        public static string ValidEmailLbl => Resource.ValidEmailLbl;
        public static string AboutTxtLbl1 => Resource.AboutTxtLbl1;
        public static string AboutTxtLbl2 => Resource.AboutTxtLbl2;
        public static string NewServiceLbl => Resource.NewServiceLbl;
        public static string DateServiceLbl => Resource.DateServiceLbl;
        public static string Fireman1Lbl => Resource.Fireman1Lbl;
        public static string SelectFiremanLbl => Resource.SelectFiremanLbl;
        public static string StartHourLbl => Resource.StartHourLbl;
        public static string FinishHourLbl => Resource.FinishHourLbl;
        public static string TypeOfServiceLbl => Resource.TypeOfServiceLbl;
        public static string ServiceNotesLbl => Resource.ServiceNotesLbl;
        public static string SaveLbl => Resource.SaveLbl;
        public static string ServiceTypeTitle => Resource.ServiceTypeTitle;
        public static string TypeNameAssygnLbl => Resource.TypeNameAssygnLbl;
        public static string PriorityLbl => Resource.PriorityLbl;
        public static string SelecPriorityLbl => Resource.SelecPriorityLbl;
        public static string ChangePasswordTitle => Resource.ChangePasswordTitle;
        public static string ActualPasswordLbl => Resource.ActualPasswordLbl;
        public static string NewPasswordLbl => Resource.NewPasswordLbl;
        public static string InsertNewPasswordLbl => Resource.InsertNewPasswordLbl;
        public static string ConfirmPasswordLbl => Resource.ConfirmPasswordLbl;
        public static string ConfirmPasswordPlhold => Resource.ConfirmPasswordPlhold;
        public static string EditServiceTitle => Resource.EditServiceTitle;
        public static string EditServiceTypeTitle => Resource.EditServiceTypeTitle;
        public static string ModifyUserTitle => Resource.ModifyUserTitle;
        public static string NameLbl => Resource.NameLbl;
        public static string LastNameLbl => Resource.LastNameLbl;
        public static string LastNameLblBis => Resource.LastNameLblBis;
        public static string TelephoneNumberlbl => Resource.TelephoneNumberlbl;
        public static string PasswordRecoverTitle => Resource.PasswordRecoverTitle;
        public static string StatisticsLbl => Resource.StatisticsLbl;
        public static string DataLbl => Resource.DataLbl;
        public static string GraphLbl => Resource.GraphLbl;
        public static string JanuaryLbl => Resource.JanuaryLbl;
        public static string FebruaryLbl => Resource.FebruaryLbl;
        public static string MarchLbl => Resource.MarchLbl;
        public static string AprilLbl => Resource.AprilLbl;
        public static string MayLbl => Resource.MayLbl;
        public static string JuneLbl => Resource.JuneLbl;
        public static string JulyLbl => Resource.JulyLbl;
        public static string AugustLbl => Resource.AugustLbl;
        public static string SeptemberLbl => Resource.SeptemberLbl;
        public static string OctoberLbl => Resource.OctoberLbl;
        public static string NovemberLbl => Resource.NovemberLbl;
        public static string DecemberLbl => Resource.DecemberLbl;
        public static string YearLbl => Resource.YearLbl;
        public static string SelectMonthLbl => Resource.SelectMonthLbl;
    }
}
