﻿using System;
using System.Collections.Generic;

namespace DBContext.Models
{
    public partial class User
    {
        public int IdUser { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Patronymic { get; set; }
        public short? Gender { get; set; }
        public DateTime? DateBirthday { get; set; }
        public string PhoneNumber { get; set; }
        public long? IdCloudPath { get; set; }
        public int IdAccount { get; set; }

        public virtual Account IdAccountNavigation { get; set; }
    }
}