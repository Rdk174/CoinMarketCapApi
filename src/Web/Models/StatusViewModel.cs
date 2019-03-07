using System;


namespace Web.Models
{
    public class StatusViewModel
    {
        public DateTimeOffset Timestamp { get; set; }
        public long ErrorCode { get; set; }
        public object ErrorMessage { get; set; }
        public long Elapsed { get; set; }
        public long CreditCount { get; set; }
    }
}