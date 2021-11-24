namespace MediaStudioService.Models.Input
{
    public class InputAccount
    {
        public string Login { get; set; }
        public string Password { get; set; }

        //По умолчанию ставим роль "Пользователь"
        public int IdTypeAccount { get; set; } = 4;
    }
}
