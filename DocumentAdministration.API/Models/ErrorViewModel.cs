using System;

namespace DocumentAdministration.API.Models
{
    public class ErrorViewModel
    {
        public string Type { get; set; }
        public string Message { get; set; }
        public string StackTrace { get; set; }

        public ErrorViewModel(Exception ex)
        {
            Type = ex.GetType().Name;
            Message = ex.Message;
            StackTrace = ex.ToString();
        }
    }
}
