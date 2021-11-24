using System;

namespace MediaStudio.Service.Models.Input
{
    public class UpdateUserModel
    {
        public string LastName { get; set; }
        public string Patronymic { get; set; }
        public short? Gender { get; set; }
        public DateTime? DateBirthday { get; set; }
        public string PhoneNumber { get; set; }
    }
}
