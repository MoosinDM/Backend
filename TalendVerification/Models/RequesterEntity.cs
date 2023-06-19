namespace TalendVerification.Models
{
    public class RequesterEntity
    {
        public int Id { get; set; }
        public string ReqType { get; set; }
        public string UserName { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string MobileNumber { get; set; }
        public string AdharNumber { get; set; }
        public string DocPath { get; set; }
        public string Status { get; set; }
        public DateTime SubDate { get; set; }
    }
}
