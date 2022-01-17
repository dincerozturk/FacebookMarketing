using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FM.Domain.Entities.Facebook
{
    public class FbAccount
    {
        [Key]
        public long Id { get; set; }

        public string Token { get; set; }

        public string FbUid { get; set; }

        public string Password { get; set; }

        public string FacebookEmail { get; set; }

        public string FacebookPassword { get; set; }

        public string BirthDay { get; set; }

        public string Name { get; set; }

        public string Note { get; set; }

        public string FaCode { get; set; }

        public List<FbToken> FbTokens { get; set; }
    }

}